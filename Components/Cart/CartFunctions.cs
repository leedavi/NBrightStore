﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Razor;
using System.Web.Script.Serialization;
using System.Windows.Forms.VisualStyles;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;
using DotNetNuke.Services.Exceptions;
using NBrightCore.common;
using NBrightCore.images;
using NBrightCore.render;
using NBrightDNN;
using Nevoweb.DNN.NBrightBuy.Admin;
using Nevoweb.DNN.NBrightBuy.Components.Interfaces;

namespace Nevoweb.DNN.NBrightBuy.Components.Cart
{
    public static class CartFunctions
    {
        #region "Admin Methods"

        public static string TemplateRelPath = "/DesktopModules/NBright/NBrightBuy";

        public static string ProcessCommand(string paramCmd, HttpContext context, string editlang = "")
        {
            var strOut = "CART - ERROR!! - No Security rights or function command.";
            var ajaxInfo = NBrightBuyUtils.GetAjaxFields(context);
            var userId = ajaxInfo.GetXmlPropertyInt("genxml/hidden/userid");

            switch (paramCmd)
            {
                case "cart_rendercartlist":
                    strOut = RenderCart(context);
                    break;
                case "cart_rendersummary":
                    strOut = RenderCart(context);
                    break;
                case "cart_rendershipmethod":
                    strOut = RenderCart(context);
                    break;
                case "cart_rendercartaddress":
                    strOut = RenderCart(context);
                    break;
                case "cart_recalculatecart":
                    RecalculateCart(context);
                    break;
                case "cart_redirecttopayment":
                    strOut = RedirectToPayment(context);
                    break;
                case "cart_clearcart":
                    var currentcart = new CartData(PortalSettings.Current.PortalId);
                    currentcart.DeleteCart();
                    break;
                case "cart_removefromcart":
                    RemoveFromCart(context);
                    break;
                case "cart_updatebilladdress":
                    strOut = UpdateCartAddress(context, "bill");
                    break;
                case "cart_updateshipaddress":
                    strOut = UpdateCartAddress(context, "ship");
                    break;
                case "cart_updateshipoption":
                    strOut = UpdateCartAddress(context, "shipoption");
                    break;
            }
            return strOut;
        }


        #endregion

        private static string RenderCart(HttpContext context, string carttemplate = "")
        {
            var ajaxInfo = NBrightBuyUtils.GetAjaxInfo(context);

            if (ajaxInfo.GetXmlProperty("genxml/hidden/carttemplate") != "")
            {
                carttemplate = ajaxInfo.GetXmlProperty("genxml/hidden/carttemplate");
            }
            if (carttemplate == "") carttemplate = ajaxInfo.GetXmlProperty("genxml/hidden/minicarttemplate");

            var theme = ajaxInfo.GetXmlProperty("genxml/hidden/carttheme");
            if (theme == "") theme = ajaxInfo.GetXmlProperty("genxml/hidden/minicarttheme");
            if (theme == "")
            {
                theme = StoreSettings.Current.ThemeFolder;
            }

            var lang = ajaxInfo.GetXmlProperty("genxml/hidden/lang");
            var controlpath = ajaxInfo.GetXmlProperty("genxml/hidden/controlpath");
            if (controlpath == "") controlpath = "/DesktopModules/NBright/NBrightBuy";
            var razorTempl = "";
            if (carttemplate != "")
            {
                if (lang == "") lang = Utils.GetCurrentCulture();
                var currentcart = new CartData(PortalSettings.Current.PortalId);
                if (UserController.Instance.GetCurrentUserInfo().UserID != -1)  // If we have a user, do save to update userid, so addrees checkout can get addresses.
                {
                    if (currentcart.UserId != UserController.Instance.GetCurrentUserInfo().UserID && currentcart.EditMode == "")
                    {
                        currentcart.Save();
                    }
                }

                razorTempl = NBrightBuyUtils.RazorTemplRender(carttemplate, 0, "", currentcart, controlpath, theme, lang, StoreSettings.Current.Settings());
            }
            return razorTempl;
        }

        private static void RecalculateCart(HttpContext context)
        {
            var ajaxInfoList = NBrightBuyUtils.GetAjaxInfoList(context);
            var currentcart = new CartData(PortalSettings.Current.PortalId);
            foreach (var ajaxInfo in ajaxInfoList)
            {
                currentcart.MergeCartInputData(currentcart.GetItemIndex(ajaxInfo.GetXmlProperty("genxml/hidden/itemcode")), ajaxInfo);
            }
            currentcart.Save(StoreSettings.Current.DebugMode, true);
        }

        private static string RecalculateSummary(HttpContext context)
        {
            var objCtrl = new NBrightBuyController();

            var currentcart = new CartData(PortalSettings.Current.PortalId);
            var ajaxInfo = NBrightBuyUtils.GetAjaxInfo(context, true);
            var shipoption = currentcart.GetShippingOption(); // ship option already set in address update.

            currentcart.AddExtraInfo(ajaxInfo);
            currentcart.SetShippingOption(shipoption);
            currentcart.PurchaseInfo.SetXmlProperty("genxml/currentcartstage", "cartsummary"); // (Legacy) we need to set this so the cart calcs shipping
            currentcart.PurchaseInfo.SetXmlProperty("genxml/extrainfo/genxml/radiobuttonlist/shippingprovider", ajaxInfo.GetXmlProperty("genxml/radiobuttonlist/shippingprovider"));

            var shipref = ajaxInfo.GetXmlProperty("genxml/radiobuttonlist/shippingprovider");
            var displayanme = "";
            var shipInfo = objCtrl.GetByGuidKey(PortalSettings.Current.PortalId, -1, "SHIPPING", shipref);
            if (shipInfo != null)
            {
                displayanme = shipInfo.GetXmlProperty("genxml/textbox/shippingdisplayname");
            }
            if (displayanme == "") displayanme = shipref;
            currentcart.PurchaseInfo.SetXmlProperty("genxml/extrainfo/genxml/hidden/shippingdisplayanme", displayanme);

            var shippingproductcode = ajaxInfo.GetXmlProperty("genxml/hidden/shippingproductcode");
            currentcart.PurchaseInfo.SetXmlProperty("genxml/shippingproductcode", shippingproductcode);
            var pickuppointref = ajaxInfo.GetXmlProperty("genxml/hidden/pickuppointref");
            currentcart.PurchaseInfo.SetXmlProperty("genxml/pickuppointref", pickuppointref);
            var pickuppointaddr = ajaxInfo.GetXmlProperty("genxml/hidden/pickuppointaddr");
            currentcart.PurchaseInfo.SetXmlProperty("genxml/pickuppointaddr", pickuppointaddr);

            currentcart.Lang = ajaxInfo.Lang;  // set lang so we can send emails in same language the order was made in.

            currentcart.Save(StoreSettings.Current.DebugMode, true);

            return "OK";
        }

        private static string RedirectToPayment(HttpContext context)
        {
            try
            {
                RecalculateSummary(context);

                var currentcart = new CartData(PortalSettings.Current.PortalId);

                if (currentcart.GetCartItemList().Count > 0)
                {
                    currentcart.SetValidated(true);
                    if (currentcart.EditMode == "E") currentcart.ConvertToOrder();
                }
                else
                {
                    currentcart.SetValidated(true);
                }
                currentcart.Save();

                var rtnurl = Globals.NavigateURL(StoreSettings.Current.PaymentTabId);
                if (currentcart.EditMode == "E")
                {
                    // is order being edited, so return to order status after edit.
                    // ONLY if the cartsummry is being displayed to the manager.
                    currentcart.ConvertToOrder();
                    // redirect to back office
                    var param = new string[2];
                    param[0] = "ctrl=orders";
                    param[1] = "eid=" + currentcart.PurchaseInfo.ItemID.ToString("");
                    var strbackofficeTabId = StoreSettings.Current.Get("backofficetabid");
                    var backofficeTabId = PortalSettings.Current.ActiveTab.TabID;
                    if (Utils.IsNumeric(strbackofficeTabId)) backofficeTabId = Convert.ToInt32(strbackofficeTabId);
                    rtnurl = Globals.NavigateURL(backofficeTabId, "", param);
                }
                return rtnurl;

            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
                return "ERROR";
            }
        }

        private static void RemoveFromCart(HttpContext context)
        {
            var ajaxInfo = NBrightBuyUtils.GetAjaxInfo(context);
            var currentcart = new CartData(PortalSettings.Current.PortalId);
            currentcart.RemoveItem(ajaxInfo.GetXmlProperty("genxml/hidden/itemcode"));
            currentcart.Save(StoreSettings.Current.DebugMode);
        }

        private static string UpdateCartAddress(HttpContext context, String addresstype = "")
        {
            var currentcart = new CartData(PortalSettings.Current.PortalId);
            var ajaxInfo = NBrightBuyUtils.GetAjaxInfo(context, true);

            currentcart.PurchaseInfo.SetXmlProperty("genxml/currentcartstage", "cartsummary"); // (Legacy) we need to set this so the cart calcs shipping

            if (addresstype == "bill")
            {
                currentcart.AddBillingAddress(ajaxInfo);
                currentcart.Save();
            }

            if (addresstype == "ship")
            {
                if (currentcart.GetShippingOption() == "2") // need to test this, becuase in legacy code the shipping option is set to "2" when we save the shipping address.
                {
                    currentcart.AddShippingAddress(ajaxInfo);
                    currentcart.Save();
                }
            }

            if (addresstype == "shipoption")
            {
                var shipoption = ajaxInfo.GetXmlProperty("genxml/radiobuttonlist/rblshippingoptions");
                currentcart.SetShippingOption(shipoption);
                currentcart.Save();
            }

            return addresstype;
        }

    }
}

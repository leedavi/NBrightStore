﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using DotNetNuke.Common;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Portals;
using NBrightCore;
using NBrightCore.TemplateEngine;
using NBrightCore.common;
using NBrightCore.images;
using NBrightCore.providers;
using NBrightCore.render;
using DotNetNuke.Entities.Users;
using NBrightDNN;
using Nevoweb.DNN.NBrightBuy.Admin;
using Nevoweb.DNN.NBrightBuy.Components;
using Image = System.Web.UI.WebControls.Image;

namespace Nevoweb.DNN.NBrightBuy.render
{
    public class GenXmlTemplateExt : GenXProvider
    {

        private string _rootname = "genxml";
        private string _databindColumn = "XMLData";
        private Dictionary<string, string> _settings = null;

        #region "Override methods"

        // This section overrides the interface methods for the GenX provider.
        // It allows providers to create controls/Literals in the NBright template system.

        public override bool CreateGenControl(string ctrltype, Control container, XmlNode xmlNod, string rootname = "genxml", string databindColum = "XMLData", string cultureCode = "", Dictionary<string, string> settings = null)
        {
            //remove namespace of token.
            // If the NBrigthCore template system is being used across mutliple modules in the portal (that use a provider interface for tokens),
            // then a namespace should be added to the front of the type attribute, this stops clashes in the tokening system. NOTE: the "tokennamespace" tag is now supported as well
            if (ctrltype.StartsWith("nbs:")) ctrltype = ctrltype.Substring(4);

            _rootname = rootname;
            _databindColumn = databindColum;
            _settings = settings;
            switch (ctrltype)
            {
                case "addqty":
                    CreateQtyField(container, xmlNod);
                    return true;
                case "modelqty":
                    CreateModelQtyField(container, xmlNod);
                    return true;
                case "relatedproducts":
                    CreateRelatedlist(container, xmlNod);
                    return true;
                case "productcount":
                    CreateProductCount(container, xmlNod);
                    return true;
                case "productdoclink":
                    CreateProductDocLink(container, xmlNod);
                    return true;
                case "productdocdesc":
                    CreateProductDocDesc(container, xmlNod);
                    return true;
                case "productimgdesc":
                    CreateProductImageDesc(container, xmlNod);
                    return true;
                case "productdocfilename":
                    CreateProductDocFileName(container, xmlNod);
                    return true;
                case "productdoctitle":
                    CreateProductDocTitle(container, xmlNod);
                    return true;
                case "productoptionname":
                    CreateProductOptionName(container, xmlNod);
                    return true;
                case "productoption":
                    Createproductoptions(container, xmlNod);
                    return true;
                case "modelslist":
                    Createmodelslist(container, xmlNod);
                    return true;
                case "orderitemlist":
                    CreateOrderItemlist(container, xmlNod);
                    return true;                    
                case "modelsradio":
                    Createmodelsradio(container, xmlNod);
                    return true;
                case "modelsdropdown":
                    Createmodelsdropdown(container, xmlNod);
                    return true;
                case "modeldefault":
                    Createmodeldefault(container, xmlNod);
                    return true;
                case "productname":
                    CreateProductName(container, xmlNod);
                    return true;
                case "manufacturer":
                    CreateManufacturer(container, xmlNod);
                    return true;
                case "summary":
                    CreateSummary(container, xmlNod);
                    return true;
                case "seoname":
                    CreateSEOname(container, xmlNod);
                    return true;
                case "seopagetitle":
                    CreateSEOpagetitle(container, xmlNod);
                    return true;
                case "tagwords":
                    CreateTagwords(container, xmlNod);
                    return true;
                case "description":
                    CreateDescription(container, xmlNod);
                    return true;
                case "currencyisocode":
                    CreateCurrencyIsoCode(container, xmlNod);
                    return true;
                case "price":
                    CreateFromPrice(container, xmlNod);
                    return true;
                case "saleprice":
                    CreateSalePrice(container, xmlNod);
                    return true;
                case "dealerprice":
                    CreateDealerPrice(container, xmlNod);
                    return true;
                case "bestprice":
                    CreateBestPrice(container, xmlNod);
                    return true;
                case "quantity":
                    CreateQuantity(container, xmlNod);
                    return true;
                case "thumbnail":
                    CreateThumbNailer(container, xmlNod);
                    return true;
                case "editlink":
                    CreateEditLink(container, xmlNod);
                    return true;
                case "entrylink":
                    CreateEntryLink(container, xmlNod);
                    return true;
                case "entryurl":
                    CreateEntryUrl(container, xmlNod);
                    return true;                    
                case "returnlink":
                    CreateReturnLink(container, xmlNod);
                    return true;
                case "currenturl":
                    CreateCurrentUrl(container, xmlNod);
                    return true;
                case "hrefpagelink":
                    Createhrefpagelink(container, xmlNod);
                    return true;                    
                case "catdropdown":
                    CreateCatDropDownList(container, xmlNod);
                    return true;
                case "categoryparentname":
                    CreateCategoryParentName(container, xmlNod);
                    return true;
                case "catlistbox":
                    CreateCatListBox(container, xmlNod);
                    return true;
                case "grouplistbox":
                    CreateGroupListBox(container, xmlNod);
                    return true;
                case "catcheckboxlist":
                    CreateCatCheckBoxList(container, xmlNod);
                    return true;
                case "countrydropdown":
                    CreateCountryDropDownList(container, xmlNod);
                    return true;
                case "addressdropdown":
                    CreateAddressDropDownList(container, xmlNod);
                    return true;
                case "catbreadcrumb":
                    CreateCatBreadCrumb(container, xmlNod);
                    return true;
                case "catshortcrumb":
                    CreateCatBreadCrumb(container, xmlNod);
                    return true;
                case "catdefaultname":
                    CreateCatDefaultName(container, xmlNod);
                    return true;
                case "catdefault":
                    CreateCatDefault(container, xmlNod);
                    return true;
                case "catvalueof":
                    CreateCatValueOf(container, xmlNod);
                    return true;
                case "catbreakof":
                    CreateCatBreakOf(container, xmlNod);
                    return true;
                case "cathtmlof":
                    CreateCatHtmlOf(container, xmlNod);
                    return true;
                case "testof":
                    CreateTestOf(container, xmlNod);
                    return true;
                case "if":
                    CreateTestOf(container, xmlNod);
                    return true;
                case "itemlistcount":
                    CreateItemListCount(container, xmlNod);
                    return true;
                case "itemlistfield":
                    CreateItemListField(container, xmlNod);
                    return true;
                case "itemlistadd":
                    CreateItemListLink(container, xmlNod, "add");
                    return true;
                case "itemlistremove":
                    CreateItemListLink(container, xmlNod, "remove");
                    return true;
                case "itemlistdelete":
                    CreateItemListLink(container, xmlNod, "delete");
                    return true;
                case "cartqtytextbox":
                    CreateCartQtyTextbox(container, xmlNod);
                    return true;
                case "orderstatus":
                    Createorderstatusdropdown(container, xmlNod);
                    return true;
                case "modelstatus":
                    Createmodelstatusdropdown(container, xmlNod);
                    return true;
                case "cartemailaddress":
                    CreateCartEmailAddress(container, xmlNod);
                    return true;
                case "groupdropdown":
                    Creategroupdropdown(container, xmlNod);
                    return true;
                case "culturecodedropdown":
                    Createculturecodedropdown(container, xmlNod);
                    return true;
                case "selectlocalebutton":
                    CreateSelectLangaugeButton(container, xmlNod);
                    return true;
                case "editflag":
                    CreateEditFlag(container, xmlNod);
                    return true;                    
                case "imageof":
                    CreateImage(container, xmlNod);
                    return true;
                case "concatenate":
                    CreateConcatenate(container, xmlNod);
                    return true;                
                default:
                    return false;

            }

        }

        public override string GetField(Control ctrl)
        {
            return "";
        }

        public override void SetField(Control ctrl, string newValue)
        {
        }

        public override string GetGenXml(List<Control> genCtrls, XmlDataDocument xmlDoc, string originalXml, string folderMapPath, string xmlRootName = "genxml")
        {
            return "";
        }

        public override string GetGenXmlTextBox(List<Control> genCtrls, XmlDataDocument xmlDoc, string originalXml, string folderMapPath, string xmlRootName = "genxml")
        {
            return "";
        }

        public override object PopulateGenObject(List<Control> genCtrls, object obj)
        {
            return null;
        }

        #endregion

        #region "create nbs:testof"

        private void CreateTestOf(Control container, XmlNode xmlNod)
        {
            var lc = new Literal { Text = xmlNod.OuterXml };
            lc.DataBinding += TestOfDataBinding;
            container.Controls.Add(lc);
        }

        private void TestOfDataBinding(object sender, EventArgs e)
        {
            var lc = (Literal)sender;
            var container = (IDataItemContainer)lc.NamingContainer;
            try
            {
                NBrightInfo info;
                                
                ProductData prodData;

                lc.Visible = NBrightGlobal.IsVisible;
                var xmlDoc = new XmlDataDocument();
                string display = "{ON}";
                string displayElse = "{OFF}";
                string dataValue = "";
                CartData currentcart;

                xmlDoc.LoadXml("<root>" + lc.Text + "</root>");
                var xmlNod = xmlDoc.SelectSingleNode("root/tag");


                if (xmlNod != null && (xmlNod.Attributes != null && (xmlNod.Attributes["display"] != null)))
                {
                    display = xmlNod.Attributes["display"].InnerXml;
                }

                if (xmlNod != null && (xmlNod.Attributes != null && (xmlNod.Attributes["displayelse"] != null)))
                {
                    displayElse = xmlNod.Attributes["displayelse"].InnerXml;
                }
                else
                {
                    if (display == "{ON}") displayElse = "{OFF}";
                    if (display == "{OFF}") displayElse = "{ON}";
                }

                //get test value, set all tests to else
                string output = displayElse;

                if (container.DataItem != null && xmlNod != null && (xmlNod.Attributes != null && xmlNod.Attributes["function"] != null))
                {
                    XmlNode nod;
                    var testValue = "";
                    if ((xmlNod.Attributes["testvalue"] != null)) testValue = xmlNod.Attributes["testvalue"].Value;

                    // check for setting key
                    var settingkey = "";
                    if ((xmlNod.Attributes["key"] != null)) settingkey = xmlNod.Attributes["key"].Value;

                    var role = "";
                    if ((xmlNod.Attributes["role"] != null)) role = xmlNod.Attributes["role"].Value;

                    var index = "";
                    if ((xmlNod.Attributes["index"] != null)) index = xmlNod.Attributes["index"].Value;

                    var modulekey = "";
                    if ((xmlNod.Attributes["modulekey"] != null)) modulekey = xmlNod.Attributes["modulekey"].Value;

                    var targetmodulekey = "";
                    if ((xmlNod.Attributes["targetmodulekey"] != null)) targetmodulekey = xmlNod.Attributes["targetmodulekey"].Value;

                    // do normal xpath test
                    if (xmlNod.Attributes["xpath"] != null)
                    {
                        nod = GenXmlFunctions.GetGenXmLnode(DataBinder.Eval(container.DataItem, _databindColumn).ToString(), xmlNod.Attributes["xpath"].InnerXml);
                        if (nod != null)
                        {
                            dataValue = nod.InnerText;
                        }
                    }

                    // do special tests for named fucntions
                    if (xmlNod.Attributes["function"] != null)
                    {
                        switch (xmlNod.Attributes["function"].Value.ToLower())
                        {
                            case "searchactive":
                                var navdata2 = new NavigationData(PortalSettings.Current.PortalId, targetmodulekey);
                                if (navdata2.Criteria == "") dataValue = "false"; else dataValue = "true";
                                break;
                            case "productcount":
                                var navdata = new NavigationData(PortalSettings.Current.PortalId, modulekey);
                                dataValue = navdata.RecordCount;
                                break;
                            case "price":
                                dataValue = GetFromPrice((NBrightInfo) container.DataItem);
                                break;
                            case "dealerprice":
                                dataValue = GetDealerPrice((NBrightInfo) container.DataItem);
                                break;
                            case "saleprice":
                                dataValue = GetSalePrice((NBrightInfo) container.DataItem);
                                break;
                            case "imgexists":
                                dataValue = testValue;
                                nod = GenXmlFunctions.GetGenXmLnode(DataBinder.Eval(container.DataItem, _databindColumn).ToString(), "genxml/imgs/genxml[" + dataValue + "]");
                                if (nod == null) dataValue = "FALSE";
                                break;
                            case "modelexists":
                                dataValue = testValue;
                                nod = GenXmlFunctions.GetGenXmLnode(DataBinder.Eval(container.DataItem, _databindColumn).ToString(), "genxml/models/genxml[" + dataValue + "]");
                                if (nod == null) dataValue = "FALSE";
                                break;
                            case "optionexists":
                                dataValue = testValue;
                                nod = GenXmlFunctions.GetGenXmLnode(DataBinder.Eval(container.DataItem, _databindColumn).ToString(), "genxml/options/genxml[" + dataValue + "]");
                                if (nod == null) dataValue = "FALSE";
                                break;
                            case "isinstock":
                                dataValue = "FALSE";
                                if (IsInStock((NBrightInfo) container.DataItem, testValue))
                                {
                                    dataValue = "TRUE";
                                    testValue = "TRUE";
                                }
                                break;
                            case "inwishlist":
                                var productid = DataBinder.Eval(container.DataItem, "ItemId").ToString();
                                dataValue = "FALSE";
                                if (Utils.IsNumeric(productid))
                                {
                                    var wl = new ItemListData(-1,StoreSettings.Current.StorageTypeClient );
                                    if (wl.IsInList(productid))
                                    {
                                        dataValue = "TRUE";
                                        testValue = "TRUE";
                                    }
                                }
                                break;
                            case "isonsale":
                                dataValue = "FALSE";
                                var saleprice = GetSalePrice((NBrightInfo) container.DataItem);
                                if ((Utils.IsNumeric(saleprice)) && (Convert.ToDouble(saleprice) > 0))
                                {
                                    dataValue = "TRUE";
                                    testValue = "TRUE";
                                }
                                break;
                            case "hasrelateditems":
                                dataValue = "FALSE";
                                info = (NBrightInfo)container.DataItem;
                                prodData = ProductUtils.GetProductData(info.ItemID, info.Lang);
                                if (prodData.GetRelatedProducts().Count > 0)
                                {
                                    dataValue = "TRUE";
                                    testValue = "TRUE";
                                }
                                break;
                            case "hasdocuments":
                                dataValue = "FALSE";
                                info = (NBrightInfo)container.DataItem;
                                prodData = ProductUtils.GetProductData(info.ItemID, info.Lang);
                                if (prodData.Docs.Count > 0)
                                {
                                    dataValue = "TRUE";
                                    testValue = "TRUE";
                                }
                                break;
                            case "haspurchasedocuments":
                                dataValue = "FALSE";
                                info = (NBrightInfo)container.DataItem;
                                prodData = ProductUtils.GetProductData(info.ItemID, info.Lang);
                                if (prodData.Docs.Select(i => i.GetXmlProperty("genxml/checkbox/chkpurchase") == "True").Any())
                                {
                                    dataValue = "TRUE";
                                    testValue = "TRUE";
                                }
                                break;
                            case "isdocpurchasable":
                                dataValue = "FALSE";
                                nod = GenXmlFunctions.GetGenXmLnode(DataBinder.Eval(container.DataItem, _databindColumn).ToString(), "genxml/docs/genxml[" + index + "]/hidden/docid");
                                if (nod != null)
                                {
                                    info = (NBrightInfo)container.DataItem;
                                    prodData = ProductUtils.GetProductData(info.ItemID, info.Lang);
                                    if (prodData.Docs.Select(i => i.GetXmlProperty("genxml/checkbox/chkpurchase") == "True" && i.GetXmlProperty("genxml/hidden/docid") == nod.InnerText).Any())
                                    {
                                        dataValue = "TRUE";
                                        testValue = "TRUE";
                                    }
                                }
                                break;
                            case "isdocpurchased":
                                dataValue = "FALSE";
                                nod = GenXmlFunctions.GetGenXmLnode(DataBinder.Eval(container.DataItem, _databindColumn).ToString(), "genxml/docs/genxml[" + index + "]/hidden/docid");
                                if (nod != null && Utils.IsNumeric(nod.InnerText))
                                {
                                    var uInfo = UserController.GetCurrentUserInfo();
                                    //[TODO: work out method of finding if user purchased document.]
                                    //if (NBrightBuyV2Utils.DocHasBeenPurchasedByDocId(uInfo.UserID, Convert.ToInt32(nod.InnerText)))
                                    //{
                                    //    dataValue = "TRUE";
                                    //    testValue = "TRUE";
                                    //}
                                }
                                break;
                            case "hasmodelsoroptions":

                                dataValue = "FALSE";
                                nod = GenXmlFunctions.GetGenXmLnode(DataBinder.Eval(container.DataItem, _databindColumn).ToString(), "genxml/models/genxml[2]/hidden/modelid");
                                if (nod != null && nod.InnerText != "")
                                {
                                    dataValue = "TRUE";
                                    testValue = "TRUE";
                                }
                                if (dataValue == "FALSE")
                                {
                                    nod = GenXmlFunctions.GetGenXmLnode(DataBinder.Eval(container.DataItem, _databindColumn).ToString(), "genxml/options/genxml[1]/hidden/optionid");
                                    if (nod != null && nod.InnerText != "")
                                    {
                                        dataValue = "TRUE";
                                        testValue = "TRUE";
                                    }
                                }
                                break;
                            case "isproductincart":
                                dataValue = "FALSE";
                                var cartData = new CartData(PortalSettings.Current.PortalId);
                                info = (NBrightInfo)container.DataItem;
                                if (cartData.GetCartItemList().Select(i => i.GetXmlProperty("genxml/productid") == info.ItemID.ToString("")).Any())
                                {
                                    dataValue = "TRUE";
                                    testValue = "TRUE";
                                }
                                break;
                            case "settings":
                                dataValue = "FALSE";
                                if (_settings[settingkey] != null && _settings[settingkey] == testValue)
                                {
                                    dataValue = "TRUE";
                                    testValue = "TRUE";
                                }
                                break;
                            case "debugmode":
                                dataValue = "FALSE";
                                if (StoreSettings.Current.DebugMode)
                                {
                                    dataValue = "TRUE";
                                    testValue = "TRUE";
                                }
                                break;
                            case "isinrole":
                                dataValue = "FALSE";
                                if (CmsProviderManager.Default.IsInRole(role))
                                {
                                    dataValue = "TRUE";
                                    testValue = "TRUE";
                                }
                                break;
                            case "isclientordermode":
                                dataValue = "FALSE";
                                currentcart = new CartData(PortalSettings.Current.PortalId);
                                if (currentcart.IsClientOrderMode())
                                {
                                    dataValue = "TRUE";
                                    testValue = "TRUE";
                                }
                                break;
                            case "carteditmode":
                                dataValue = "FALSE";
                                currentcart = new CartData(PortalSettings.Current.PortalId);
                                var editmode = currentcart.GetInfo().GetXmlProperty("genxml/carteditmode");
                                if (editmode == testValue)
                                {
                                    dataValue = "TRUE";
                                    testValue = "TRUE";
                                }
                                break;
                            default:
                                dataValue = "";
                                break;
                        }
                    }

                    if (testValue == dataValue)
                        output = display;
                    else
                        output = displayElse;

                }


                // If the Visible flag is OFF then keep it off, even if the child test is true
                // This allows nested tests to function correctly, by using the parent result.
                if (!NBrightGlobal.IsVisible)
                {
                    if (output == "{ON}" | output == "{OFF}") NBrightGlobal.IsVisibleList.Add(false); // only add level on {} testof
                }
                else
                {
                    if (output == "{ON}") NBrightGlobal.IsVisibleList.Add(true);
                    if (output == "{OFF}") NBrightGlobal.IsVisibleList.Add(false);
                }

                if (NBrightGlobal.IsVisible && output != "{ON}") lc.Text = output;
                if (output == "{ON}" | output == "{OFF}") lc.Text = ""; // don;t display the test tag
            }
            catch (Exception)
            {
                lc.Text = "";
            }
        }


        #endregion

        #region "create Shortcuts"

        private void CreateProductImageDesc(Control container, XmlNode xmlNod)
        {
            if (xmlNod.Attributes != null && (xmlNod.Attributes["index"] != null))
            {
                if (Utils.IsNumeric(xmlNod.Attributes["index"].Value)) // must have a index
                {
                    var l = new Literal();
                    l.DataBinding += ShortcutDataBinding;
                    l.Text = xmlNod.Attributes["index"].Value;
                    l.Text = "genxml/lang/genxml/imgs/genxml[" + xmlNod.Attributes["index"].Value + "]/textbox/txtimagedesc";
                    container.Controls.Add(l);
                }
            }
        }

        private void CreateProductDocDesc(Control container, XmlNode xmlNod)
        {
            if (xmlNod.Attributes != null && (xmlNod.Attributes["index"] != null))
            {
                if (Utils.IsNumeric(xmlNod.Attributes["index"].Value)) // must have a index
                {
                    var l = new Literal();
                    l.DataBinding += ShortcutDataBinding;
                    l.Text = xmlNod.Attributes["index"].Value;
                    l.Text = "genxml/lang/genxml/docs/genxml[" + xmlNod.Attributes["index"].Value + "]/textbox/txtdocdesc";
                    container.Controls.Add(l);
                }
            }
        }
        private void CreateProductDocFileName(Control container, XmlNode xmlNod)
        {
            if (xmlNod.Attributes != null && (xmlNod.Attributes["index"] != null))
            {
                if (Utils.IsNumeric(xmlNod.Attributes["index"].Value)) // must have a index
                {
                    var l = new Literal();
                    l.DataBinding += ShortcutDataBinding;
                    l.Text = xmlNod.Attributes["index"].Value;
                    l.Text = "genxml/docs/genxml[" + xmlNod.Attributes["index"].Value + "]/textbox/txtfilename";
                    container.Controls.Add(l);
                }
            }
        }
        private void CreateProductDocTitle(Control container, XmlNode xmlNod)
        {
            if (xmlNod.Attributes != null && (xmlNod.Attributes["index"] != null))
            {
                if (Utils.IsNumeric(xmlNod.Attributes["index"].Value)) // must have a index
                {
                    var l = new Literal();
                    l.DataBinding += ShortcutDataBinding;
                    l.Text = xmlNod.Attributes["index"].Value;
                    l.Text = "genxml/lang/genxml/docs/genxml[" + xmlNod.Attributes["index"].Value + "]/textbox/txttitle";
                    container.Controls.Add(l);
                }
            }
        }
        private void CreateProductOptionName(Control container, XmlNode xmlNod)
        {
            if (xmlNod.Attributes != null && (xmlNod.Attributes["index"] != null))
            {
                if (Utils.IsNumeric(xmlNod.Attributes["index"].Value)) // must have a index
                {
                    var l = new Literal();
                    l.DataBinding += ShortcutDataBinding;
                    l.Text = xmlNod.Attributes["index"].Value;
                    l.Text = "genxml/lang/genxml/options/genxml[./hidden/optionid=../../../../options/genxml[" + xmlNod.Attributes["index"].Value + "]/hidden/optionid]/textbox/txtoptiondesc";
                    container.Controls.Add(l);
                }
            }
        }
        private void CreateProductName(Control container, XmlNode xmlNod)
        {
            var l = new Literal();
            l.DataBinding += ShortcutDataBinding;
            l.Text = "genxml/lang/genxml/textbox/txtproductname";
            container.Controls.Add(l);
        }
        private void CreateManufacturer(Control container, XmlNode xmlNod)
        {
            var l = new Literal();
            l.DataBinding += ShortcutDataBinding;
            l.Text = "genxml/lang/genxml/textbox/txtmanufacturer";
            container.Controls.Add(l);
        }
        private void CreateSummary(Control container, XmlNode xmlNod)
        {
            var l = new Literal();
            l.DataBinding += ShortcutDataBinding;
            l.Text = "genxml/lang/genxml/textbox/txtsummary";
            container.Controls.Add(l);
        }
        private void CreateSEOname(Control container, XmlNode xmlNod)
        {
            var l = new Literal();
            l.DataBinding += ShortcutDataBinding;
            l.Text = "genxml/lang/genxml/textbox/txtseoname";
            container.Controls.Add(l);
        }
        private void CreateTagwords(Control container, XmlNode xmlNod)
        {
            var l = new Literal();
            l.DataBinding += ShortcutDataBinding;
            l.Text = "genxml/lang/genxml/textbox/txttagwords";
            container.Controls.Add(l);
        }
        private void CreateSEOpagetitle(Control container, XmlNode xmlNod)
        {
            var l = new Literal();
            l.DataBinding += ShortcutDataBinding;
            l.Text = "genxml/lang/genxml/textbox/txtseopagetitle";
            container.Controls.Add(l);
        }
        private void CreateDescription(Control container, XmlNode xmlNod)
        {
            var l = new Literal();
            l.DataBinding += ShortcutDataBindingHtmlDecode;
            l.Text = "genxml/lang/genxml/edt/description";
            container.Controls.Add(l);
        }
        private void CreateQuantity(Control container, XmlNode xmlNod)
        {
            var l = new Literal();
            l.DataBinding += ShortcutDataBinding;
            //l.Text = "genxml/models/genxml/textbox/txtqtyremaining";
            //Get quantity with the lowest unitcost value with xpath
            l.Text = "(genxml/models/genxml/textbox/txtqtyremaining[not(number((.)[1]) > number((../../../genxml/textbox/txtqtyremaining)[1]))][1])[1]";
            container.Controls.Add(l);
        }
        private void ShortcutDataBinding(object sender, EventArgs e)
        {
            var l = (Literal)sender;
            var container = (IDataItemContainer)l.NamingContainer;
            try
            {
                l.Visible = NBrightGlobal.IsVisible;
                XmlNode nod = GenXmlFunctions.GetGenXmLnode(DataBinder.Eval(container.DataItem, _databindColumn).ToString(), l.Text);
                if ((nod != null))
                {
                    l.Text = System.Web.HttpUtility.UrlDecode(XmlConvert.DecodeName(nod.InnerText)); // the urldecode is included for filename on documents, which was forced to encoded in v2 so it work correctly. 
                }
                else
                {
                    l.Text = "";
                }

            }
            catch (Exception ex)
            {
                l.Text = ex.ToString();
            }
        }
        private void ShortcutDataBindingHtmlDecode(object sender, EventArgs e)
        {
            var l = (Literal)sender;
            var container = (IDataItemContainer)l.NamingContainer;
            try
            {
                l.Visible = NBrightGlobal.IsVisible;
                XmlNode nod = GenXmlFunctions.GetGenXmLnode(DataBinder.Eval(container.DataItem, _databindColumn).ToString(), l.Text);
                if ((nod != null))
                {
                    l.Text = System.Web.HttpUtility.HtmlDecode(XmlConvert.DecodeName(nod.InnerText));
                }
                else
                {
                    l.Text = "";
                }

            }
            catch (Exception ex)
            {
                l.Text = ex.ToString();
            }
        }
        private void ShortcutDataBindingCurrency(object sender, EventArgs e)
        {
            var l = (Literal)sender;
            var container = (IDataItemContainer)l.NamingContainer;
            try
            {
                l.Visible = NBrightGlobal.IsVisible;
                XmlNode nod = GenXmlFunctions.GetGenXmLnode(DataBinder.Eval(container.DataItem, _databindColumn).ToString(), l.Text);
                if ((nod != null))
                {
                    Double v = 0;
                    if (Utils.IsNumeric(XmlConvert.DecodeName(nod.InnerText)))
                    {
                        v  = Convert.ToDouble(XmlConvert.DecodeName(nod.InnerText));
                    }
                    l.Text = NBrightBuyUtils.FormatToStoreCurrency(v); 
                }
                else
                {
                    l.Text = "";
                }

            }
            catch (Exception ex)
            {
                l.Text = ex.ToString();
            }
        }

        #endregion

        #region "Create Thumbnailer"

        private void CreateThumbNailer(Control container, XmlNode xmlNod)
        {
            var l = new Literal();

            var thumbparams = "";
            var imagenum = "1";
            if (xmlNod.Attributes != null)
            {
                foreach (XmlAttribute a in xmlNod.Attributes)
                {
                    if (a.Name.ToLower() != "type")
                    {
                        if (a.Name.ToLower() != "image")
                            thumbparams += "&amp;" + a.Name + "=" + a.Value; // don;t use the type in the params
                        else
                            imagenum = a.Value;
                    }
                }
            }

            l.Text = imagenum + ":" + thumbparams; // pass the attributes to be added

            l.DataBinding += ThumbNailerDataBinding;
            container.Controls.Add(l);
        }

        private void ThumbNailerDataBinding(object sender, EventArgs e)
        {
            var l = (Literal)sender;
            var container = (IDataItemContainer)l.NamingContainer;
            try
            {
                l.Visible = NBrightGlobal.IsVisible;
                var imagesrc = "0";
                var imageparams = l.Text.Split(':');

                XmlNode nod = GenXmlFunctions.GetGenXmLnode(DataBinder.Eval(container.DataItem, _databindColumn).ToString(), "genxml/imgs/genxml[" + imageparams[0] + "]/hidden/imageurl");
                if ((nod != null)) imagesrc = nod.InnerText;
                var url = "/DesktopModules/NBright/NBrightBuy/NBrightThumb.ashx?src=" + imagesrc + imageparams[1];
                l.Text = url;
            }
            catch (Exception ex)
            {
                l.Text = ex.ToString();
            }
        }

        #endregion

        #region "create EntryLink/URL control"

        private void CreateEntryLink(Control container, XmlNode xmlNod)
        {
            var lk = new HyperLink();
            lk = (HyperLink)GenXmlFunctions.AssignByReflection(lk, xmlNod);

            if (xmlNod.Attributes != null)
            {
                if (xmlNod.Attributes["tabid"] != null) lk.Attributes.Add("tabid", xmlNod.Attributes["tabid"].InnerText);
                if (xmlNod.Attributes["modkey"] != null) lk.Attributes.Add("modkey", xmlNod.Attributes["modkey"].InnerText);
                if (xmlNod.Attributes["xpath"] != null) lk.Attributes.Add("xpath", xmlNod.Attributes["xpath"].InnerText);
            }
            lk.DataBinding += EntryLinkDataBinding;
            container.Controls.Add(lk);
        }

        private void EntryLinkDataBinding(object sender, EventArgs e)
        {
            var lk = (HyperLink)sender;
            var container = (IDataItemContainer)lk.NamingContainer;
			try
			{
				//set a default url

                lk.Visible = NBrightGlobal.IsVisible;

				var entryid = Convert.ToString(DataBinder.Eval(container.DataItem, "ItemID"));

			    var urlname = "Default";
                if (lk.Attributes["xpath"] != null)
                {
                    var nod = GenXmlFunctions.GetGenXmLnode(DataBinder.Eval(container.DataItem, _databindColumn).ToString(), lk.Attributes["xpath"]);
                    if ((nod != null)) urlname = nod.InnerText;
                }
                var t = "";
				if (lk.Attributes["tabid"] != null && Utils.IsNumeric(lk.Attributes["tabid"])) t = lk.Attributes["tabid"];
                var c = "";
                if (lk.Attributes["catid"] != null && Utils.IsNumeric(lk.Attributes["catid"])) c = lk.Attributes["catid"];
			    var moduleref = "";
                if ((lk.Attributes["modkey"] != null)) moduleref = lk.Attributes["modkey"];

                var url = NBrightBuyUtils.GetEntryUrl(PortalSettings.Current.PortalId, entryid, moduleref, urlname, t);
                lk.NavigateUrl = url;

			}
			catch (Exception ex)
			{
				lk.Text = ex.ToString();
			}
        }

        private void CreateEntryUrl(Control container, XmlNode xmlNod)
        {
            var l = new Literal();
            if (xmlNod.Attributes != null)
            {
                // we dont; have any attributes for a literal, so pass data as string (tabid,modulekey,entryname)
                var t = PortalSettings.Current.ActiveTab.TabID.ToString("");
                var mk = "";
                var xp = "";
                if (xmlNod.Attributes["tabid"] != null) t = xmlNod.Attributes["tabid"].InnerText;
                if (xmlNod.Attributes["modkey"] != null) mk = xmlNod.Attributes["modkey"].InnerText;
                if (xmlNod.Attributes["xpath"] != null) xp = xmlNod.Attributes["xpath"].InnerText;

                l.Text = t + '*' + mk + '*' + xp.Replace('*','-');
            }
            l.DataBinding += EntryUrlDataBinding;
            container.Controls.Add(l);
        }

        private void EntryUrlDataBinding(object sender, EventArgs e)
        {
            var l = (Literal)sender;
            var container = (IDataItemContainer)l.NamingContainer;
            try
            {
                //set a default url

                l.Visible = NBrightGlobal.IsVisible;

                var entryid = Convert.ToString(DataBinder.Eval(container.DataItem, "ItemID"));
                var dataIn = l.Text.Split('*'); 
                var urlname = "Default";
                var t = "";
                var moduleref = "";

                if (dataIn.Length == 3)
                {
                    if (Utils.IsNumeric(dataIn[0])) t = dataIn[0];
                    if (Utils.IsNumeric(dataIn[1])) moduleref = dataIn[1];
                    var nod = GenXmlFunctions.GetGenXmLnode(DataBinder.Eval(container.DataItem, _databindColumn).ToString(), dataIn[2]);
                    if ((nod != null)) urlname = nod.InnerText;
                }

                var url = NBrightBuyUtils.GetEntryUrl(PortalSettings.Current.PortalId, entryid, moduleref, urlname, t);
                l.Text = url;

            }
            catch (Exception ex)
            {
                l.Text = ex.ToString();
            }
        }

        #endregion

        #region "create ReturnLink control"

        private void CreateReturnLink(Control container, XmlNode xmlNod)
        {
            var lk = new HyperLink();
            lk = (HyperLink)GenXmlFunctions.AssignByReflection(lk, xmlNod);

            if (xmlNod.Attributes != null && (xmlNod.Attributes["tabid"] != null))
            {
                lk.Attributes.Add("tabid", xmlNod.Attributes["tabid"].InnerText);
            }

            lk.DataBinding += ReturnLinkDataBinding;
            container.Controls.Add(lk);
        }

        private void ReturnLinkDataBinding(object sender, EventArgs e)
        {
            var lk = (HyperLink)sender;
            var container = (IDataItemContainer)lk.NamingContainer;
            try
            {
                lk.Visible = NBrightGlobal.IsVisible;

                var t = "";
                if (lk.Attributes["tabid"] != null && Utils.IsNumeric(lk.Attributes["tabid"]))
                {
                    t = lk.Attributes["tabid"];
                }

                var url = NBrightBuyUtils.GetReturnUrl(t);
                lk.NavigateUrl = url;

            }
            catch (Exception ex)
            {
                lk.Text = ex.ToString();
            }
        }

        #endregion

        #region "create HrefPageLink control"
        private void Createhrefpagelink(Control container, XmlNode xmlNod)
        {
            var l = new Literal();
            l.Text = "-1";
            if (xmlNod.Attributes != null && (xmlNod.Attributes["moduleid"] != null))
            {
                l.Text = xmlNod.Attributes["moduleid"].InnerXml;
            }
            l.DataBinding += hrefpagelinkbind;
            container.Controls.Add(l);
        }

        private void hrefpagelinkbind(object sender, EventArgs e)
        {
            var l = (Literal)sender;
            var container = (IDataItemContainer)l.NamingContainer;
            try
            {
                l.Visible = NBrightGlobal.IsVisible;
                var catparam = "";
                var pagename = PortalSettings.Current.ActiveTab.TabName + ".aspx";
                var catid = Utils.RequestParam(HttpContext.Current, "catid");
                if (Utils.IsNumeric(catid))
                {
                    pagename = NBrightBuyUtils.GetCurrentPageName(Convert.ToInt32(catid)) + ".aspx";
                    catparam = "&catid=" + catid;
                }
                var url = DotNetNuke.Services.Url.FriendlyUrl.FriendlyUrlProvider.Instance().FriendlyUrl(PortalSettings.Current.ActiveTab, "~/Default.aspx?tabid=" + PortalSettings.Current.ActiveTab.TabID.ToString("") + catparam + "&page=" + Convert.ToString(DataBinder.Eval(container.DataItem, "PageNumber")) + "&pagemid=" + l.Text, pagename);
                l.Text = "<a href=\"" + url + "\">" + Convert.ToString(DataBinder.Eval(container.DataItem, "Text")) + "</a>";
            }
            catch (Exception ex)
            {
                l.Text = ex.ToString();
            }
        }


        #endregion

        #region "create CurrentUrl control"
        private void CreateCurrentUrl(Control container, XmlNode xmlNod)
        {
            var l = new Literal();

            l.DataBinding += CurrentUrlDataBinding;
            container.Controls.Add(l);
        }

        private void CurrentUrlDataBinding(object sender, EventArgs e)
        {
            var l = (Literal)sender;
            var container = (IDataItemContainer)l.NamingContainer;
            try
            {
                l.Visible = NBrightGlobal.IsVisible;
                //set a default url
                var url = DotNetNuke.Entities.Portals.PortalSettings.Current.ActiveTab.FullUrl;
                l.Text = url;

            }
            catch (Exception ex)
            {
                l.Text = ex.ToString();
            }
        }

        #endregion

        #region  "category dropdown and checkbox list"

        private void CreateCatCheckBoxList(Control container, XmlNode xmlNod)
        {
            try
            {

                var cbl = new CheckBoxList();
                cbl = (CheckBoxList) GenXmlFunctions.AssignByReflection(cbl, xmlNod);
                var selected = false;
                if (xmlNod.Attributes != null && (xmlNod.Attributes["selected"] != null))
                {
                    if (xmlNod.Attributes["selected"].InnerText.ToLower() == "true") selected = true;
                }

                var tList = GetCatList(xmlNod);
                foreach (var tItem in tList)
                {
                    var li = new ListItem();
                    li.Text = tItem.Value;
                    li.Value = tItem.Key.ToString("");
                    li.Selected = selected;
                    cbl.Items.Add(li);
                }

                cbl.DataBinding += CbListDataBinding;
                container.Controls.Add(cbl);
            }
            catch (Exception e)
            {
                var lc = new Literal();
                lc.Text = e.ToString();
                container.Controls.Add(lc);
            }

        }

        private void CbListDataBinding(object sender, EventArgs e)
        {
            var chk = (CheckBoxList)sender;
            var container = (IDataItemContainer)chk.NamingContainer;
            try
            {
                chk.Visible = NBrightGlobal.IsVisible;
                var xmlNod = GenXmlFunctions.GetGenXmLnode(chk.ID, "checkboxlist", (string)DataBinder.Eval(container.DataItem, _databindColumn));
                var xmlNodeList = xmlNod.SelectNodes("./chk");
                if (xmlNodeList != null)
                {
                    foreach (XmlNode xmlNoda in xmlNodeList)
                    {
                        if (xmlNoda.Attributes != null)
                        {
                            if (xmlNoda.Attributes.GetNamedItem("data") != null)
                            {
                                var datavalue = xmlNoda.Attributes["data"].Value;
                                //use the data attribute if there
                                if ((chk.Items.FindByValue(datavalue).Value != null))
                                {
                                    chk.Items.FindByValue(datavalue).Selected = Convert.ToBoolean(xmlNoda.Attributes["value"].Value);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                //do nothing
            }

        }

        private void CreateCatDropDownList(Control container, XmlNode xmlNod)
        {
            try
            {
                var ddl = new DropDownList();
                ddl = (DropDownList)GenXmlFunctions.AssignByReflection(ddl, xmlNod);

                if (xmlNod.Attributes != null && (xmlNod.Attributes["allowblank"] != null))
                {
                        var li = new ListItem();
                        li.Text = "";
                        li.Value = "";
                        ddl.Items.Add(li);
                }

                var tList = GetCatList(xmlNod);
                foreach (var tItem in tList)
                {
                    var li = new ListItem();
                    li.Text = tItem.Value;
                    li.Value = tItem.Key.ToString("");
                    
                    ddl.Items.Add(li);
                }

                ddl.DataBinding += DdListDataBinding;
                container.Controls.Add(ddl);
            }
            catch (Exception e)
            {
                var lc = new Literal();
                lc.Text = e.ToString();
                container.Controls.Add(lc);
            }
        }

        private void DdListDataBinding(object sender, EventArgs e)
        {
            var ddl = (DropDownList)sender;
            var container = (IDataItemContainer)ddl.NamingContainer;

            try
            {
                ddl.Visible = NBrightGlobal.IsVisible;

                var strValue = GenXmlFunctions.GetGenXmlValue(ddl.ID, "dropdownlist", Convert.ToString(DataBinder.Eval(container.DataItem, _databindColumn)));

                if ((ddl.Items.FindByValue(strValue) != null))
                {
                    ddl.SelectedValue = strValue;
                }
                else
                {
                    var nod = GenXmlFunctions.GetGenXmLnode(ddl.ID, "dropdownlist", Convert.ToString(DataBinder.Eval(container.DataItem, _databindColumn)));
                    if ((nod.Attributes != null) && (nod.Attributes["selectedtext"] != null))
                    {
                        strValue = XmlConvert.DecodeName(nod.Attributes["selectedtext"].Value);
                        if ((ddl.Items.FindByValue(strValue) != null))
                        {
                            ddl.SelectedValue = strValue;
                        }
                    }
                }
            }
            catch (Exception)
            {
                //do nothing
            }
        }

        private void CreateCatListBox(Control container, XmlNode xmlNod)
        {
            try
            {
                var ddl = new ListBox();
                ddl = (ListBox)GenXmlFunctions.AssignByReflection(ddl, xmlNod);

                if (xmlNod.Attributes != null && (xmlNod.Attributes["allowblank"] != null))
                {
                    var li = new ListItem();
                    li.Text = "";
                    li.Value = "";
                    ddl.Items.Add(li);
                }

                var tList = GetCatList(xmlNod);
                foreach (var tItem in tList)
                {
                    var li = new ListItem();
                    li.Text = tItem.Value;
                    li.Value = tItem.Key.ToString("");

                    ddl.Items.Add(li);
                }

                ddl.DataBinding += DdListBoxDataBinding;
                container.Controls.Add(ddl);
            }
            catch (Exception e)
            {
                var lc = new Literal();
                lc.Text = e.ToString();
                container.Controls.Add(lc);
            }
        }

        private void CreateGroupListBox(Control container, XmlNode xmlNod)
        {
            try
            {
                var ddl = new ListBox();
                ddl = (ListBox)GenXmlFunctions.AssignByReflection(ddl, xmlNod);

                var tList = NBrightBuyUtils.GetCategoryGroups(StoreSettings.Current.EditLanguage, true);
                foreach (var tItem in tList)
                {
                    if (tItem.GetXmlProperty("genxml/textbox/groupref") != "cat")
                    {
                        var li = new ListItem();
                        li.Text = tItem.GetXmlProperty("genxml/lang/genxml/textbox/groupname");
                        li.Value = tItem.GetXmlProperty("genxml/textbox/groupref");
                        ddl.Items.Add(li);                        
                    }
                }

                ddl.DataBinding += DdListBoxDataBinding;
                container.Controls.Add(ddl);
            }
            catch (Exception e)
            {
                var lc = new Literal();
                lc.Text = e.ToString();
                container.Controls.Add(lc);
            }
        }
        private void DdListBoxDataBinding(object sender, EventArgs e)
        {
            var ddl = (ListBox)sender;
            var container = (IDataItemContainer)ddl.NamingContainer;

            try
            {
                ddl.Visible = NBrightGlobal.IsVisible;

                var strValue = GenXmlFunctions.GetGenXmlValue(ddl.ID, "dropdownlist", Convert.ToString(DataBinder.Eval(container.DataItem, _databindColumn)));

                if ((ddl.Items.FindByValue(strValue) != null))
                {
                    ddl.SelectedValue = strValue;
                }
                else
                {
                    var nod = GenXmlFunctions.GetGenXmLnode(ddl.ID, "dropdownlist", Convert.ToString(DataBinder.Eval(container.DataItem, _databindColumn)));
                    if ((nod.Attributes != null) && (nod.Attributes["selectedtext"] != null))
                    {
                        strValue = XmlConvert.DecodeName(nod.Attributes["selectedtext"].Value);
                        if ((ddl.Items.FindByValue(strValue) != null))
                        {
                            ddl.SelectedValue = strValue;
                        }
                    }
                }
            }
            catch (Exception)
            {
                //do nothing
            }
        }

        private void CreateCategoryParentName(Control container, XmlNode xmlNod)
        {
            try
            {
                var l = new Literal();
                l.DataBinding += CategoryParentNameDataBinding;
                container.Controls.Add(l);
            }
            catch (Exception e)
            {
                var lc = new Literal();
                lc.Text = e.ToString();
                container.Controls.Add(lc);
            }
        }

        private void CategoryParentNameDataBinding(object sender, EventArgs e)
        {
            var l = (Literal)sender;
            var container = (IDataItemContainer)l.NamingContainer;

            try
            {
                l.Visible = NBrightGlobal.IsVisible;
                l.Text = "";
                 var strValue = Convert.ToString(DataBinder.Eval(container.DataItem, "ParentItemId"));
                if (Utils.IsNumeric(strValue))
                {
                    var pCat = new CategoryData(Convert.ToInt32(strValue), StoreSettings.Current.EditLanguage);
                    if (pCat.Exists) l.Text = pCat.Info.GetXmlProperty("genxml/lang/genxml/textbox/txtcategoryname");                    
                }
            }
            catch (Exception)
            {
                //do nothing
            }
        }



        private Dictionary<int, string> BuildCatList(int displaylevels = 20, Boolean showHidden = false, Boolean showArchived = false, int parentid = 0, String catreflist = "", String prefix = "", bool displayCount = false, bool showEmpty = true, string groupref = "", string breadcrumbseparator = ">",string lang = "")
        {
            if (lang == "") lang = Utils.GetCurrentCulture();

            var rtnDic = new Dictionary<int, string>();

            var strCacheKey = "NBrightBuy_BuildCatList" + PortalSettings.Current.PortalId + "*" + displaylevels + "*" + showHidden.ToString(CultureInfo.InvariantCulture) + "*" + showArchived.ToString(CultureInfo.InvariantCulture) + "*" + parentid + "*" + catreflist + "*" + prefix + "*" + Utils.GetCurrentCulture() + "*" + showEmpty + "*" + displayCount + "*" + groupref + "*" + lang;

            var objCache = NBrightBuyUtils.GetModCache(strCacheKey);

            if (objCache == null | StoreSettings.Current.DebugMode)
            {
                var grpCatCtrl = new GrpCatController(lang);
                var d = new Dictionary<int, string>();
                var rtnList = new List<GroupCategoryData>();
                rtnList = grpCatCtrl.GetTreeCategoryList(rtnList, 0, parentid, groupref,breadcrumbseparator);
                var strCount = "";
                foreach (var grpcat in rtnList)
                {
                    if (displayCount) strCount = " (" + grpcat.entrycount.ToString("") + ")";

                    if (grpcat.depth < displaylevels)
                    {
                        if (showEmpty || grpcat.entrycount > 0)
                        {
                            if (grpcat.ishidden == false || showHidden)
                            {
                                var addprefix = new String(' ', grpcat.depth).Replace(" ", prefix);
                                if (catreflist == "")
                                    rtnDic.Add(grpcat.categoryid, addprefix + grpcat.categoryname + strCount);
                                else
                                {
                                    if (grpcat.categoryref != "" &&
                                        (catreflist + ",").Contains(grpcat.categoryref + ","))
                                    {
                                        rtnDic.Add(grpcat.categoryid, addprefix + grpcat.categoryname + strCount);
                                    }
                                }
                            }
                        }
                    }
                }
                NBrightBuyUtils.SetModCache(-1, strCacheKey, rtnDic);

            }
            else
            {
                rtnDic = (Dictionary<int, string>)objCache;
            }
            return rtnDic;
        }

        private Dictionary<int, string> GetCatList(XmlNode xmlNod)
        {
            var displaylevels = 20;
            var parentref = "";
            var prefix = "..";
            var showhidden = "False";
            var showarchived = "False";
            var showempty = "True";
            var showHidden = false;
            var showArchived = false;
            var catreflist = "";
            var parentid = 0;
            var displaycount = "False";
            var displayCount = false;
            var showEmpty = true;
            var groupref = "";
            var filtermode = "";
            List<int> validCatList = null;
            var modulekey = "";
            var redirecttabid = "";
            var tabid = "";
            var lang = Utils.GetCurrentCulture(); 

            if (xmlNod.Attributes != null)
            {
                if (xmlNod.Attributes["displaylevels"] != null)
                {
                    if (Utils.IsNumeric(xmlNod.Attributes["displaylevels"].Value)) displaylevels = Convert.ToInt32(xmlNod.Attributes["displaylevels"].Value);
                }

                if (xmlNod.Attributes["parentref"] != null) parentref = xmlNod.Attributes["parentref"].Value;
                if (xmlNod.Attributes["showhidden"] != null) showhidden = xmlNod.Attributes["showhidden"].Value;
                if (xmlNod.Attributes["showarchived"] != null) showarchived = xmlNod.Attributes["showarchived"].Value;
                if (xmlNod.Attributes["showempty"] != null) showempty = xmlNod.Attributes["showempty"].Value;
                if (xmlNod.Attributes["displaycount"] != null) displaycount = xmlNod.Attributes["displaycount"].Value;
                if (xmlNod.Attributes["prefix"] != null) prefix = xmlNod.Attributes["prefix"].Value;
                if (xmlNod.Attributes["groupref"] != null) groupref = xmlNod.Attributes["groupref"].Value;
                if (xmlNod.Attributes["filtermode"] != null) filtermode = xmlNod.Attributes["filtermode"].Value;
                if (xmlNod.Attributes["modulekey"] != null) modulekey = xmlNod.Attributes["modulekey"].Value;
                if (xmlNod.Attributes["lang"] != null) lang = xmlNod.Attributes["lang"].Value;
                
                if (showhidden.ToLower() == "true") showHidden = true;
                if (showarchived.ToLower() == "true") showArchived = true;
                if (showempty.ToLower() == "false") showEmpty = false;
                if (displaycount.ToLower() == "true") displayCount = true;
                if (xmlNod.Attributes["catreflist"] != null) catreflist = xmlNod.Attributes["catreflist"].Value;
                var grpCatCtrl = new GrpCatController(lang);
                if (parentref != "")
                {
                    var p = grpCatCtrl.GetGrpCategoryByRef(parentref);
                    if (p != null) parentid = p.categoryid;                    
                }
                var catid = "";
                if (filtermode != "")
                {
                    var navigationData = new NavigationData(PortalSettings.Current.PortalId, modulekey);
                    catid = Utils.RequestQueryStringParam(HttpContext.Current.Request, "catid");
                    if (String.IsNullOrEmpty(catid)) catid = navigationData.CategoryId; 
                    if (Utils.IsNumeric(catid))
                    {
                        validCatList = GetCateoriesInProductList(Convert.ToInt32(catid));
                    }
                }

            }

            var rtnList = BuildCatList(displaylevels, showHidden, showArchived, parentid, catreflist, prefix, displayCount, showEmpty, groupref,">",lang);

            if (validCatList != null)
            {
                var nonValid = new List<int>();
                // we have a filter on the list, so remove any categories not in valid list.
                foreach (var k in rtnList)
                {
                    if (!validCatList.Contains(k.Key)) nonValid.Add(k.Key);
                }
                foreach (var k in nonValid)
                {
                    rtnList.Remove(k);
                }
            }

            return rtnList;
        }

        /// <summary>
        /// Return a list of category ids for all the valid categories for a given product list (selected by a categoryid)  
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        private List<int> GetCateoriesInProductList(int categoryId)
        {
            var objQual = DotNetNuke.Data.DataProvider.Instance().ObjectQualifier;
            var dbOwner = DotNetNuke.Data.DataProvider.Instance().DatabaseOwner;

            var objCtrl = new NBrightBuyController();
            var strXML = objCtrl.GetSqlxml("select distinct XrefItemId from " + dbOwner + "[" + objQual + "NBrightBuy] where (typecode = 'CATCASCADE' or typecode = 'CATXREF') and parentitemid in (select parentitemid from " + dbOwner + "[" + objQual + "NBrightBuy] where (typecode = 'CATCASCADE' or typecode = 'CATXREF') and XrefItemId in (" + categoryId + ")) for xml raw ");
            // get returned XML into generic List
            var xmlDoc = new XmlDataDocument();
            xmlDoc.LoadXml("<root>" + strXML + "</root>");
            var nList = xmlDoc.SelectNodes("root/row");
            var rtnList = new List<int>();
            foreach (XmlNode n in nList)
            {
                if (n.Attributes["XrefItemId"].Value != null && Utils.IsNumeric(n.Attributes["XrefItemId"].Value))
                {
                    rtnList.Add(Convert.ToInt32(n.Attributes["XrefItemId"].Value));
                }
            }
            return rtnList;
        }

        #endregion

        #region "catbreadcrumb"

        private void CreateCatBreadCrumb(Control container, XmlNode xmlNod)
        {
            var lc = new Literal();
            lc.Text = xmlNod.OuterXml;
            lc.DataBinding += CatBreadCrumbDataBind;
            container.Controls.Add(lc);
        }

        private void CatBreadCrumbDataBind(object sender, EventArgs e)
        {
            var lc = (Literal)sender;
            var container = (IDataItemContainer)lc.NamingContainer;
            try
            {
                var grpCatCtrl = new GrpCatController(Utils.GetCurrentCulture());

                lc.Visible = NBrightGlobal.IsVisible;

                if (NBrightGlobal.IsVisible)
                {

                    var xmlDoc = new XmlDataDocument();
                    xmlDoc.LoadXml("<root>" + lc.Text + "</root>");
                    var xmlNod = xmlDoc.SelectSingleNode("root/tag");

                    var catid = -1;
                    if (container.DataItem is NBrightInfo)
                    {
                        // Must be displaying a product or category with (NBrightInfo), so get categoryid
                        var objCInfo = (NBrightInfo) container.DataItem;
                        if (objCInfo.TypeCode == "PRD" || String.IsNullOrEmpty(objCInfo.TypeCode)) // no type is list header, so use catid in url if there. 
                        {
                            //Is product so get categoryid    
                            var id = Convert.ToString(DataBinder.Eval(container.DataItem, "ItemId"));
                            var targetModuleKey = "";
                            if (xmlNod != null && xmlNod.Attributes != null && xmlNod.Attributes["targetmodulekey"] != null) targetModuleKey = xmlNod.Attributes["targetmodulekey"].InnerText;
                            var obj = grpCatCtrl.GetCurrentCategoryData(PortalSettings.Current.PortalId, lc.Page.Request, Convert.ToInt32(id), _settings, targetModuleKey);
                            if (obj != null) catid = obj.categoryid;
                        }
                        else
                        {
                            catid = objCInfo.ItemID;
                        }
                    }

                    if (container.DataItem is GroupCategoryData)
                    {
                        // GroupCategoryData class, so use categoryid
                        var id = Convert.ToString(DataBinder.Eval(container.DataItem, "categoryid"));
                        if (Utils.IsNumeric(id)) catid = Convert.ToInt32(id);
                    }


                    var intLength = 400;
                    var intShortLength = -1;
                    var isLink = false;
                    var separator = ">";
                    var aslist = false;

                    if (xmlNod != null && xmlNod.Attributes != null)
                    {
                        if (xmlNod.Attributes["length"] != null)
                        {
                            if (Utils.IsNumeric(xmlNod.Attributes["length"].InnerText))
                            {
                                intLength = Convert.ToInt32(xmlNod.Attributes["length"].InnerText);
                            }
                        }
                        if (xmlNod.Attributes["links"] != null) isLink = true;
                        if (xmlNod.Attributes["short"] != null)
                        {
                            if (Utils.IsNumeric(xmlNod.Attributes["short"].InnerText))
                            {
                                intShortLength = Convert.ToInt32(xmlNod.Attributes["short"].InnerText);
                            }
                        }
                        if (xmlNod.Attributes["separator"] != null) separator = xmlNod.Attributes["separator"].InnerText;
                        if (xmlNod.Attributes["aslist"] != null && xmlNod.Attributes["aslist"].InnerText.ToLower() == "true") aslist = true;
                    }

                    if (catid > 0) // check we have a catid
                    {
                        if (isLink)
                        {
                            var defTabId = PortalSettings.Current.ActiveTab.TabID;
                            if (_settings.ContainsKey("ddllisttabid") && Utils.IsNumeric(_settings["ddllisttabid"])) defTabId = Convert.ToInt32(_settings["ddllisttabid"]);
                            lc.Text = grpCatCtrl.GetBreadCrumbWithLinks(catid, defTabId, intShortLength, separator, aslist);
                        }
                        else
                        {
                            lc.Text = grpCatCtrl.GetBreadCrumb(catid, intShortLength, separator, aslist);
                        }

                        if ((lc.Text.Length > intLength) && (!aslist))
                        {
                            lc.Text = lc.Text.Substring(0, (intLength - 3)) + "...";
                        }
                    }
                }
            }
            catch (Exception)
            {
                lc.Text = "";
            }
        }

        #endregion

        #region "CreateCatDefaultName"

        private void CreateCatDefaultName(Control container, XmlNode xmlNod)
        {
            var lc = new Literal();
            if (xmlNod.Attributes != null && xmlNod.Attributes["default"] != null) lc.Text = xmlNod.Attributes["default"].InnerText;
            lc.DataBinding += CatDefaultNameDataBind;
            container.Controls.Add(lc);
        }

        private void CatDefaultNameDataBind(object sender, EventArgs e)
        {
            var lc = (Literal)sender;
            var container = (IDataItemContainer)lc.NamingContainer;
            try
            {
                lc.Visible = NBrightGlobal.IsVisible;
                var moduleId = DataBinder.Eval(container.DataItem, "ModuleId");
                var id = Convert.ToString(DataBinder.Eval(container.DataItem, "ItemId"));
                var lang = Convert.ToString(DataBinder.Eval(container.DataItem, "lang"));

                if (Utils.IsNumeric(id) && Utils.IsNumeric(moduleId))
                {
                    var grpCatCtrl = new GrpCatController(Utils.GetCurrentCulture());
                    var objCInfo = grpCatCtrl.GetCurrentCategoryData(PortalSettings.Current.PortalId, lc.Page.Request, Convert.ToInt32(id));
                    if (objCInfo != null)
                    {
                        lc.Text = objCInfo.categoryname;
                    }
                }

            }
            catch (Exception ex)
            {
                lc.Text = ex.ToString();
            }
        }

        #endregion

        #region "CreateCatDefault"

        private void CreateCatDefault(Control container, XmlNode xmlNod)
        {
            var lc = new Literal();
            if (xmlNod.Attributes != null && xmlNod.Attributes["name"] != null) lc.Text = xmlNod.Attributes["name"].InnerText;
            lc.DataBinding += CatDefaultDataBind;
            container.Controls.Add(lc);
        }

        private void CatDefaultDataBind(object sender, EventArgs e)
        {
            var lc = (Literal)sender;
            var name = lc.Text;
            lc.Text = "";
            var container = (IDataItemContainer)lc.NamingContainer;
            try
            {
                lc.Visible = NBrightGlobal.IsVisible;
                var moduleId = DataBinder.Eval(container.DataItem, "ModuleId");
                var id = Convert.ToString(DataBinder.Eval(container.DataItem, "ItemId"));
                var lang = Convert.ToString(DataBinder.Eval(container.DataItem, "lang"));

                if (Utils.IsNumeric(id) && Utils.IsNumeric(moduleId))
                {
                    var grpCatCtrl = new GrpCatController(Utils.GetCurrentCulture());
                    var objCInfo = grpCatCtrl.GetCurrentCategoryData(PortalSettings.Current.PortalId, lc.Page.Request, Convert.ToInt32(id));
                    if (objCInfo != null)
                    {
                        switch (name.ToLower())
                        {
                            case "categorydesc":
                                lc.Text = objCInfo.categorydesc;
                                break;
                            case "message":
                                lc.Text = System.Web.HttpUtility.HtmlDecode(objCInfo.message);
                                break;
                            case "archived":
                                lc.Text = objCInfo.archived.ToString(CultureInfo.InvariantCulture);
                                break;
                            case "breadcrumb":
                                lc.Text = objCInfo.breadcrumb;
                                break;
                            case "categoryid":
                                lc.Text = objCInfo.categoryid.ToString("");
                                break;
                            case "categoryname":
                                lc.Text = objCInfo.categoryname;
                                break;
                            case "categoryref":
                                lc.Text = objCInfo.categoryref;
                                break;
                            case "depth":
                                lc.Text = objCInfo.depth.ToString("");
                                break;
                            case "disabled":
                                lc.Text = objCInfo.disabled.ToString(CultureInfo.InvariantCulture) ;
                                break;
                            case "entrycount":
                                lc.Text = objCInfo.entrycount.ToString("");
                                break;
                            case "grouptyperef":
                                lc.Text = objCInfo.grouptyperef;
                                break;
                            case "imageurl":
                                lc.Text = objCInfo.imageurl;
                                break;
                            case "ishidden":
                                lc.Text = objCInfo.ishidden.ToString(CultureInfo.InvariantCulture);
                                break;
                            case "isvisible":
                                lc.Text = objCInfo.isvisible.ToString(CultureInfo.InvariantCulture) ;
                                break;
                            case "metadescription":
                                lc.Text = objCInfo.metadescription;
                                break;
                            case "metakeywords":
                                lc.Text = objCInfo.metakeywords;
                                break;
                            case "parentcatid":
                                lc.Text = objCInfo.parentcatid.ToString("");
                                break;
                            case "recordsortorder":
                                lc.Text = objCInfo.recordsortorder.ToString("");
                                break;
                            case "seoname":
                                lc.Text = objCInfo.seoname;
                                if (lc.Text == "") lc.Text = objCInfo.categoryname;
                                break;
                            case "seopagetitle":
                                lc.Text = objCInfo.seopagetitle ;
                                break;
                            case "url":
                                lc.Text = objCInfo.url ;
                                break;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                lc.Text = ex.ToString();
            }
        }

        #endregion

        #region "CreateCatValueOf"

        private void CreateCatValueOf(Control container, XmlNode xmlNod)
        {
            var lc = new Literal();
            if (xmlNod.Attributes != null && (xmlNod.Attributes["xpath"] != null))
            {
                lc.Text = xmlNod.Attributes["xpath"].Value;
            }
            lc.DataBinding += CatValueOfDataBind;
            container.Controls.Add(lc);
        }

        private void CatValueOfDataBind(object sender, EventArgs e)
        {
            var lc = (Literal)sender;
            var container = (IDataItemContainer)lc.NamingContainer;
            try
            {
                lc.Visible = NBrightGlobal.IsVisible;
                var moduleId = DataBinder.Eval(container.DataItem, "ModuleId");
                var id = Convert.ToString(DataBinder.Eval(container.DataItem, "ItemId"));
                var lang = Convert.ToString(DataBinder.Eval(container.DataItem, "lang"));
                if (Utils.IsNumeric(id) && Utils.IsNumeric(moduleId))
                {
                    var grpCatCtrl = new GrpCatController(Utils.GetCurrentCulture());
                    var objCInfo = grpCatCtrl.GetCurrentCategoryInfo(PortalSettings.Current.PortalId, lc.Page.Request, Convert.ToInt32(id));
                    if (objCInfo != null)
                    {
                        lc.Text = objCInfo.GetXmlProperty(lc.Text);
                    }
                    else
                    {
                        lc.Text = "";
                    }
                }

            }
            catch (Exception)
            {
                lc.Text = "";
            }
        }

        #endregion

        #region "CreateCatBreakOf"

        private void CreateCatBreakOf(Control container, XmlNode xmlNod)
        {
            var lc = new Literal();
            if (xmlNod.Attributes != null && (xmlNod.Attributes["xpath"] != null))
            {
                lc.Text = xmlNod.Attributes["xpath"].Value;
            }
            lc.DataBinding += CatBreakOfDataBind;
            container.Controls.Add(lc);
        }

        private void CatBreakOfDataBind(object sender, EventArgs e)
        {
            var lc = (Literal)sender;
            var container = (IDataItemContainer)lc.NamingContainer;
            try
            {
                lc.Visible = NBrightGlobal.IsVisible;
                var moduleId = DataBinder.Eval(container.DataItem, "ModuleId");
                var id = Convert.ToString(DataBinder.Eval(container.DataItem, "ItemId"));
                var lang = Convert.ToString(DataBinder.Eval(container.DataItem, "lang"));
                if (Utils.IsNumeric(id) && Utils.IsNumeric(moduleId))
                {
                    var grpCatCtrl = new GrpCatController(Utils.GetCurrentCulture());
                    var objCInfo = grpCatCtrl.GetCurrentCategoryInfo(PortalSettings.Current.PortalId, lc.Page.Request, Convert.ToInt32(id));
                    if (objCInfo != null)
                    {
                        lc.Text = objCInfo.GetXmlProperty(lc.Text);
                        lc.Text = System.Web.HttpUtility.HtmlEncode(lc.Text);
                        lc.Text = lc.Text.Replace(Environment.NewLine, "<br/>");
                    }
                    else
                    {
                        lc.Text = "";
                    }
                }

            }
            catch (Exception)
            {
                lc.Text = "";
            }
        }

        #endregion

        #region "CreateCatHtmlOf"

        private void CreateCatHtmlOf(Control container, XmlNode xmlNod)
        {
            var lc = new Literal();
            if (xmlNod.Attributes != null && (xmlNod.Attributes["xpath"] != null))
            {
                lc.Text = xmlNod.Attributes["xpath"].Value;
            }
            lc.DataBinding += CatHtmlOfDataBind;
            container.Controls.Add(lc);
        }

        private void CatHtmlOfDataBind(object sender, EventArgs e)
        {
            var lc = (Literal)sender;
            var container = (IDataItemContainer)lc.NamingContainer;
            try
            {
                lc.Visible = NBrightGlobal.IsVisible;
                var moduleId = DataBinder.Eval(container.DataItem, "ModuleId");
                var id = Convert.ToString(DataBinder.Eval(container.DataItem, "ItemId"));
                var lang = Convert.ToString(DataBinder.Eval(container.DataItem, "lang"));
                if (Utils.IsNumeric(id) && Utils.IsNumeric(moduleId))
                {
                    var grpCatCtrl = new GrpCatController(Utils.GetCurrentCulture());
                    var objCInfo = grpCatCtrl.GetCurrentCategoryInfo(PortalSettings.Current.PortalId, lc.Page.Request, Convert.ToInt32(id));
                    if (objCInfo != null)
                    {
                        lc.Text = objCInfo.GetXmlProperty(lc.Text);
                        lc.Text = System.Web.HttpUtility.HtmlDecode(lc.Text);
                    }
                    else
                    {
                        lc.Text = "";
                    }
                }

            }
            catch (Exception)
            {
                lc.Text = "";
            }
        }

        #endregion

        #region "Product Options"

        private void Createproductoptions(Control container, XmlNode xmlNod)
        {
            // create all 3 control possible
            var ddl = new DropDownList();
            var chk = new CheckBox();
            var txt = new TextBox();
            // pass wrapper templates using ddl attributes.
            if (xmlNod.Attributes != null && (xmlNod.Attributes["template"] != null))
            {
                ddl.Attributes.Add("template", xmlNod.Attributes["template"].Value);
                chk.Attributes.Add("template", xmlNod.Attributes["template"].Value);
                txt.Attributes.Add("template", xmlNod.Attributes["template"].Value);
            }
            if (xmlNod.Attributes != null && (xmlNod.Attributes["index"] != null))
            {
                if (Utils.IsNumeric(xmlNod.Attributes["index"].Value)) // must have a index
                {
                    ddl.Attributes.Add("index", xmlNod.Attributes["index"].Value);
                    ddl = (DropDownList) GenXmlFunctions.AssignByReflection(ddl, xmlNod);
                    ddl.DataBinding += ProductoptionsDataBind;
                    ddl.Visible = false;
                    ddl.Enabled = false;
                    ddl.ID = "optionddl" + xmlNod.Attributes["index"].Value;
                    if (xmlNod.Attributes != null && (xmlNod.Attributes["blank"] != null))
                    {
                        ddl.Attributes.Add("blank", xmlNod.Attributes["blank"].Value);
                    }
                    container.Controls.Add(ddl);
                    chk.Attributes.Add("index", xmlNod.Attributes["index"].Value);
                    chk = (CheckBox)GenXmlFunctions.AssignByReflection(chk, xmlNod);
                    chk.DataBinding += ProductoptionsDataBind;
                    chk.ID = "optionchk" + xmlNod.Attributes["index"].Value;
                    chk.Visible = false;
                    chk.Enabled = false;
                    container.Controls.Add(chk);
                    txt.Attributes.Add("index", xmlNod.Attributes["index"].Value);
                    txt = (TextBox)GenXmlFunctions.AssignByReflection(txt, xmlNod);
                    txt.DataBinding += ProductoptionsDataBind;
                    txt.ID = "optiontxt" + xmlNod.Attributes["index"].Value;
                    txt.Visible = false;
                    txt.Enabled = false;
                    container.Controls.Add(txt);
                    var hid = new HiddenField();
                    hid.DataBinding += ProductoptionsDataBind;
                    hid.ID = "optionid" + xmlNod.Attributes["index"].Value;
                    hid.Value = xmlNod.Attributes["index"].Value;
                    container.Controls.Add(hid);                    
                }
            }
        }

        private void ProductoptionsDataBind(object sender, EventArgs e)
        {
            if (NBrightGlobal.IsVisible)
            {
                #region "Init"

                var ctl = (Control) sender;
                var container = (IDataItemContainer) ctl.NamingContainer;
                var objInfo = (NBrightInfo) container.DataItem;
                var useCtrlType = "";
                var index = "1";
                DropDownList ddl = null;
                CheckBox chk = null;
                TextBox txt = null;
                HiddenField hid = null;

                if (ctl is HiddenField)
                {
                    hid = (HiddenField)ctl;
                    index = hid.Value;
                }

                if (ctl is DropDownList)
                {
                    ddl = (DropDownList) ctl;
                    index = ddl.Attributes["index"];
                    ddl.Attributes.Remove("index");
                }
                if (ctl is CheckBox)
                {
                    chk = (CheckBox) ctl;
                    index = chk.Attributes["index"];
                    chk.Attributes.Remove("index");
                }
                if (ctl is TextBox)
                {
                    txt = (TextBox)ctl;
                    index = txt.Attributes["index"];
                    txt.Attributes.Remove("index");
                }

                var optionid = "";
                var optiondesc = "";
                XmlNodeList nodList = null;
                var nod = objInfo.XMLDoc.SelectSingleNode("genxml/options/genxml[" + index + "]/hidden/optionid");
                if (nod != null)
                {
                    optionid = nod.InnerText;
                    if (hid != null) hid.Value = optionid;
                    var nodDesc = objInfo.XMLDoc.SelectSingleNode("genxml/lang/genxml/options/genxml[" + index + "]/textbox/txtoptiondesc");
                    if (nodDesc != null) optiondesc = nodDesc.InnerText;

                     nodList = objInfo.XMLDoc.SelectNodes("genxml/optionvalues[@optionid='" + optionid + "']/*");
                     if (nodList != null)
                     {
                         switch (nodList.Count)
                         {
                             case 0:
                                 useCtrlType = "TextBox";
                                 break;
                             case 1:
                                 useCtrlType = "CheckBox";
                                 break;
                             default:
                                 useCtrlType = "DropDownList";
                                 break;
                         }
                     }
                }

                #endregion

                if (ddl != null && useCtrlType == "DropDownList")
                {
                    try
                    {
                        ddl.Visible = true;
                        ddl.Enabled = true;
                        if (nodList != null)
                        {

                            if (ddl.Attributes["blank"] != null)
                            {
                                var li = new ListItem();
                                li.Text = ddl.Attributes["blank"];
                                li.Value = "0";
                                ddl.Items.Add(li);
                                ddl.Attributes.Remove("blank");
                            }

                            var lp = 1;
                            foreach (XmlNode nodOptVal in nodList)
                            {
                                var nodVal = nodOptVal.SelectSingleNode("hidden/optionvalueid");
                                if (nodVal != null)
                                {
                                    var optionvalueid = nodVal.InnerText;
                                    var li = new ListItem();
                                    var nodLang = objInfo.XMLDoc.SelectSingleNode("genxml/lang/genxml/optionvalues[@optionid='" + optionid + "']/genxml[" + lp.ToString("D") + "]/textbox/txtoptionvaluedesc");
                                    if (nodLang != null)
                                    {
                                        li.Text = nodLang.InnerText;
                                        li.Value = optionvalueid;
                                        if (li.Text != "") ddl.Items.Add(li);
                                    }
                                }
                                lp += 1;
                            }
                            if (nodList.Count > 0) ddl.SelectedIndex = 0;
                        }

                    }
                    catch (Exception)
                    {
                        ddl.Visible = false;
                    }
                }

                if (chk != null && useCtrlType == "CheckBox")
                {
                    try
                    {
                        chk.Visible = true;
                        chk.Enabled = true;
                        if (nodList != null)
                        {
                            var lp = 1;
                            foreach (XmlNode nodOptVal in nodList)
                            {
                                var nodVal = nodOptVal.SelectSingleNode("hidden/optionvalueid");
                                if (nodVal != null)
                                {
                                    var optionvalueid = nodVal.InnerText;
                                    var nodLang = objInfo.XMLDoc.SelectSingleNode("genxml/lang/genxml/optionvalues[@optionid='" + optionid + "']/genxml[" + lp.ToString("D") + "]/textbox/txtoptionvaluedesc");
                                    if (nodLang != null)
                                    {
                                        chk.Text = nodLang.InnerText;
                                        chk.Attributes.Add("optionvalueid",optionvalueid);
                                        chk.Attributes.Add("optionid", optionid);
                                    }
                                }
                                lp += 1;
                            }
                        }

                    }
                    catch (Exception)
                    {
                        chk.Visible = false;
                    }

                }

                if (txt != null && useCtrlType == "TextBox")
                {
                    txt.Visible = true;
                    txt.Enabled = true;
                    txt.Attributes.Add("optionid", optionid);
                    txt.Attributes.Add("optiondesc", optiondesc);
                }
            }
        }

        #endregion

        #region "Models"

        private void Createmodelslist(Control container, XmlNode xmlNod)
        {
            if (xmlNod.Attributes != null && (xmlNod.Attributes["template"] != null))
            {
                var templName = xmlNod.Attributes["template"].Value;
                var buyCtrl = new NBrightBuyController();
                var rpTempl = buyCtrl.GetTemplateData(-1, templName, Utils.GetCurrentCulture(), _settings, StoreSettings.Current.DebugMode);

                //remove templName from template, so we don't get a loop.
                if (rpTempl.Contains(templName)) rpTempl = rpTempl.Replace(templName, "");
                var rpt = new Repeater { ItemTemplate = new GenXmlTemplate(rpTempl, _settings) };
                rpt.Init += ModelslistInit; // use init so we don;t get a infinate loop on databind.
                container.Controls.Add(rpt);
            }
        }

        private void ModelslistInit(object sender, EventArgs e)
        {
            var rpt = (Repeater)sender;
            var container = (IDataItemContainer)rpt.NamingContainer;
            rpt.Visible = NBrightGlobal.IsVisible;
            if (rpt.Visible && container.DataItem != null)  // check for null dataitem, becuase we won't have it on postback.
            {
                //build models list
                var objL = BuildModelList((NBrightInfo)container.DataItem, true);
                rpt.DataSource = objL;
                rpt.DataBind();
            }
        }


        private void Createmodelsradio(Control container, XmlNode xmlNod)
        {
            var rbl = new RadioButtonList();
            if (xmlNod.Attributes != null && (xmlNod.Attributes["template"] != null))
            {
                rbl.Attributes.Add("template", xmlNod.Attributes["template"].Value);
            }
            if (xmlNod.Attributes != null && (xmlNod.Attributes["blank"] != null))
            {
                rbl.Attributes.Add("blank", xmlNod.Attributes["blank"].Value);
            }
            rbl = (RadioButtonList)GenXmlFunctions.AssignByReflection(rbl, xmlNod);
            rbl.DataBinding += ModelsradioDataBind;
            rbl.ID = "rblModelsel";
            container.Controls.Add(rbl);
        }

        private void ModelsradioDataBind(object sender, EventArgs e)
        {
            var rbl = (RadioButtonList)sender;
            var container = (IDataItemContainer)rbl.NamingContainer;
            try
            {
                rbl.Visible = NBrightGlobal.IsVisible;
                if (rbl.Visible)
                {
                    var templ = "{name} {price}";
                    if (rbl.Attributes["template"] != null)
                    {
                        templ = rbl.Attributes["template"];
                        rbl.Attributes.Remove("template");
                    }

                    var objL = BuildModelList((NBrightInfo) container.DataItem, true);

                    var displayPrice = HasDifferentPrices((NBrightInfo)container.DataItem);

                    if (rbl.Attributes["blank"] != null)
                    {
                        var li = new ListItem();
                        li.Text = rbl.Attributes["blank"];
                        li.Value = "0";
                        rbl.Items.Add(li);
                        rbl.Attributes.Remove("blank");
                    }

                    foreach (var obj in objL)
                    {
                        var li = new ListItem();
                        li.Text = GetItemDisplay(obj, templ, displayPrice);
                        li.Value = obj.GetXmlProperty("genxml/hidden/modelid");
                        if (li.Text != "") rbl.Items.Add(li);
                    }
                    if (rbl.Items.Count > 0) rbl.SelectedIndex = 0;
                }

            }
            catch (Exception)
            {
                rbl.Visible = false;
            }
        }

        private void Createmodelsdropdown(Control container, XmlNode xmlNod)
        {
            var rbl = new DropDownList();
            if (xmlNod.Attributes != null && (xmlNod.Attributes["template"] != null))
            {
                rbl.Attributes.Add("template", xmlNod.Attributes["template"].Value);
            }
            if (xmlNod.Attributes != null && (xmlNod.Attributes["blank"] != null))
            {
                rbl.Attributes.Add("blank", xmlNod.Attributes["blank"].Value);
            }
            rbl = (DropDownList)GenXmlFunctions.AssignByReflection(rbl, xmlNod);
            rbl.DataBinding += ModelsdropdownDataBind;
            rbl.ID = "ddlModelsel";
            container.Controls.Add(rbl);
        }

        private void ModelsdropdownDataBind(object sender, EventArgs e)
        {
            var ddl = (DropDownList)sender;
            var container = (IDataItemContainer)ddl.NamingContainer;
            try
            {
                ddl.Visible = NBrightGlobal.IsVisible;
                if (ddl.Visible)
                {
                    var templ = "{name} {price}";
                    if(ddl.Attributes["template"] != null)
                    {
                        templ = ddl.Attributes["template"];
                        ddl.Attributes.Remove("template");                        
                    }

                    var objL = BuildModelList((NBrightInfo)container.DataItem, true);

                    var displayPrice = HasDifferentPrices((NBrightInfo)container.DataItem);

                    if (ddl.Attributes["blank"] != null)
                    {                       
                        var li = new ListItem();
                        li.Text = ddl.Attributes["blank"];
                        li.Value = "0";
                        ddl.Items.Add(li);
                        ddl.Attributes.Remove("blank");
                    }

                    foreach (var obj in objL)
                    {
                        var li = new ListItem();
                        li.Text = GetItemDisplay(obj, templ, displayPrice);
                        li.Value = obj.GetXmlProperty("genxml/hidden/modelid");
                        if (li.Text != "") ddl.Items.Add(li);
                    }
                    if (ddl.Items.Count > 0) ddl.SelectedIndex = 0;
                }

            }
            catch (Exception)
            {
                ddl.Visible = false;
            }
        }

        private void Createmodeldefault(Control container, XmlNode xmlNod)
        {
            var hf = new HiddenField();
            hf.DataBinding += ModeldefaultDataBind;
            hf.ID = "modeldefault";
            container.Controls.Add(hf);
        }

        private void ModeldefaultDataBind(object sender, EventArgs e)
        {
            var hf = (HiddenField)sender;
            var container = (IDataItemContainer)hf.NamingContainer;
            try
            {
                hf.Visible = NBrightGlobal.IsVisible;
                var obj = (NBrightInfo)container.DataItem;
                if (obj != null) hf.Value = obj.GetXmlProperty("genxml/models/genxml[1]/hidden/modelid");
            }
            catch (Exception)
            {
                //do nothing
            }
        }


        private String GetItemDisplay(NBrightInfo obj, String templ, Boolean displayPrices)
        {
            var isDealer = CmsProviderManager.Default.IsInRole(_settings["dealer.role"]);
            var outText = templ;
            var stockOn = obj.GetXmlPropertyBool("genxml/checkbox/chkstockon");
            var stock = obj.GetXmlPropertyDouble("genxml/textbox/txtqtyremaining");
            if (stock > 0 | stockOn)
            {
                outText = outText.Replace("{ref}", obj.GetXmlProperty("genxml/textbox/txtmodelref"));
                outText = outText.Replace("{name}", obj.GetXmlProperty("genxml/lang/genxml/textbox/txtmodelname"));
                outText = outText.Replace("{stock}", stock.ToString("D"));

                if (displayPrices)
                {
                    //[TODO: add promotional calc]
                    //var price = obj.GetXmlPropertyDouble("genxml/hidden/saleprice");
                    var price = obj.GetXmlPropertyDouble("genxml/textbox/txtunitcost");
                    var strprice = NBrightBuyUtils.FormatToStoreCurrency(price);

                    var strdealerprice = "";
                    var dealerprice = obj.GetXmlPropertyDouble("genxml/textbox/txtdealercost");
                    if (isDealer)
                    {
                        strdealerprice = NBrightBuyUtils.FormatToStoreCurrency(dealerprice);
                        if (!outText.Contains("{dealerprice}") && (price > dealerprice)) strprice = strdealerprice;
                    }

                    outText = outText.Replace("{price}", "(" + strprice + ")");
                    outText = outText.Replace("{dealerprice}", strdealerprice);
                }
                else
                {
                    outText = outText.Replace("{price}", "");
                    outText = outText.Replace("{dealerprice}", "");
                }

                return outText;
            }
            return ""; // no stock so return empty string.
        }

        #endregion

        #region "orderstatus"

        private void Createorderstatusdropdown(Control container, XmlNode xmlNod)
        {
            var ddl = new DropDownList();
            if (xmlNod.Attributes != null && (xmlNod.Attributes["blank"] != null)) ddl.Attributes.Add("blank", xmlNod.Attributes["blank"].Value);
            if (xmlNod.Attributes != null && (xmlNod.Attributes["id"] != null))
                ddl.ID = xmlNod.Attributes["id"].InnerText;
            else
                ddl.ID = "orderstatus";

            ddl = (DropDownList)GenXmlFunctions.AssignByReflection(ddl, xmlNod);
            ddl.DataBinding += OrderstatusDataBind;
            container.Controls.Add(ddl);
        }

        private void OrderstatusDataBind(object sender, EventArgs e)
        {
            var ddl = (DropDownList)sender;
            var container = (IDataItemContainer)ddl.NamingContainer;
            try
            {
                ddl.Visible = NBrightGlobal.IsVisible;
                if (ddl.Visible)
                {

                    const string Resxpath = "/DesktopModules/NBright/NBrightBuy/App_LocalResources/General.ascx.resx";
                    var orderstatuscode = DnnUtils.GetLocalizedString("orderstatus.Code", Resxpath, Utils.GetCurrentCulture());
                    var orderstatustext = DnnUtils.GetLocalizedString("orderstatus.Text", Resxpath, Utils.GetCurrentCulture());
                    if (orderstatuscode != null && orderstatustext != null)
                    {
                        if (ddl.Attributes["blank"] != null)
                        {
                            orderstatuscode = "," + orderstatuscode;
                            orderstatustext = "," + orderstatustext;
                        }

                        var aryCode = orderstatuscode.Split(',');
                        var aryText = orderstatustext.Split(',');

                        var lp = 0;
                        foreach (var c in aryCode)
                        {
                            var li = new ListItem();
                            li.Text = aryText[lp];
                            li.Value = c;
                            if (li.Text != "")
                                ddl.Items.Add(li);
                            else
                            {
                                if (lp == 0) ddl.Items.Add(li); // allow the first entry to be blank.
                            }
                            lp += 1;
                        }
                        var strValue = GenXmlFunctions.GetGenXmlValue(ddl.ID, "dropdownlist", Convert.ToString(DataBinder.Eval(container.DataItem, _databindColumn)));
                        if ((ddl.Items.FindByValue(strValue) != null))
                            ddl.SelectedValue = strValue;
                        else if (aryCode.Length > 0) ddl.SelectedIndex = 0;
                    }
                }

            }
            catch (Exception)
            {
                ddl.Visible = false;
            }
        }


        #endregion

        #region "orderstatus"

        private void Createmodelstatusdropdown(Control container, XmlNode xmlNod)
        {
            var ddl = new DropDownList();
            if (xmlNod.Attributes != null && (xmlNod.Attributes["blank"] != null)) ddl.Attributes.Add("blank", xmlNod.Attributes["blank"].Value);
            if (xmlNod.Attributes != null && (xmlNod.Attributes["id"] != null))
                ddl.ID = xmlNod.Attributes["id"].InnerText;
            else
                ddl.ID = "modelstatus";

            ddl = (DropDownList)GenXmlFunctions.AssignByReflection(ddl, xmlNod);
            ddl.DataBinding += ModelstatusDataBind;
            container.Controls.Add(ddl);
        }

        private void ModelstatusDataBind(object sender, EventArgs e)
        {
            var ddl = (DropDownList)sender;
            var container = (IDataItemContainer)ddl.NamingContainer;
            try
            {
                ddl.Visible = NBrightGlobal.IsVisible;
                if (ddl.Visible)
                {

                    const string Resxpath = "/DesktopModules/NBright/NBrightBuy/App_LocalResources/General.ascx.resx";
                    var orderstatuscode = DnnUtils.GetLocalizedString("modelstatus.Code", Resxpath, Utils.GetCurrentCulture());
                    var orderstatustext = DnnUtils.GetLocalizedString("modelstatus.Text", Resxpath, Utils.GetCurrentCulture());
                    if (orderstatuscode != null && orderstatustext != null)
                    {
                        if (ddl.Attributes["blank"] != null)
                        {
                            orderstatuscode = "," + orderstatuscode;
                            orderstatustext = "," + orderstatustext;
                        }

                        var aryCode = orderstatuscode.Split(',');
                        var aryText = orderstatustext.Split(',');

                        var lp = 0;
                        foreach (var c in aryCode)
                        {
                            var li = new ListItem();
                            li.Text = aryText[lp];
                            li.Value = c;
                            if (li.Text != "")
                                ddl.Items.Add(li);
                            else
                            {
                                if (lp == 0) ddl.Items.Add(li); // allow the first entry to be blank.
                            }
                            lp += 1;
                        }
                        var strValue = GenXmlFunctions.GetGenXmlValue(ddl.ID, "dropdownlist", Convert.ToString(DataBinder.Eval(container.DataItem, _databindColumn)));
                        if ((ddl.Items.FindByValue(strValue) != null))
                            ddl.SelectedValue = strValue;
                        else if (aryCode.Length > 0) ddl.SelectedIndex = 0;
                    }
                }

            }
            catch (Exception)
            {
                ddl.Visible = false;
            }
        }


        #endregion

        #region "ProductCount"

        private void CreateProductCount(Control container, XmlNode xmlNod)
        {
            if (xmlNod.Attributes != null && (xmlNod.Attributes["modulekey"] != null))
            {
                var l = new Literal();
                l.DataBinding += ProductCountDataBind;
                l.Text = xmlNod.Attributes["modulekey"].Value;
                container.Controls.Add(l);
            }
        }

        private void ProductCountDataBind(object sender, EventArgs e)
        {
            var l = (Literal)sender;
            try
            {
                var navdata = new NavigationData(PortalSettings.Current.PortalId, l.Text);
                l.Text = navdata.RecordCount;
                l.Visible = NBrightGlobal.IsVisible;
            }
            catch (Exception ex)
            {
                l.Text = ex.ToString();
            }
        }


        #endregion

        #region "Docs"

        private void CreateProductDocLink(Control container, XmlNode xmlNod)
        {
            if (xmlNod.Attributes != null && (xmlNod.Attributes["index"] != null))
            {
                if (Utils.IsNumeric(xmlNod.Attributes["index"].Value)) // must have a index
                {
                    var cmd = new LinkButton();
                    cmd = (LinkButton)GenXmlFunctions.AssignByReflection(cmd, xmlNod);
                    cmd.Attributes.Add("index",xmlNod.Attributes["index"].Value);
                    cmd.DataBinding += ProductDocLinkDataBind;
                    container.Controls.Add(cmd);
                }
            }
        }

        private void ProductDocLinkDataBind(object sender, EventArgs e)
        {
            var cmd = (LinkButton)sender;
            var container = (IDataItemContainer)cmd.NamingContainer;
            try
            {
                cmd.Visible = NBrightGlobal.IsVisible;
                if (cmd.Visible)
                {
                    var index = cmd.Attributes["index"];
                    cmd.Attributes.Remove("index");

                    var objInfo = (NBrightInfo) container.DataItem;
                    cmd.CommandName = "docdownload";
                    if (cmd.Text == "")
                    {
                        var nodDesc = objInfo.XMLDoc.SelectSingleNode("genxml/lang/genxml/docs/genxml[" + index + "]/textbox/txtdocdesc");
                        if (nodDesc != null) cmd.Text = nodDesc.InnerText;
                    }
                    if (cmd.ToolTip == "")
                    {
                        var nodName = objInfo.XMLDoc.SelectSingleNode("genxml/docs/genxml[" + index + "]/textbox/txtfilename");
                        if (nodName != null) cmd.ToolTip = nodName.InnerText;
                    }
                    cmd.CommandArgument = objInfo.ItemID.ToString("D") + ":" + index;

                    cmd.Visible = true;
                    var nodPurchase = objInfo.XMLDoc.SelectSingleNode("genxml/docs/genxml[" + index + "]/checkbox/chkpurchase");
                    if (nodPurchase != null && nodPurchase.InnerText == "True")
                    {
                        //[TODO: work out purchase document logic]                        
                        //if (NBrightBuyV2Utils.DocIsPurchaseOnlyByDocId(Convert.ToInt32(nodDocId.InnerText)))
                        //{
                        //    cmd.Visible = false;
                        //    var role = "Manager";
                        //    if (!String.IsNullOrEmpty(_settings["manager.role"])) role = _settings["manager.role"];
                        //    var uInfo = UserController.GetCurrentUserInfo();
                        //    if (NBrightBuyV2Utils.DocHasBeenPurchasedByDocId(uInfo.UserID, Convert.ToInt32(nodDocId.InnerText)) || CmsProviderManager.Default.IsInRole(role)) cmd.Visible = true;
                        //}
                    }
                }

            }
            catch (Exception)
            {
                cmd.Visible = false;
            }
        }


        #endregion

        #region "Related Products"

        private void CreateRelatedlist(Control container, XmlNode xmlNod)
        {
            var lc = new Literal();
            if (xmlNod.Attributes != null && (xmlNod.Attributes["template"] != null))
            {
                lc.Text = xmlNod.Attributes["template"].Value;
            }
            lc.DataBinding += RelatedlistDataBind;
            container.Controls.Add(lc);
        }

        private void RelatedlistDataBind(object sender, EventArgs e)
        {
            var lc = (Literal)sender;
            var container = (IDataItemContainer)lc.NamingContainer;
            try
            {
                var strOut = "";
                lc.Visible = NBrightGlobal.IsVisible;
                if (lc.Visible)
                {

                    var moduleid = _settings["moduleid"];
                    var id = Convert.ToString(DataBinder.Eval(container.DataItem, "ItemId"));
                    var templName = lc.Text;
                    if (Utils.IsNumeric(id) && Utils.IsNumeric(moduleid) && (templName != ""))
                    {
                        var modCtrl = new NBrightBuyController();
                        var rpTempl = modCtrl.GetTemplateData(Convert.ToInt32(moduleid), templName, Utils.GetCurrentCulture(), _settings, StoreSettings.Current.DebugMode); 

                        //remove templName from template, so we don't get a loop.
                        if (rpTempl.Contains('"' + templName + '"')) rpTempl = rpTempl.Replace(templName, "");
                        //build list
                        var objInfo = (NBrightInfo)container.DataItem;

                        List<NBrightInfo> objL = null;
                        var strCacheKey = Utils.GetCurrentCulture() + "*" + objInfo.ItemID;
                        if (!StoreSettings.Current.DebugMode) objL = (List<NBrightInfo>)Utils.GetCache(strCacheKey);
                        if (objL == null)
                        {
                            var prodData = ProductUtils.GetProductData(objInfo.ItemID, Utils.GetCurrentCulture());
                            //objL = NBrightBuyV2Utils.GetRelatedProducts(objInfo);
                            objL = prodData.GetRelatedProducts();
                            if (!StoreSettings.Current.DebugMode) NBrightBuyUtils.SetModCache(Convert.ToInt32(moduleid), strCacheKey, objL);                            
                        }
                        // render repeater
                        try
                        {
                            strOut = GenXmlFunctions.RenderRepeater(objL, rpTempl, "", "XMLData", "", _settings);
                        }
                        catch (Exception exc)
                        {
                            strOut = "ERROR: NOTE: sub rendered templates CANNOT contain postback controls.<br/>" + exc;
                        }
                    }
                }
                lc.Text = strOut;

            }
            catch (Exception)
            {
                lc.Text = "";
            }
        }


        #endregion

        #region "Qty Field"

        private void CreateQtyField(Control container, XmlNode xmlNod)
        {
            var txt = new TextBox();
            txt = (TextBox)GenXmlFunctions.AssignByReflection(txt, xmlNod);
            txt.ID = "selectedaddqty";
            txt.DataBinding += QtyFieldDataBind;
            container.Controls.Add(txt);
        }

        private void QtyFieldDataBind(object sender, EventArgs e)
        {
            var txt = (TextBox)sender;
            txt.Visible = NBrightGlobal.IsVisible;
        }

        #endregion

        #region "Model Qty Field"

        private void CreateModelQtyField(Control container, XmlNode xmlNod)
        {
            var txt = new TextBox();
            txt = (TextBox)GenXmlFunctions.AssignByReflection(txt, xmlNod);
            txt.ID = "selectedmodelqty";
            txt.DataBinding += ModelQtyFieldDataBind;
            container.Controls.Add(txt);
            var hid = new HiddenField();
            hid.ID = "modelid";
            hid.DataBinding += ModelHiddenFieldDataBind;
            container.Controls.Add(hid);
            
        }

        private void ModelHiddenFieldDataBind(object sender, EventArgs e)
        {
            var txt = (HiddenField)sender;
            var container = (IDataItemContainer)txt.NamingContainer;
            var strXml = DataBinder.Eval(container.DataItem, _databindColumn).ToString();
            var nbi = new NBrightInfo();
            nbi.XMLData = strXml;
            txt.Value = nbi.GetXmlProperty("genxml/hidden/modelid");
            txt.Visible = NBrightGlobal.IsVisible;
        }

        private void ModelQtyFieldDataBind(object sender, EventArgs e)
        {
            var txt = (TextBox)sender;
            var container = (IDataItemContainer)txt.NamingContainer;
            var strXml = DataBinder.Eval(container.DataItem, _databindColumn).ToString();
            var nbi = new NBrightInfo();
            nbi.XMLData = strXml;
            if (nbi.GetXmlProperty("genxml/hidden/modelid") == "") txt.Text = "ERR! - MODELQTY can only be used on modellist template";
            txt.Attributes.Add("modelid", nbi.GetXmlProperty("genxml/hidden/modelid"));
            txt.Visible = NBrightGlobal.IsVisible;
        }

        #endregion

        #region "create EditLink control"

        private void CreateEditLink(Control container, XmlNode xmlNod)
        {
            var lk = new HyperLink();
            lk = (HyperLink)GenXmlFunctions.AssignByReflection(lk, xmlNod);

            // if we are using xsl then we might not have a databind ItemId (if the xsl is in the header loop).  So pass it in here, via the xsl, so we can use it in the link. 
            if (xmlNod.Attributes != null && (xmlNod.Attributes["itemid"] != null))
            {
                lk.NavigateUrl = xmlNod.Attributes["itemid"].InnerXml;
            }
            else
            {
                lk.NavigateUrl = "";
            }

            lk.DataBinding += EditLinkDataBinding;
            container.Controls.Add(lk);
        }

        private void EditLinkDataBinding(object sender, EventArgs e)
        {
            var lk = (HyperLink)sender;
            var container = (IDataItemContainer)lk.NamingContainer;
            try
            {
                lk.Visible = NBrightGlobal.IsVisible;

                var entryid = Convert.ToString(DataBinder.Eval(container.DataItem, "ItemID"));

                if (lk.NavigateUrl != "") entryid = lk.NavigateUrl; // use the itemid passed in (XSL loop in display header)

                var url = "Unable to find BackOffice Setting, go into Back Office settings and save.";
                if (Utils.IsNumeric(entryid) && StoreSettings.Current.GetInt("backofficetabid") > 0)
                {
                    var paramlist = new string[4];
                    paramlist[1] = "eid=" + entryid;
                    paramlist[2] = "ctrl=products";
                    if (_settings != null && _settings.ContainsKey("currenttabid")) paramlist[3] = "rtntab=" + _settings["currenttabid"];
                    if (_settings != null && _settings.ContainsKey("moduleid")) paramlist[3] = "rtnmid=" + _settings["moduleid"];
                    var urlpage = Utils.RequestParam(HttpContext.Current, "page");
                    if (urlpage.Trim() != "")
                    {
                        IncreaseArray(ref paramlist, 1);
                        paramlist[paramlist.Length - 1] = "PageIndex=" + urlpage.Trim();
                    }
                    var urlcatid = Utils.RequestParam(HttpContext.Current, "catid");
                    if (urlcatid.Trim() != "")
                    {
                        IncreaseArray(ref paramlist, 1);
                        paramlist[paramlist.Length - 1] = "catid=" + urlcatid.Trim();
                    }
                    url = Globals.NavigateURL(StoreSettings.Current.GetInt("backofficetabid"), "", paramlist);                    
                }
                lk.NavigateUrl = url;
            }
            catch (Exception ex)
            {
                lk.Text = ex.ToString();
            }
        }

        #endregion

        #region "Sale Price"

        private void CreateSalePrice(Control container, XmlNode xmlNod)
        {
            var l = new Literal();
            l.DataBinding += SalePriceDataBinding;
            l.Text = "";
            container.Controls.Add(l);
        }

        private void SalePriceDataBinding(object sender, EventArgs e)
        {
            var l = (Literal)sender;
            var container = (IDataItemContainer)l.NamingContainer;
            try
            {
                l.Text = "";
                l.Visible = NBrightGlobal.IsVisible;
                var sp = GetSalePrice((NBrightInfo)container.DataItem);
                if (Utils.IsNumeric(sp))
                {
                    Double v = -1;
                    if (Utils.IsNumeric(XmlConvert.DecodeName(sp)))
                    {
                        v = Convert.ToDouble(XmlConvert.DecodeName(sp), CultureInfo.GetCultureInfo("en-US"));
                    }
                    if (v >= 0) l.Text = NBrightBuyUtils.FormatToStoreCurrency(v);
                }
            }
            catch (Exception ex)
            {
                l.Text = ex.ToString();
            }
        }


        #endregion

        #region "Dealer Price"

        private void CreateDealerPrice(Control container, XmlNode xmlNod)
        {
            var l = new Literal();
            l.DataBinding += DealerPriceDataBinding;
            l.Text = "";
            container.Controls.Add(l);
        }

        private void DealerPriceDataBinding(object sender, EventArgs e)
        {
            var l = (Literal)sender;
            var container = (IDataItemContainer)l.NamingContainer;
            try
            {
                l.Text = "";
                l.Visible = NBrightGlobal.IsVisible;
                var sp = GetDealerPrice((NBrightInfo)container.DataItem);
                if (Utils.IsNumeric(sp))
                {
                    Double v = -1;
                    if (Utils.IsNumeric(XmlConvert.DecodeName(sp)))
                    {
                        v = Convert.ToDouble(XmlConvert.DecodeName(sp), CultureInfo.GetCultureInfo("en-US"));
                    }
                    if (v >= 0) l.Text = NBrightBuyUtils.FormatToStoreCurrency(v);
                }
            }
            catch (Exception ex)
            {
                l.Text = ex.ToString();
            }
        }


        #endregion

        #region "CreateCurrencyIsoCode"

        private void CreateCurrencyIsoCode(Control container, XmlNode xmlNod)
        {
            var l = new Literal();
            l.DataBinding += CreateCurrencyIsoCodeDataBinding;
            l.Text = "";
            container.Controls.Add(l);
        }

        private void CreateCurrencyIsoCodeDataBinding(object sender, EventArgs e)
        {
            var l = (Literal)sender;
            var container = (IDataItemContainer)l.NamingContainer;
            try
            {
                l.Text = "";
                l.Visible = NBrightGlobal.IsVisible;
                l.Text = NBrightBuyUtils.GetCurrencyIsoCode();
            }
            catch (Exception ex)
            {
                l.Text = ex.ToString();
            }
        }


        #endregion

        #region "From Price"

        private void CreateFromPrice(Control container, XmlNode xmlNod)
        {
            var l = new Literal();
            l.DataBinding += FromPriceDataBinding;
            l.Text = "";
            container.Controls.Add(l);
        }

        private void FromPriceDataBinding(object sender, EventArgs e)
        {
            var l = (Literal)sender;
            var container = (IDataItemContainer)l.NamingContainer;
            try
            {
                l.Text = "";
                l.Visible = NBrightGlobal.IsVisible;
                var sp = GetFromPrice((NBrightInfo)container.DataItem);
                if (Utils.IsNumeric(sp))
                {
                    Double v = -1;
                    if (Utils.IsNumeric(XmlConvert.DecodeName(sp)))
                    {
                        v = Convert.ToDouble(XmlConvert.DecodeName(sp), CultureInfo.GetCultureInfo("en-US"));
                    }
                    if (v >= 0) l.Text = NBrightBuyUtils.FormatToStoreCurrency(v);
                }
            }
            catch (Exception ex)
            {
                l.Text = ex.ToString();
            }
        }


        #endregion

        #region "Best Price"

        private void CreateBestPrice(Control container, XmlNode xmlNod)
        {
            var l = new Literal();
            l.DataBinding += BestPriceDataBinding;
            l.Text = "";
            container.Controls.Add(l);
        }

        private void BestPriceDataBinding(object sender, EventArgs e)
        {
            var l = (Literal)sender;
            var container = (IDataItemContainer)l.NamingContainer;
            try
            {
                l.Text = "";
                l.Visible = NBrightGlobal.IsVisible;
                var sp = GetBestPrice((NBrightInfo)container.DataItem);
                if (Utils.IsNumeric(sp))
                {
                    Double v = -1;
                    if (Utils.IsNumeric(XmlConvert.DecodeName(sp)))
                    {
                        v = Convert.ToDouble(XmlConvert.DecodeName(sp), CultureInfo.GetCultureInfo("en-US"));
                    }
                    if (v >= 0) l.Text = NBrightBuyUtils.FormatToStoreCurrency(v);
                }
            }
            catch (Exception ex)
            {
                l.Text = ex.ToString();
            }
        }


        #endregion

        #region "ItemList (WishList)"

        private void CreateItemListCount(Control container, XmlNode xmlNod)
        {
            if (_settings != null)
            {
                var l = new Literal();

                var listName = "ItemList";
                if (xmlNod.Attributes != null && (xmlNod.Attributes["listname"] != null))
                {
                    listName = xmlNod.Attributes["listname"].InnerText;
                }
                var il = new ItemListData(-1, StoreSettings.Current.StorageTypeClient, listName);
                l.Text = il.ItemCount;
                if (l.Text == "") l.Text = "0";
                l.Text = "<span class='nbxItemListCount'>" + l.Text + "</span>";
                container.Controls.Add(l);
            }
        }

        private void CreateItemListField(Control container, XmlNode xmlNod)
        {
            if (_settings != null)
            {
                var h = new HiddenField();

                var listName = "ItemList";
                if (xmlNod.Attributes != null && (xmlNod.Attributes["listname"] != null))
                {
                    listName = xmlNod.Attributes["listname"].InnerText;
                }
                var il = new ItemListData(-1, StoreSettings.Current.StorageTypeClient, listName);
                h.Value = il.ItemList;
                h.ID = "nbxItemList" + listName;
                container.Controls.Add(h);
                // add required JS
                var l = new Literal();
                l.Text = "<script>$(document).ready(function() {nbxbuttonview('input[id*=\"nbxItemList" + listName + "\"]');});</script>";
                container.Controls.Add(l);
            }
        }

        private void CreateItemListLink(Control container, XmlNode xmlNod, String action)
        {
            var lk = new HyperLink();
            lk = (HyperLink)GenXmlFunctions.AssignByReflection(lk, xmlNod);

            if (xmlNod.Attributes != null && (xmlNod.Attributes["class"] != null))
            {
                lk.CssClass = xmlNod.Attributes["class"].InnerXml;
            }

            var listName = "ItemList";
            if (xmlNod.Attributes != null && (xmlNod.Attributes["listname"] != null))
            {
                lk.Attributes.Add("listName", xmlNod.Attributes["listname"].InnerText);
            }

            lk.Attributes.Add("action", action);

            lk.DataBinding += ItemListDataBinding;
            container.Controls.Add(lk);

            var l = new Literal();
            l.Text = action;
            l.DataBinding += ItemListScriptBinding;
            container.Controls.Add(l);

        }

        private void ItemListDataBinding(object sender, EventArgs e)
        {
            var lk = (HyperLink)sender;
            var container = (IDataItemContainer)lk.NamingContainer;
            try
            {
                lk.Visible = NBrightGlobal.IsVisible;

                var entryid = Convert.ToString(DataBinder.Eval(container.DataItem, "ItemID"));
                var moduleId = Convert.ToString(DataBinder.Eval(container.DataItem, "ModuleId"));
                if (Utils.IsNumeric(moduleId))
                {
                    var listname = "ItemList";
                    if (lk.Attributes["listname"] != null) listname = lk.Attributes["listname"];
                    var cmd = "";
                    if (lk.Attributes["action"] == "add")
                    {
                        cmd = "/DesktopModules/NBright/NBrightBuy/XmlConnector.ashx?cmd=additemlist&itemid=" + entryid + "&listname=" + listname;
                        lk.ID = "nbxItemListAdd" + entryid;
                        lk.Target = entryid;
                    }
                    if (lk.Attributes["action"] == "remove")
                    {
                        cmd = "/DesktopModules/NBright/NBrightBuy/XmlConnector.ashx?cmd=removeitemlist&itemid=" + entryid + "&listname=" + listname;
                        lk.ID = "nbxItemListRemove" + entryid;
                        lk.Target = entryid;
                    }
                    if (lk.Attributes["action"] == "delete")
                    {
                        lk.ID = "nbxItemListDelete";
                        cmd = "/DesktopModules/NBright/NBrightBuy/XmlConnector.ashx?cmd=deleteitemlist&listname=" + listname;
                    }

                    lk.Attributes.Add("cmd", cmd);

                }

            }
            catch (Exception ex)
            {
                lk.Text = ex.ToString();
            }
        }

        private void ItemListScriptBinding(object sender, EventArgs e)
        {
            var l = (Literal)sender;
            var container = (IDataItemContainer)l.NamingContainer;
            try
            {
                l.Visible = NBrightGlobal.IsVisible;
                var entryid = Convert.ToString(DataBinder.Eval(container.DataItem, "ItemID"));
                if (l.Text == "add") l.Text = "<script>nbxajaxaction('a[id*=\"nbxItemListAdd" + entryid + "\"]');</script>";
                if (l.Text == "remove") l.Text = "<script>nbxajaxaction('a[id*=\"nbxItemListRemove" + entryid + "\"]');</script>";
                if (l.Text == "delete") l.Text = "<script>nbxajaxaction('a[id*=\"nbxItemListDelete\"]');</script>";

            }
            catch (Exception ex)
            {
                l.Text = ex.ToString();
            }
        }

        #endregion

        #region "CartQtyTextbox"

        private void CreateCartQtyTextbox(Control container, XmlNode xmlNod)
        {
            var txt = new TextBox { Text = "" };

            txt = (TextBox)GenXmlFunctions.AssignByReflection(txt, xmlNod);

            if (xmlNod.Attributes != null && (xmlNod.Attributes["text"] != null))
            {
                txt.Text = xmlNod.Attributes["text"].InnerXml;
            }

            txt.DataBinding += CartQtyTextDataBinding;
            container.Controls.Add(txt);
        }

        private void CartQtyTextDataBinding(object sender, EventArgs e)
        {
            var txt = (TextBox)sender;
            var container = (IDataItemContainer)txt.NamingContainer;

            try
            {
                txt.Visible = NBrightGlobal.IsVisible;
                if (txt.Width == 0) txt.Visible = false; // always hide if we have a width of zero.
                else
                {
                    var strXML = Convert.ToString(DataBinder.Eval(container.DataItem,"XMLData" ));
                    var nbInfo = new NBrightInfo();
                    nbInfo.XMLData = strXML;
                    txt.Text = nbInfo.GetXmlProperty("genxml/qty");
                }
            }
            catch (Exception)
            {
                //do nothing
            }
        }


        #endregion

        #region "CartEmailAddress"

        private void CreateCartEmailAddress(Control container, XmlNode xmlNod)
        {
            var txt = new TextBox { Text = "" };

            txt = (TextBox)GenXmlFunctions.AssignByReflection(txt, xmlNod);
            txt.ID = "cartemailaddress";
            if (xmlNod.Attributes != null && (xmlNod.Attributes["text"] != null))
            {
                txt.Text = xmlNod.Attributes["text"].InnerXml;
            }

            txt.DataBinding += CartEmailAddressDataBinding;
            container.Controls.Add(txt);
        }

        private void CartEmailAddressDataBinding(object sender, EventArgs e)
        {
            var txt = (TextBox)sender;
            var container = (IDataItemContainer)txt.NamingContainer;

            try
            {
                txt.Visible = NBrightGlobal.IsVisible;
                if (txt.Width == 0) txt.Visible = false; // always hide if we have a width of zero.
                else
                {
                    var strXML = Convert.ToString(DataBinder.Eval(container.DataItem, "XMLData"));
                    var nbInfo = new NBrightInfo();
                    nbInfo.XMLData = strXML;
                    txt.Text = nbInfo.GetXmlProperty("genxml/textbox/emailaddress");
                    if (txt.Text == "")
                    {
                        var usr = UserController.GetCurrentUserInfo();
                        if (usr != null && usr.UserID > 0) txt.Text = usr.Email;
                    }
                }
            }
            catch (Exception)
            {
                //do nothing
            }
        }


        #endregion

        #region "groups"


        private void Creategroupdropdown(Control container, XmlNode xmlNod)
        {
            var rbl = new DropDownList();
            if (xmlNod.Attributes != null && (xmlNod.Attributes["blank"] != null))
            {
                rbl.Attributes.Add("blank", xmlNod.Attributes["blank"].Value);
            }
            if (xmlNod.Attributes != null && (xmlNod.Attributes["groupsonly"] != null))
            {
                rbl.Attributes.Add("groupsonly", xmlNod.Attributes["groupsonly"].Value);
            }
            rbl = (DropDownList)GenXmlFunctions.AssignByReflection(rbl, xmlNod);
            rbl.DataBinding += GroupdropdownDataBind;
            if (xmlNod.Attributes != null && (xmlNod.Attributes["id"] != null))
                rbl.ID = xmlNod.Attributes["id"].InnerText;
            else
                rbl.ID = "ddlGroupsel";
            container.Controls.Add(rbl);
        }

        private void GroupdropdownDataBind(object sender, EventArgs e)
        {
            var ddl = (DropDownList)sender;
            var container = (IDataItemContainer)ddl.NamingContainer;
            try
            {
                ddl.Visible = NBrightGlobal.IsVisible;
                if (ddl.Visible)
                {

                    var objL = NBrightBuyUtils.GetCategoryGroups(Utils.GetCurrentCulture(),true);

                    if (ddl.Attributes["blank"] != null)
                    {
                        var li = new ListItem();
                        li.Text = ddl.Attributes["blank"];
                        li.Value = "0";
                        ddl.Items.Add(li);
                        ddl.Attributes.Remove("blank");
                    }

                    var gref = "";
                    if (ddl.Attributes["groupsonly"] != null) gref = "cat";

                    foreach (var obj in objL)
                    {
                        if (obj.GetXmlProperty("genxml/textbox/groupref") != gref)
                        {
                            var li = new ListItem();
                            li.Text = obj.GetXmlProperty("genxml/lang/genxml/textbox/groupname");
                            li.Value = obj.GetXmlProperty("genxml/textbox/groupref");
                            if (li.Text != "") ddl.Items.Add(li);
                        }
                    }
                    var strValue = GenXmlFunctions.GetGenXmlValue(ddl.ID, "dropdownlist", Convert.ToString(DataBinder.Eval(container.DataItem, _databindColumn)));
                    if ((ddl.Items.FindByValue(strValue) != null)) ddl.SelectedValue = strValue;
                }

            }
            catch (Exception)
            {
                ddl.Visible = false;
            }
        }



        #endregion

        #region "create Image control"

        private void CreateImage(Control container, XmlNode xmlNod)
        {
            var img = new Image();

            img = (Image)GenXmlFunctions.AssignByReflection(img, xmlNod);
            if (xmlNod.Attributes != null && (xmlNod.Attributes["xpath"] != null))
            {
                img.ImageUrl = xmlNod.Attributes["xpath"].InnerXml; // use imageurl to get the xpath of the image
            }
            if (xmlNod.Attributes != null && (xmlNod.Attributes["thumb"] != null))
            {
                img.Attributes.Add("thumb", xmlNod.Attributes["thumb"].InnerXml);
            }

            img.DataBinding += ImageDataBinding;
            container.Controls.Add(img);
        }

        private void ImageDataBinding(object sender, EventArgs e)
        {
            var img = (Image)sender;
            var container = (IDataItemContainer)img.NamingContainer;
            try
            {
                img.Visible = NBrightGlobal.IsVisible;
                var src = "";

                XmlNode nod = GenXmlFunctions.GetGenXmLnode(DataBinder.Eval(container.DataItem, _databindColumn).ToString(), img.ImageUrl);
                if ((nod != null))
                {
                    src += nod.InnerText;
                }

                var altpath = img.ImageUrl.Replace("genxml/hidden/hid", "genxml/textbox/txt");
                nod = GenXmlFunctions.GetGenXmLnode(DataBinder.Eval(container.DataItem, _databindColumn).ToString(), altpath);
                if ((nod != null))
                {
                    img.AlternateText = nod.InnerText;
                }

                if (img.Attributes["thumb"] == null || img.Attributes["thumb"] == "")
                {
                    img.ImageUrl = src;
                }
                else
                {
                    var w = ImgUtils.GetThumbWidth(img.Attributes["thumb"]).ToString("");
                    var h = ImgUtils.GetThumbHeight(img.Attributes["thumb"]).ToString("");
                    if (w == "-1") w = "0";
                    if (h == "-1") h = "0";
                    img.Attributes.Remove("thumb");
                    img.ImageUrl = "/DesktopModules/NBright/NBrightBuy/NBrightThumb.ashx?w=" + w + "&h=" + h + "&src=" + src;
                }

            }
            catch (Exception ex)
            {
                // no error
            }
        }

        #endregion

        #region "Country and culture"

       
        private void Createculturecodedropdown(Control container, XmlNode xmlNod)
        {
            var rbl = new DropDownList();
            if (xmlNod.Attributes != null && (xmlNod.Attributes["blank"] != null))
            {
                rbl.Attributes.Add("blank", xmlNod.Attributes["blank"].Value);
            }
            rbl = (DropDownList) GenXmlFunctions.AssignByReflection(rbl, xmlNod);
            rbl.DataBinding += CultureCodeDropdownDataBind;
            if (xmlNod.Attributes != null && (xmlNod.Attributes["id"] != null))
            {
                rbl.ID = xmlNod.Attributes["id"].InnerText;
                container.Controls.Add(rbl);
            }
        }

        private void CultureCodeDropdownDataBind(object sender, EventArgs e)
        {
            

            var ddl = (DropDownList)sender;
            var container = (IDataItemContainer)ddl.NamingContainer;
            try
            {
                ddl.Visible = NBrightGlobal.IsVisible;
                if (ddl.Visible)
                {

                    //var countries = CultureInfo.GetCultures(CultureTypes.AllCultures).Except(CultureInfo.GetCultures(CultureTypes.SpecificCultures));
                    var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
                    var dnnCultureCode = DnnUtils.GetCountryCodeList();

                    var joinItems = (from d1 in cultures where dnnCultureCode.ContainsKey(d1.Name.Split('-').Last()) select d1).ToList<CultureInfo>();


                    if (ddl.Attributes["blank"] != null)
                    {
                        var li = new ListItem();
                        li.Text = ddl.Attributes["blank"];
                        li.Value = "0";
                        ddl.Items.Add(li);
                        ddl.Attributes.Remove("blank");
                    }

                    foreach (var obj in joinItems)
                    {
                        var li = new ListItem();
                        li.Text = obj.DisplayName + " : " + obj.Name;
                        li.Value = obj.Name;
                        ddl.Items.Add(li);
                    }
                    var strValue = GenXmlFunctions.GetGenXmlValue(ddl.ID, "dropdownlist", Convert.ToString(DataBinder.Eval(container.DataItem, _databindColumn)));
                    if (strValue == "") strValue = Utils.GetCurrentCulture();
                    if ((ddl.Items.FindByValue(strValue) != null)) ddl.SelectedValue = strValue;
                }

            }
            catch (Exception)
            {
                ddl.Visible = false;
            }
        }


        private void CreateCountryDropDownList(Control container, XmlNode xmlNod)
        {
            var rbl = new DropDownList();
            if (xmlNod.Attributes != null && (xmlNod.Attributes["blank"] != null))
            {
                rbl.Attributes.Add("blank", xmlNod.Attributes["blank"].Value);
            }
            rbl = (DropDownList)GenXmlFunctions.AssignByReflection(rbl, xmlNod);
            rbl.DataBinding += CountryCodeDropdownDataBind;
            if (xmlNod.Attributes != null && (xmlNod.Attributes["id"] != null))
            {
                rbl.ID = xmlNod.Attributes["id"].InnerText;
                container.Controls.Add(rbl);
            }
        }

        private void CountryCodeDropdownDataBind(object sender, EventArgs e)
        {

            var ddl = (DropDownList)sender;
            var container = (IDataItemContainer)ddl.NamingContainer;
            try
            {
                ddl.Visible = NBrightGlobal.IsVisible;
                if (ddl.Visible)
                {

                    if (ddl.Attributes["blank"] != null)
                    {
                        var li = new ListItem();
                        li.Text = ddl.Attributes["blank"];
                        li.Value = "0";
                        ddl.Items.Add(li);
                        ddl.Attributes.Remove("blank");
                    }
                    
                    var tList = NBrightBuyUtils.GetCountryList();
                    foreach (var tItem in tList)
                    {
                        var li = new ListItem();
                        li.Text = tItem.Value;
                        li.Value = tItem.Key;
                        ddl.Items.Add(li);
                    }

                    var strValue = GenXmlFunctions.GetGenXmlValue(ddl.ID, "dropdownlist", Convert.ToString(DataBinder.Eval(container.DataItem, _databindColumn)));
                    if (strValue == "") strValue = Utils.GetCurrentCulture();
                    if ((ddl.Items.FindByValue(strValue) != null)) ddl.SelectedValue = strValue;

                }

            }
            catch (Exception)
            {
                ddl.Visible = false;
            }
        }

        private void CreateEditFlag(Control container, XmlNode xmlNod)
        {
            var lc = new Literal();
            var size = "16";
            if (xmlNod.Attributes != null && (xmlNod.Attributes["size"] != null)) size = xmlNod.Attributes["size"].Value;
            lc.Text = "<img src='/DesktopModules/NBright/NBrightBuy/Themes/config/img/flags/" + size + "/" + StoreSettings.Current.EditLanguage + ".png' />";
            lc.DataBinding += EditFlagDataBind;
            container.Controls.Add(lc);
        }
        private void EditFlagDataBind(object sender, EventArgs e)
        {
            var lc = (Literal)sender;
            lc.Visible = NBrightGlobal.IsVisible;
        }

        private void CreateSelectLangaugeButton(Control container, XmlNode xmlNod)
        {
            var cmd = new EditLanguage();
            cmd = (EditLanguage)GenXmlFunctions.AssignByReflection(cmd, xmlNod);
            cmd.DataBinding += SelectLangaugeDataBind;
            container.Controls.Add(cmd);
        }
        private void SelectLangaugeDataBind(object sender, EventArgs e)
        {
            var lc = (EditLanguage)sender;
            lc.Visible = NBrightGlobal.IsVisible;
        }


        #endregion


        #region "Addressdropdown"

        private void CreateAddressDropDownList(Control container, XmlNode xmlNod)
        {
            var rbl = new DropDownList();
            if (xmlNod.Attributes != null && (xmlNod.Attributes["blank"] != null))
            {
                rbl.Attributes.Add("blank", xmlNod.Attributes["blank"].Value);
            }
            if (xmlNod.Attributes != null && (xmlNod.Attributes["template"] != null))
            {
                rbl.Attributes.Add("template", xmlNod.Attributes["template"].Value);
            }
            if (xmlNod.Attributes != null && (xmlNod.Attributes["data"] != null))
            {
                rbl.Attributes.Add("data", xmlNod.Attributes["data"].Value);
            }
            if (xmlNod.Attributes != null && (xmlNod.Attributes["datavalue"] != null))
            {
                rbl.Attributes.Add("datavalue", xmlNod.Attributes["datavalue"].Value);
            }
            if (xmlNod.Attributes != null && (xmlNod.Attributes["datavalue"] != null))
            {
                rbl.Attributes.Add("formselector", xmlNod.Attributes["formselector"].Value);
            }

            rbl = (DropDownList)GenXmlFunctions.AssignByReflection(rbl, xmlNod);
            rbl.DataBinding += AddressDropdownDataBind;
            if (xmlNod.Attributes != null && (xmlNod.Attributes["id"] != null))
            {
                rbl.ID = xmlNod.Attributes["id"].InnerText;
                container.Controls.Add(rbl);
            }
        }

        private void AddressDropdownDataBind(object sender, EventArgs e)
        {

            var ddl = (DropDownList)sender;
            var container = (IDataItemContainer)ddl.NamingContainer;
            try
            {
                ddl.Visible = NBrightGlobal.IsVisible;
                if (ddl.Visible)
                {

                    var usr = UserController.GetCurrentUserInfo();
                    var addressData = new AddressData(usr.UserID.ToString(""));

                    if (ddl.Attributes["blank"] != null)
                    {
                        var li = new ListItem();
                        li.Text = ddl.Attributes["blank"];
                        li.Value = "-1";
                        ddl.Items.Add(li);
                        ddl.Attributes.Remove("blank");
                    }

                    var addrlist = addressData.GetAddressList();
                    foreach (var tItem in addrlist)
                    {
                        var itemtext = tItem.GetXmlProperty("genxml/textbox/firstname") + "," + tItem.GetXmlProperty("genxml/textbox/lastname") + "," + tItem.GetXmlProperty("genxml/textbox/unit") + "," + tItem.GetXmlProperty("genxml/textbox/street") + "," + tItem.GetXmlProperty("genxml/textbox/city");
                        if (ddl.Attributes["template"] != null)
                        {
                            itemtext = "";
                            var xpathList = ddl.Attributes["template"].Split(',');
                            foreach (var xp in xpathList)
                            {
                                itemtext += "," + tItem.GetXmlProperty(xp);
                            }
                        }
                        var datatext = "";
                        if (ddl.Attributes["data"] != null)
                        {
                            var xpathList = ddl.Attributes["data"].Split(',');
                            foreach (var xp in xpathList)
                            {
                                datatext += "," + tItem.GetXmlProperty(xp).Replace(","," ");
                            }
                        }
                        var datavalue = "";
                        if (ddl.Attributes["datavalue"] != null) datavalue += ddl.Attributes["datavalue"];

                        var idx = tItem.GetXmlProperty("genxml/hidden/index");
                        if (ddl.Items.FindByValue(idx) == null)
                        {
                            var li = new ListItem();
                            li.Text = itemtext.TrimStart(',');
                            li.Value = idx;
                            li.Attributes.Add("data", datatext.TrimStart(','));
                            li.Attributes.Add("datavalue", datavalue);
                            ddl.Items.Add(li);                            
                        }
                    }

                }

            }
            catch (Exception)
            {
                ddl.Visible = false;
            }
        }



#endregion

        #region "Order Itemlist"

        private void CreateOrderItemlist(Control container, XmlNode xmlNod)
        {
            var lc = new Literal();
            if (xmlNod.Attributes != null && (xmlNod.Attributes["template"] != null))
            {
                lc.Text = xmlNod.Attributes["template"].Value;
            }
            lc.DataBinding += OrderItemlistDataBind;
            container.Controls.Add(lc);
        }

        private void OrderItemlistDataBind(object sender, EventArgs e)
        {
            var lc = (Literal)sender;
            var container = (IDataItemContainer)lc.NamingContainer;
            try
            {
                var strOut = "";
                lc.Visible = NBrightGlobal.IsVisible;
                if (lc.Visible)
                {

                    var id = Convert.ToString(DataBinder.Eval(container.DataItem, "ItemId"));
                    var lang = Convert.ToString(DataBinder.Eval(container.DataItem, "lang"));
                    if (lang == "") lang = Utils.GetCurrentCulture();
                    var templName = lc.Text;
                    if (Utils.IsNumeric(id) && (templName != ""))
                    {
                        var buyCtrl = new NBrightBuyController();
                        var rpTempl = buyCtrl.GetTemplateData(-1, templName, lang, _settings, StoreSettings.Current.DebugMode);

                        //remove templName from template, so we don't get a loop.
                        if (rpTempl.Contains(templName)) rpTempl = rpTempl.Replace(templName, "");
                        //build models list

                        var objInfo = (NBrightInfo)container.DataItem;
                        var ordData = new OrderData(objInfo.PortalId,objInfo.ItemID);
                        // render repeater
                        try
                        {
                            strOut = GenXmlFunctions.RenderRepeater(ordData.GetCartItemList(), rpTempl, "", "XMLData", "", _settings);
                        }
                        catch (Exception exc)
                        {
                            strOut = "ERROR: NOTE: sub rendered templates CANNOT contain postback controls.<br/>" + exc;
                        }
                    }
                }
                lc.Text = strOut;

            }
            catch (Exception)
            {
                lc.Text = "";
            }
        }

        #endregion

        #region "Sale Price"

        private void CreateConcatenate(Control container, XmlNode xmlNod)
        {
            var l = new Literal();
            l.Text = "";
            if (xmlNod.Attributes != null)
            {
                foreach (XmlAttribute attr in xmlNod.Attributes)
                {
                    if (attr.Name.StartsWith("xpath"))
                    {
                        l.Text += ";" + attr.InnerText;
                    }
                }
            }
            
            l.DataBinding += ConcatenateDataBinding;
 
            container.Controls.Add(l);
        }

        private void ConcatenateDataBinding(object sender, EventArgs e)
        {
            var l = (Literal)sender;
            var container = (IDataItemContainer)l.NamingContainer;
            var strXml = DataBinder.Eval(container.DataItem, _databindColumn).ToString();
            var nbi = new NBrightInfo();
            nbi.XMLData = strXml;
            try
            {
                var xlist = l.Text.Split(';');
                l.Text = "";
                foreach (var s in xlist)
                {
                    if (s != "" && !l.Text.Contains(nbi.GetXmlProperty(s))) l.Text += " " + nbi.GetXmlProperty(s);
                }
                l.Visible = NBrightGlobal.IsVisible;

            }
            catch (Exception ex)
            {
                l.Text = ex.ToString();
            }
        }


        #endregion


        #region "Functions"

        private List<NBrightInfo> BuildModelList(NBrightInfo dataItemObj,Boolean addSalePrices = false)
        {
            //build models list
            var objL = new List<NBrightInfo>();
            var nodList = dataItemObj.XMLDoc.SelectNodes("genxml/models/*");
            if (nodList != null)
            {

                #region "Init"

                var dealerrole = "Dealer"; //defualt dealer role
                if (_settings.ContainsKey("dealer.role")) dealerrole = _settings["dealer.role"];
                var isDealer = CmsProviderManager.Default.IsInRole(dealerrole);


                #endregion

                var lp = 1;
                foreach (XmlNode nod in nodList)
                {
                    // check if Deleted
                    var selectDeletedFlag = nod.SelectSingleNode("checkbox/chkdeleted");
                    if ((selectDeletedFlag != null) && (selectDeletedFlag.InnerText == "False"))
                    {
                        // check if dealer
                        var selectDealerFlag = nod.SelectSingleNode("checkbox/chkdealeronly");
                        if (((selectDealerFlag != null) && (!isDealer && (selectDealerFlag.InnerText == "False"))) | isDealer)
                        {
                            // get modelid
                            var nodModelId = nod.SelectSingleNode("hidden/modelid");
                            var modelId = "";
                            if (nodModelId != null) modelId = nodModelId.InnerText;

                            //Build NBrightInfo class for model
                            var o = new NBrightInfo();
                            o.XMLData = nod.OuterXml;

                            #region "Add Lanaguge Data"

                            var nodLang = dataItemObj.XMLDoc.SelectSingleNode("genxml/lang/genxml/models/genxml[" + lp.ToString("D") + "]");
                            if (nodLang != null)
                            {
                                o.AddSingleNode("lang", "", "genxml");
                                o.AddXmlNode(nodLang.OuterXml, "genxml", "genxml/lang");
                            }

                            #endregion

                            #region "Prices"

                            if (addSalePrices)
                            {
                                var uInfo = UserController.GetCurrentUserInfo();
                                if (uInfo != null)
                                {
                                    o.SetXmlPropertyDouble("genxml/hidden/saleprice", "-1"); // set to -1 so unitcost is displayed (turns off saleprice)
                                    //[TODO: convert to new promotion provider]
                                    //var objPromoCtrl = new PromoController();
                                    //var objPCtrl = new ProductController();
                                    //var objM = objPCtrl.GetModel(modelId, Utils.GetCurrentCulture());
                                    //var salePrice = objPromoCtrl.GetSalePrice(objM, uInfo);
                                    //o.AddSingleNode("saleprice", salePrice.ToString(CultureInfo.GetCultureInfo("en-US")), "genxml/hidden");
                                }
                            }

                            #endregion

                            // product data for display in modellist
                            o.SetXmlProperty("genxml/lang/genxml/textbox/txtproductname", dataItemObj.GetXmlProperty("genxml/lang/genxml/textbox/txtproductname"));
                            o.SetXmlProperty("genxml/textbox/txtproductref", dataItemObj.GetXmlProperty("genxml/textbox/txtproductref"));
                            o.SetXmlProperty("genxml/hidden/productid", dataItemObj.ItemID.ToString("D"));

                            objL.Add(o);
                        }
                    }
                    lp += 1;
                }
            }
            return objL;
        }

        private String GetSalePrice(NBrightInfo dataItemObj)
        {
            Double saleprice = -1;
            var l = BuildModelList(dataItemObj, true);
            foreach (var m in l)
            {
                var s = m.GetXmlPropertyDouble("genxml/hidden/saleprice");
                if ((s < saleprice) || (saleprice == -1)) saleprice = s;
            }
            return saleprice.ToString(CultureInfo.GetCultureInfo("en-US"));
        }


        private String GetDealerPrice(NBrightInfo dataItemObj)
        {
            var dealprice = "-1";
            var l = BuildModelList(dataItemObj);
            foreach (var m in l)
            {
                var s = m.GetXmlProperty("genxml/textbox/txtdealercost");
                if (Utils.IsNumeric(s))
                {
                    if ((Convert.ToDouble(s, CultureInfo.GetCultureInfo("en-US")) < Convert.ToDouble(dealprice, CultureInfo.GetCultureInfo("en-US"))) | (dealprice == "-1")) dealprice = s;
                }
            }
            return dealprice;
        }

        private String GetFromPrice(NBrightInfo dataItemObj)
        {
            var price = "-1";
            var l = BuildModelList(dataItemObj);
            foreach (var m in l)
            {
                var s = m.GetXmlProperty("genxml/textbox/txtunitcost");
                if (Utils.IsNumeric(s))
                {
                    // NBrightBuy numeric always stored in en-US format.
                    if ((Convert.ToDouble(s, CultureInfo.GetCultureInfo("en-US")) < Convert.ToDouble(price, CultureInfo.GetCultureInfo("en-US"))) | (price == "-1")) price = s;
                }
            }
            return price;
        }

        private String GetBestPrice(NBrightInfo dataItemObj)
        {
            var fromprice = Convert.ToDouble(GetFromPrice(dataItemObj));
            if (fromprice < 0) fromprice = 0; // make sure we have a valid price
            var saleprice = Convert.ToDouble(GetSalePrice(dataItemObj));
            if (saleprice < 0) saleprice = fromprice; // sale price might not exists.

            var role = "Dealer";
            if (!String.IsNullOrEmpty(_settings["dealer.role"])) role = _settings["dealer.role"];
            if (CmsProviderManager.Default.IsInRole(role))
            {
                var dealerprice = Convert.ToDouble(GetDealerPrice(dataItemObj));
                if (dealerprice <= 0) dealerprice = fromprice; // check for valid dealer price.
                if (fromprice < dealerprice)
                {
                    if (fromprice < saleprice) return fromprice.ToString(CultureInfo.GetCultureInfo("en-US"));
                    return saleprice.ToString(CultureInfo.GetCultureInfo("en-US"));
                }
                if (dealerprice < saleprice) return dealerprice.ToString(CultureInfo.GetCultureInfo("en-US"));
                return saleprice.ToString(CultureInfo.GetCultureInfo("en-US"));
            }
            if (fromprice < saleprice) return fromprice.ToString(CultureInfo.GetCultureInfo("en-US"));
            return saleprice.ToString(CultureInfo.GetCultureInfo("en-US"));                
        }

        private Boolean HasDifferentPrices(NBrightInfo dataItemObj)
        {
            var nodList = dataItemObj.XMLDoc.SelectNodes("genxml/models/*");
            if (nodList != null)
            {
                //check if we really need to add prices (don't if all the same)
                var holdPrice = "";
                var holdDealerPrice = "";
                var isDealer = CmsProviderManager.Default.IsInRole(_settings["dealer.role"]);
                foreach (XmlNode nod in nodList)
                {
                    var mPrice = nod.SelectSingleNode("textbox/txtunitcost");
                    if (mPrice != null)
                    {
                        if (holdPrice != "" && mPrice.InnerText != holdPrice)
                        {
                            return true;
                        }
                        holdPrice = mPrice.InnerText;
                    }
                    if (isDealer)
                    {
                        var mDealerPrice = nod.SelectSingleNode("textbox/txtdealercost");
                        if (mDealerPrice != null)
                        {
                            if (holdDealerPrice != "" && mDealerPrice.InnerText != holdDealerPrice) return true;
                            holdDealerPrice = mDealerPrice.InnerText;
                        }                        
                    }
                }
            }
            return false;
        }

        public static void IncreaseArray(ref string[] values, int increment)
        {
            var array = new string[values.Length + increment];
            values.CopyTo(array, 0);
            values = array;
        }

        private Boolean IsInStock(NBrightInfo dataItem,String qtyTestAmt = "0")
        {
            var amtTest = StoreSettings.Current.GetInt("minimumstocklevel"); 

            if (Utils.IsNumeric(qtyTestAmt)) amtTest = Convert.ToInt32(qtyTestAmt);
            var nodList = BuildModelList(dataItem);
            foreach (var obj in nodList)
            {
                var stockOn = obj.GetXmlPropertyBool("genxml/checkbox/chkstockon");
                if (stockOn)
                {
                    var modelstatus = obj.GetXmlProperty("genxml/dropdownlist/modelstatus");
                    if (modelstatus == "010") return true;                    
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}

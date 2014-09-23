// --- Copyright (c) notice NevoWeb ---
//  Copyright (c) 2014 SARL NevoWeb.  www.nevoweb.com. The MIT License (MIT).
// Author: D.C.Lee
// ------------------------------------------------------------------------
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
// ------------------------------------------------------------------------
// This copyright notice may NOT be removed, obscured or modified without written consent from the author.
// --- End copyright notice --- 

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;
using NBrightCore.common;
using NBrightCore.render;
using NBrightDNN;
using Nevoweb.DNN.NBrightBuy.Base;
using Nevoweb.DNN.NBrightBuy.Components;

namespace Nevoweb.DNN.NBrightBuy.Admin
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewNBrightGen class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class Tools : NBrightBuyAdminBase
    {


        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);

            try
            {


                #region "load templates"

                var t1 = "tools.html";

                // Get Display Body
                var dataTempl = ModCtrl.GetTemplateData(ModSettings, t1, Utils.GetCurrentCulture(), DebugMode);
                // insert page header text
                rpData.ItemTemplate = NBrightBuyUtils.GetGenXmlTemplate(dataTempl, ModSettings.Settings(), PortalSettings.HomeDirectory);

            }
            catch (Exception exc)
            {
                //display the error on the template (don;t want to log it here, prefer to deal with errors directly.)
                var l = new Literal();
                l.Text = exc.ToString();
                phData.Controls.Add(l);
            }

        }


        protected override void OnLoad(EventArgs e)
        {
            try
            {
                base.OnLoad(e);
                if (Page.IsPostBack == false)
                {
                    PageLoad();
                }
            }
            catch (Exception exc) //Module failed to load
            {
                //display the error on the template (don;t want to log it here, prefer to deal with errors directly.)
                var l = new Literal();
                l.Text = exc.ToString();
                phData.Controls.Add(l);
            }
        }

        private void PageLoad()
        {

            #region "Data Repeater"
            if (UserId > 0) // only logged in users can see data on this module.
            {
                var obj = new NBrightInfo(true);
                base.DoDetail(rpData,obj);
            }

            #endregion

        }

                #endregion

        #region  "Events "

        protected void CtrlItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var cArg = e.CommandArgument.ToString();
            var param = new string[3];

            switch (e.CommandName.ToLower())
            {
                case "cleardownstore":
                    param[0] = "";
                    DoClearDown();
                    Response.Redirect(Globals.NavigateURL(TabId, "", param), true);
                    break;
                case "validatestore":
                    param[0] = "";
                    ValidateStore();
                    NBrightBuyUtils.SetNotfiyMessage(ModuleId, "validatecompleted", NotifyCode.ok);
                    Response.Redirect(Globals.NavigateURL(TabId, "", param), true);
                    break;
                case "cancel":
                    param[0] = "";
                    Response.Redirect(Globals.NavigateURL(TabId, "", param), true);
                    break;
            }

        }

        #endregion

        private int ValidateStore()
        {
            var objCtrl = new NBrightBuyController();
            var errcount = 0;

            // Validate Products
            var prodList = objCtrl.GetList(DotNetNuke.Entities.Portals.PortalSettings.Current.PortalId, -1, "PRD");            
            foreach (var p in prodList)
            {
                var prodData = new ProductData(p.ItemID, StoreSettings.Current.EditLanguage);
                errcount += prodData.Validate();
            }

            // Validate Categories
            var catList = objCtrl.GetList(DotNetNuke.Entities.Portals.PortalSettings.Current.PortalId, -1, "PRD");
            foreach (var c in catList)
            {
                var catData = new CategoryData(c.ItemID, StoreSettings.Current.EditLanguage);
                errcount += catData.Validate();
            }
            return errcount;
        }

        private void DoClearDown()
        {
            var pass = GenXmlFunctions.GetField(rpData, "txtclearpass");
            if (pass == StoreSettings.Current.Get("adminpin") && pass != "")
            {
                var done = false;
                var objCtrl = new NBrightBuyController();
                var objQual = DotNetNuke.Data.DataProvider.Instance().ObjectQualifier;
                var dbOwner = DotNetNuke.Data.DataProvider.Instance().DatabaseOwner;
                var stmt = "";
                if (GenXmlFunctions.GetField(rpData, "clearproducts") == "True")
                {
                    stmt = "delete from " + dbOwner + "[" + objQual + "NBrightBuy] where PortalId = " + PortalId.ToString("") + " and typecode = 'PRD' ";
                    objCtrl.GetSqlxml(stmt);
                    stmt = "delete from " + dbOwner + "[" + objQual + "NBrightBuy] where PortalId = " + PortalId.ToString("") + " and typecode = 'PRDLANG' ";
                    objCtrl.GetSqlxml(stmt);
                    stmt = "delete from " + dbOwner + "[" + objQual + "NBrightBuy] where PortalId = " + PortalId.ToString("") + " and typecode = 'PRDXREF' ";
                    objCtrl.GetSqlxml(stmt);
                    stmt = "delete from " + dbOwner + "[" + objQual + "NBrightBuy] where PortalId = " + PortalId.ToString("") + " and typecode = 'CATCASCADE' ";
                    objCtrl.GetSqlxml(stmt);
                    stmt = "delete from " + dbOwner + "[" + objQual + "NBrightBuy] where PortalId = " + PortalId.ToString("") + " and typecode = 'CATXREF' ";
                    objCtrl.GetSqlxml(stmt);
                    done = true;
                }

                if (GenXmlFunctions.GetField(rpData, "clearcategories") == "True")
                {
                    stmt = "delete from " + dbOwner + "[" + objQual + "NBrightBuy] where PortalId = " + PortalId.ToString("") + " and typecode = 'CATEGORY' ";
                    objCtrl.GetSqlxml(stmt);
                    stmt = "delete from " + dbOwner + "[" + objQual + "NBrightBuy] where PortalId = " + PortalId.ToString("") + " and typecode = 'CATEGORYLANG' ";
                    objCtrl.GetSqlxml(stmt);
                    done = true;
                }


                if (GenXmlFunctions.GetField(rpData, "clearproperties") == "True")
                {
                    stmt = "delete from " + dbOwner + "[" + objQual + "NBrightBuy] where PortalId = " + PortalId.ToString("") + " and typecode = 'GROUP' ";
                    objCtrl.GetSqlxml(stmt);
                    stmt = "delete from " + dbOwner + "[" + objQual + "NBrightBuy] where PortalId = " + PortalId.ToString("") + " and typecode = 'GROUPLANG' ";
                    objCtrl.GetSqlxml(stmt);
                    done = true;
                }

                if (GenXmlFunctions.GetField(rpData, "clearorders") == "True")
                {
                    stmt = "delete from " + dbOwner + "[" + objQual + "NBrightBuy] where PortalId = " + PortalId.ToString("") + " and typecode = 'ORDER' ";
                    objCtrl.GetSqlxml(stmt);
                    done = true;
                }

                if (done) NBrightBuyUtils.SetNotfiyMessage(ModuleId, "deletecompleted", NotifyCode.ok);

            }

        }

    }

}

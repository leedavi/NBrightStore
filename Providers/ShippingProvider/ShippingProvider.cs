﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBrightDNN;

namespace Nevoweb.DNN.NBrightBuy.Providers
{
    public class ShippingProvider :Components.Interfaces.ShippingInterface 
    {
        public override NBrightInfo CalculateShipping(NBrightInfo cartInfo)
        {
            var nbi = new NBrightInfo(true);
            var shipData = new ShippingData();
            var shipoption = cartInfo.GetXmlProperty("genxml/extrainfo/gexml/radiobuttonlist/rblshippingoptions");
            var total = cartInfo.GetXmlPropertyDouble("genxml/appliedtotal");
            var countrycode = "";
            var regioncode = "";
            Double rangeValue = 0;
            switch (shipoption)
            {
                case "1":
                    countrycode = cartInfo.GetXmlProperty("genxml/billaddress/gexml/dropdownlist/country");
                    regioncode = cartInfo.GetXmlProperty("genxml/billaddress/gexml/dropdownlist/region");
                    rangeValue = cartInfo.GetXmlPropertyDouble("genxml/total");
                    nbi.SetXmlPropertyDouble("genxml/totaltest", shipData.CalculateShipping(countrycode,regioncode, rangeValue, total));    
                    return nbi;
                case "2":
                    countrycode = cartInfo.GetXmlProperty("genxml/shipaddress/gexml/dropdownlist/country");
                    regioncode = cartInfo.GetXmlProperty("genxml/shipaddress/gexml/dropdownlist/region");
                    rangeValue = cartInfo.GetXmlPropertyDouble("genxml/total");
                    nbi.SetXmlPropertyDouble("genxml/totaltest", shipData.CalculateShipping(countrycode, regioncode, rangeValue, total));    
                    return nbi;
                default:
                    nbi.SetXmlPropertyDouble("genxml/totaltest", 0);            
                    return nbi;
            }

        }

    }
}

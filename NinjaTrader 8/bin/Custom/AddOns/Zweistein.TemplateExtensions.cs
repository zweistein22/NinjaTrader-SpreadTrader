using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Zweistein
{
    public partial class TemplateExtensions
    {

        static void SetBarsPeriod(XmlNode node,NinjaTrader.Data.BarsPeriod barsperiod)
        {

            foreach(XmlNode n in node.ChildNodes)
            {
                if(n.Name== "BarsPeriodTypeSerialize") n.InnerText= XmlConvert.ToString(barsperiod.BarsPeriodTypeSerialize);
                if(n.Name== "BaseBarsPeriodType") n.InnerText = barsperiod.BaseBarsPeriodType.ToString();
                if(n.Name== "BaseBarsPeriodValue") n.InnerText = XmlConvert.ToString(barsperiod.BaseBarsPeriodValue);
                if(n.Name== "MarketDataType") n.InnerText = barsperiod.MarketDataType.ToString();
                if(n.Name== "PointAndFigurePriceType") n.InnerText = barsperiod.PointAndFigurePriceType.ToString();
                if(n.Name == "ReversalType") n.InnerText = barsperiod.ReversalType.ToString();
                if(n.Name == "Value") n.InnerText = XmlConvert.ToString(barsperiod.Value);
                if (n.Name == "Value2") n.InnerText = XmlConvert.ToString(barsperiod.Value2);
            }

        }
        public static string SpreadChartTemplate(string strInstrument, string B_strInstrument, int A, int B, double APriceDisplayMultiplier, double BPriceDisplayMultiplier, string pricestring,NinjaTrader.Data.BarsPeriod bp)
        {


           /*
            XmlDocument UIxml = new XmlDocument();
            UIxml.Load(NinjaTrader.Core.Globals.UserDataDir + "UI.xml");
            XmlNode lastbarsperiod = UIxml.SelectSingleNode("//NinjaTrader/LastBarsPeriod/BarsPeriod");
            SetBarsPeriod(lastbarsperiod, bp);
            UIxml.Save(NinjaTrader.Core.Globals.UserDataDir + "UI.xml");
			*/
            string TradingHoursSerializable = "Default 24 x 7";
            string charttemplatedir = NinjaTrader.Core.Globals.UserDataDir + "templates\\Chart\\";
            //string xmlfile = charttemplatedir + "SpreadTraderChart.xml";
			MemoryStream ms = new MemoryStream(Zweistein.TemplateExtensions.SpreadTraderChart,false);
			
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(ms);
			//xmlDoc.Load(xmlfile);

            int i = 0;
            XmlNodeList nodes0 = xmlDoc.SelectNodes("//NinjaTrader/NTTabPage/DataSeries");
            foreach(XmlNode n0 in nodes0)
            {

                XmlNodeList nodes00 = n0.SelectNodes("BarsProperties");
                foreach(XmlNode n00 in nodes00)
                {

                    foreach(XmlNode n01 in n00.SelectNodes("BarsProperties"))
                    {
                        foreach (XmlNode n02 in n01.ChildNodes)
                        {
                            if (n02.Name == "TradingHoursSerializable") n02.InnerText = TradingHoursSerializable;
                            if (n02.Name == "Label") n02.InnerText = i == 0 ? strInstrument : B_strInstrument;
                            if (n02.Name == "BarsPeriod") SetBarsPeriod(n02, bp);
                            if (n02.Name == "Instrument") n02.InnerText = i == 0 ? strInstrument : B_strInstrument;
                        }

                    }

                   

                }

                i++;
                if (i >= 2) break;
            }


            XmlNodeList userNodes = xmlDoc.SelectNodes("//NinjaTrader/NTTabPage/Indicators/Indicator");
            foreach (XmlNode userNode in userNodes)
            {
				string name=null;
				try {
                 name = userNode.Attributes["Name"].Value;
				}
				catch{}
                if (name==null ||name == "NinjaTrader.NinjaScript.Indicators.Spread")
                {
                   
					if(name!=null) {
						 //string displayname = userNode.Attributes["DisplayName"].Value;
                    	string ndp = "Spread(" + A.ToString() + "," + APriceDisplayMultiplier.ToString() + "," + B.ToString() + "," + B_strInstrument + "," + BPriceDisplayMultiplier.ToString() + "," + pricestring + ")";
                   		userNode.Attributes["DisplayName"].Value = ndp;
                    	userNode.Attributes["Instrument"].Value = strInstrument;
					}
                    XmlNodeList spreadnodes = userNode.SelectNodes("Spread");

                    foreach (XmlNode spreadnode in spreadnodes)
                    {
                        foreach (XmlNode childnode in spreadnode.ChildNodes)
                        {
                            if (childnode.Name == "B_strInstrument") childnode.InnerText = B_strInstrument.ToString();
                            if (childnode.Name == "A") childnode.InnerText = XmlConvert.ToString(A);
                            if (childnode.Name == "B") childnode.InnerText = XmlConvert.ToString(B);
                            if (childnode.Name == "APriceDisplayMultiplier") childnode.InnerText = XmlConvert.ToString(APriceDisplayMultiplier);
                            if (childnode.Name == "BPriceDisplayMultiplier") childnode.InnerText = XmlConvert.ToString(BPriceDisplayMultiplier);
                            if (childnode.Name == "BarsPeriodSerializable") SetBarsPeriod(childnode, bp);
                            if (childnode.Name == "PriceString") childnode.InnerText = pricestring;
                            

                        }
                    }
                }
            }

            string instr = "";
            if (A != 1) instr += A.ToString();
            instr += " " + strInstrument;
            instr += "-";
            if (B != 1) instr += B.ToString() + " ";
            instr += " " + B_strInstrument;

            xmlDoc.Save(charttemplatedir + instr + ".xml");
            return instr;
        }


    }


}

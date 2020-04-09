using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Globalization;
using System.Linq.Expressions;

using System.Xml;
using System.Data;
namespace Zweistein
{
    public static  class StringHelpers3
    {

        public static string RemoveQuotes(this string s)
        {

            string q = "\'";
            int i = s.IndexOf(q);
            if (i == -1) return s;
            int j = s.Substring(i + 1).IndexOf(q);
            if (j == -1) return s;
            return s.Substring(i + 1, j - i);

        }

        public static string RemoveSpecialSpaces(this string s)
        {

            string q = "\xa0";
            int i = s.IndexOf(q);
            if (i == -1) return s;
            else
            {
                if(i==0) s =s.Substring(i + 1);
                if (i > 0) s = s.Substring(0, i) + s.Substring(i + 1);
                return s.RemoveSpecialSpaces();
            }
        }

        
    }
        
    
    public class CurrencyConversion
    {
        public class Cube
        {
            string currency;
            double rate;

            static List<Cube> _l= new List<Cube>();
            static DateTime lastupdate = new DateTime(1950, 1, 1);
            Cube(string _currency, double _rate)
            {
                rate = _rate;
                currency = _currency;
            }
            public string Currency
            {
                get { return currency; }
            }
            public double Rate
            {
                get { return rate; }
            }

            public DateTime LastUpdate
            {
                get { return lastupdate; }
                set { lastupdate = value; }
            }

            public static List<Cube> EuroFxRefDaily()
            {
                if (lastupdate.Date.AddDays(1) > DateTime.Now.Date) return _l;
              
                XmlDocument doc = new XmlDocument();
                try
                {
                    doc.Load("http://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml");
                    lastupdate = DateTime.Now;
                }
                catch (Exception ex)
                {
                    Zweistein.NinjaTraderLog.Process("Cube.EuroFxRefDaily", ex.Message, NinjaTrader.Cbi.LogLevel.Error, NinjaTrader.Cbi.LogCategories.NinjaScript);
                    return _l ;
                }
                _l.Clear();
                // Get forecast with XPath  
                XmlNodeList nodes = doc.ChildNodes;
                CultureInfo CI = new CultureInfo("en-US", false);
              
                foreach (XmlNode node in nodes)
                {
                   foreach (XmlNode node2 in node.ChildNodes)
                    {
                        if (node2.Name == "Cube")
                        {
                            foreach (XmlNode node3 in node2.ChildNodes)
                            {
                                foreach (XmlNode node4 in node3.ChildNodes)
                                {
                                    double rate = System.Convert.ToDouble(node4.Attributes["rate"].Value, CI.NumberFormat); 
                                    Cube c = new Cube(node4.Attributes["currency"].Value, rate);
                                    _l.Add(c);
                                    

                                }

                            }

                        }

                    }
                }
                _l.Add(new Cube("EUR", 1.0));
                return _l;
            }
            public static decimal Convert(decimal amount, string fromCurrency, string toCurrency)
            {
                List<Cube> l = EuroFxRefDaily();
                Cube f=l.Find(c =>c.Currency==fromCurrency);
				if(f==null) throw new ArgumentNullException(fromCurrency, fromCurrency+ toCurrency+": no conversion rate defined");
                Cube t =l.Find(c => c.Currency == toCurrency);
				if(t==null) throw new ArgumentNullException(toCurrency, fromCurrency + toCurrency+": no conversion rate defined");
                return amount* (decimal)( t.Rate/f.Rate) ;
              
            }
        }

        public static string ISO(NinjaTrader.Cbi.Currency currency)
        {

            switch (currency)
            {

                case NinjaTrader.Cbi.Currency.AustralianDollar: return "AUD";
                case NinjaTrader.Cbi.Currency.BritishPound: return "GBP";
                case NinjaTrader.Cbi.Currency.CanadianDollar: return "CAD";
                case NinjaTrader.Cbi.Currency.Euro: return "EUR";
                case NinjaTrader.Cbi.Currency.HongKongDollar: return "HKD";
                case NinjaTrader.Cbi.Currency.IndianRupee: return "INR";
                case NinjaTrader.Cbi.Currency.JapaneseYen: return "JPY";
                case NinjaTrader.Cbi.Currency.KoreanWon: return "KRW";
                case NinjaTrader.Cbi.Currency.SwedishKrona: return "SEK";
				case NinjaTrader.Cbi.Currency.MexicanPeso: return "MXN";
                case NinjaTrader.Cbi.Currency.SwissFranc: return "CHF";
                case NinjaTrader.Cbi.Currency.UsDollar: return "USD";
					 case NinjaTrader.Cbi.Currency.BrazilianReal: return "BRL";
					 case NinjaTrader.Cbi.Currency.SingaporeDollar: return "SGD";
					 case NinjaTrader.Cbi.Currency.ChinaYuan: return "CNY";
					 case NinjaTrader.Cbi.Currency.MalaysiaRinggit: return "MYR";
					 case NinjaTrader.Cbi.Currency.ThailandBaht: return "THB";
					 case NinjaTrader.Cbi.Currency.TaiwanNewDollar: return "TWD";
					 case NinjaTrader.Cbi.Currency.NewZealandDollar: return "NZD";
					 case NinjaTrader.Cbi.Currency.SouthAfricanRand: return "ZAR";
					 case NinjaTrader.Cbi.Currency.CzechRepublicKoruna: return "CZK";
					 case NinjaTrader.Cbi.Currency.NorwayKrone: return "NOK";
					 case NinjaTrader.Cbi.Currency.DenmarkKrone: return "DKK";
					 case NinjaTrader.Cbi.Currency.HungaryForint: return "HUF";
					 case NinjaTrader.Cbi.Currency.PolandZloty: return "PLN";
					 case NinjaTrader.Cbi.Currency.TurkeyLira: return "TRY";
					 case NinjaTrader.Cbi.Currency.IsraeliShekel: return "ILS";
					 case NinjaTrader.Cbi.Currency.RussiaRuble: return "RUB";
					
				
            }

            return "???";
        }
   
        

        public static decimal gConvert(decimal amount, string fromCurrency, string toCurrency)
        {

            if (fromCurrency == toCurrency) return amount;
            WebClient web = new WebClient();

            string url = string.Format("http://www.google.com/ig/calculator?hl=en&q={2}{0}%3D%3F{1}", fromCurrency.ToUpper(), toCurrency.ToUpper(), amount);

            string response = web.DownloadString(url);

            Regex regex = new Regex("rhs: \\\"(\\d*.\\d*)");
            Match match = regex.Match(response);
            CultureInfo CI = new CultureInfo("en-US", false);
            string tmp=match.Groups[1].Value.RemoveSpecialSpaces();
            decimal rate = System.Convert.ToDecimal(tmp, CI.NumberFormat);
            return rate;
        }

       
    }
}

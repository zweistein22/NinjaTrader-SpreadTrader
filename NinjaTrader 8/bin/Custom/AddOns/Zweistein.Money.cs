#region Using declarations
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.NinjaScript;
using System.Globalization;
using System.Collections.Generic;
using System.Net;

#endregion


namespace Zweistein {
   
public sealed class Money : IEquatable<Money>, IComparable, IComparable<Money> {
     NinjaTrader.Cbi.Currency currency;
     decimal amount;
 
     public Money() : this((decimal) 0,LocalCurrency()) { }
	  public Money(double amount) : this((decimal)amount,LocalCurrency()) { }
     public Money(decimal amount) : this(amount,LocalCurrency()) { }
     public Money(long amount) : this((decimal)amount, LocalCurrency()) { }
     public Money(string isoCurrencySymbol) : this(MoneyString.FromString(isoCurrencySymbol)) { }
     public Money(decimal amount, string isoCurrencySymbol) : this(amount, MoneyString.FromString(isoCurrencySymbol)) { }
     public Money(Currency currency) : this((decimal) 0,currency) { }
	
	 public Money(double amount, Currency currency):this((decimal) amount,currency){
		
	}
     public Money(decimal amount, Currency currency) {
         this.currency = currency;
         this.amount = amount;
     }
	
	static NinjaTrader.Cbi.Currency LocalCurrency(){
		RegionInfo regionInfo=new RegionInfo(CultureInfo.CurrentCulture.LCID);
		return MoneyString.FromString(regionInfo.ISOCurrencySymbol);
	}
	
    static Dictionary<string, double> ConversionRate = new Dictionary<string, double>();



    static KeyValuePair<string, double> FromString(string s)
    {

        string[] tok = s.Split(new char[]{'='});
        KeyValuePair<string, double> kvp = new KeyValuePair<string, double>(tok[0],Double.Parse(tok[1],CultureInfo.InvariantCulture ));
        return kvp;

    }

    static KeyValuePair<string, double> Reverse(KeyValuePair<string, double> kvp)
    {
        string A = kvp.Key.Substring(0, 3);
        string B = kvp.Key.Substring(3, 3);
        KeyValuePair<string, double> pvk = new KeyValuePair<string, double>(B+A,1.0/kvp.Value);
        return pvk;
    }

   

	
	public static string[] Rates {
		get {
            if(rates==null) UpdateRates();
            return rates;
        }
		set {rates = value;}
		
	}
    public static void UpdateRates()
    {
        List<string> l=new List<string>();
        CultureInfo CI = new CultureInfo("en-US", false);
        string cur1="EUR";
        string cur2="USD";
        decimal d=Zweistein.CurrencyConversion.Cube.Convert(1,cur1,cur2);
        l.Add(cur1+cur2+"="+d.ToString());

        cur2="CHF";
        d=Zweistein.CurrencyConversion.Cube.Convert(1,cur1,cur2);
        l.Add(cur1+cur2+"="+d.ToString());

		cur2="SEK";
        d=Zweistein.CurrencyConversion.Cube.Convert(1,cur1,cur2);
        l.Add(cur1+cur2+"="+d.ToString());
		
		cur2="HKD";
        d=Zweistein.CurrencyConversion.Cube.Convert(1,cur1,cur2);
        l.Add(cur1+cur2+"="+d.ToString());
		
		cur2="KRW";
        d=Zweistein.CurrencyConversion.Cube.Convert(1,cur1,cur2);
        l.Add(cur1+cur2+"="+d.ToString());
		
		cur2="INR";
        d=Zweistein.CurrencyConversion.Cube.Convert(1,cur1,cur2);
        l.Add(cur1+cur2+"="+d.ToString());
		
		cur2="MXN";
        d=Zweistein.CurrencyConversion.Cube.Convert(1,cur1,cur2);
        l.Add(cur1+cur2+"="+d.ToString());
		
         cur2="GBP";
        d=Zweistein.CurrencyConversion.Cube.Convert(1,cur1,cur2);
        l.Add(cur1+cur2+"="+d.ToString());

        cur2="JPY";
        d=Zweistein.CurrencyConversion.Cube.Convert(1,cur1,cur2);
        l.Add(cur1+cur2+"="+d.ToString());

        cur2="CAD";
        d=Zweistein.CurrencyConversion.Cube.Convert(1,cur1,cur2);
        l.Add(cur1+cur2+"="+d.ToString());

        cur2="AUD";
        d=Zweistein.CurrencyConversion.Cube.Convert(1,cur1,cur2);
        l.Add(cur1+cur2+"="+d.ToString());

		cur2="BRL";
        d=Zweistein.CurrencyConversion.Cube.Convert(1,cur1,cur2);
        l.Add(cur1+cur2+"="+d.ToString());

		cur2="SGD";
        d=Zweistein.CurrencyConversion.Cube.Convert(1,cur1,cur2);
        l.Add(cur1+cur2+"="+d.ToString());

		cur2="CNY";
        d=Zweistein.CurrencyConversion.Cube.Convert(1,cur1,cur2);
        l.Add(cur1+cur2+"="+d.ToString());

		cur2="MYR";
        d=Zweistein.CurrencyConversion.Cube.Convert(1,cur1,cur2);
        l.Add(cur1+cur2+"="+d.ToString());

		cur2="THB";
        d=Zweistein.CurrencyConversion.Cube.Convert(1,cur1,cur2);
        l.Add(cur1+cur2+"="+d.ToString());
        try
        {
            cur2 = "TWD";
            d = Zweistein.CurrencyConversion.Cube.Convert(1, cur1, cur2);
            l.Add(cur1 + cur2 + "=" + d.ToString());
        }
        catch { }

        try
        {
            cur2 ="NZD";
             d=Zweistein.CurrencyConversion.Cube.Convert(1,cur1,cur2);
             l.Add(cur1+cur2+"="+d.ToString());
        }
        catch { }

        try
            {
                cur2 ="ZAR";
                 d=Zweistein.CurrencyConversion.Cube.Convert(1,cur1,cur2);
                l.Add(cur1+cur2+"="+d.ToString());
        }
        catch { }
        try
                {
                    cur2 ="CZK";
                     d=Zweistein.CurrencyConversion.Cube.Convert(1,cur1,cur2);
                    l.Add(cur1+cur2+"="+d.ToString());
        }
        catch { }
        try
                    {
                        cur2 ="NOK";
                        d=Zweistein.CurrencyConversion.Cube.Convert(1,cur1,cur2);
                        l.Add(cur1+cur2+"="+d.ToString());
        }
        catch { }
            try
            {
                cur2 ="DKK";
        		d=Zweistein.CurrencyConversion.Cube.Convert(1,cur1,cur2);
       			 l.Add(cur1+cur2+"="+d.ToString());
            }
            catch { }
            try
                {
                    cur2 ="HUF";
       				 d=Zweistein.CurrencyConversion.Cube.Convert(1,cur1,cur2);
        			l.Add(cur1+cur2+"="+d.ToString());
            }
            catch { }
            try
                    {
                        cur2 ="PLN";
        				d=Zweistein.CurrencyConversion.Cube.Convert(1,cur1,cur2);
        				l.Add(cur1+cur2+"="+d.ToString());
            }
            catch { }
            try
                        {
                            cur2 ="TRY";
        					d=Zweistein.CurrencyConversion.Cube.Convert(1,cur1,cur2);
       					 l.Add(cur1+cur2+"="+d.ToString());
            }
            catch { }
            try {
                                cur2 ="ILS";
       							 d=Zweistein.CurrencyConversion.Cube.Convert(1,cur1,cur2);
       							 l.Add(cur1+cur2+"="+d.ToString());
            }
            catch { }
            try {
               	cur2 ="RUB";
        		d=Zweistein.CurrencyConversion.Cube.Convert(1,cur1,cur2);
        		l.Add(cur1+cur2+"="+d.ToString());
            }
            catch { }

            rates = l.ToArray();

    }
    static string[] rates=null;
    
   
    public static void FillConversionRate()
    {
        if (rates == null) UpdateRates();

        foreach (NinjaTrader.Cbi.Currency a in Enum.GetValues(typeof(NinjaTrader.Cbi.Currency)))
        {
            string iso = CurrencyConversion.ISO(a);
            string key = iso + iso;
            ConversionRate.Add(key, 1.0);
        }
        
        foreach (string r in rates)
        {
            KeyValuePair<string,double> kvp=FromString(r);
            ConversionRate.Add(kvp.Key, kvp.Value);
            KeyValuePair<string,double>pvk=Reverse(kvp);
            ConversionRate.Add(pvk.Key, pvk.Value);
        }
        
        foreach (NinjaTrader.Cbi.Currency a in Enum.GetValues(typeof(NinjaTrader.Cbi.Currency)))
        {
            foreach (NinjaTrader.Cbi.Currency b in Enum.GetValues(typeof(NinjaTrader.Cbi.Currency)))
            {
                double d=1.0;
                string ab = CurrencyConversion.ISO(a) + CurrencyConversion.ISO(b);
                if (!ConversionRate.TryGetValue(ab, out d))
                {
                    // ab not exist, search ac and cb or also ac and bc
                    foreach (NinjaTrader.Cbi.Currency c in Enum.GetValues(typeof(NinjaTrader.Cbi.Currency)))
                    {
                        if (c == a) continue;
                        if (c == b) continue;
                        string ac = CurrencyConversion.ISO(a) + CurrencyConversion.ISO(c);
                        double d2 = 1.0;
                        if (ConversionRate.TryGetValue(ac, out d2))
                        {
                            // found ac, so check for bc 
                            double d3 = 1.0;
                            string cb = CurrencyConversion.ISO(c) + CurrencyConversion.ISO(b);
                            if (ConversionRate.TryGetValue(cb, out d3))
                            {
                                // so we have ac and cb
                                // means ab = ac*cb
                                // so save ab and ba
                                ConversionRate.Add(ab, d2 * d3);
                                string t1 = ab.Substring(0, 3);
                                string t2 = ab.Substring(3, 3);
                                ConversionRate.Add(t2+t1,1.0/( d2 * d3));


                                break;


                            }


                        }


                    }
                }

            }
        }



    }


    static decimal _crossrate(NinjaTrader.Cbi.Currency A,NinjaTrader.Cbi.Currency B)
    {
        if (ConversionRate.Count == 0) FillConversionRate();
        double d = 1.0;
        ConversionRate.TryGetValue(CurrencyConversion.ISO(A) + CurrencyConversion.ISO(B),out d);
        return (decimal)d;
    }


 	public NinjaTrader.Cbi.Currency Currency {
		get {return currency;}	
		
	}
	static decimal CrossRate(Currency A,Currency B){
		if(A==B) return 1;
        return _crossrate(A, B);
  	}
	static decimal CrossRate(Money A,Money B){
		if(A.Currency==B.Currency) return 1;
        return _crossrate(A.Currency, B.Currency);
	}
	
     public string ISOCurrencySymbol {
         get { return CurrencyConversion.ISO(currency); }
     }
     public decimal Amount {
         get { return amount; }
     }
   
     public static bool operator >(Money first, Money second) {
        
         return first.amount > second.amount*CrossRate(second,first);
     }
     public static bool operator >=(Money first, Money second) {
         AssertSameCurrency(first, second);
         return first.amount >= second.amount*CrossRate(second,first);
     }
     public static bool operator <=(Money first, Money second) {
         AssertSameCurrency(first, second);
         return first.amount <= second.amount*CrossRate(second,first);
     }
     public static bool operator <(Money first, Money second) {
         AssertSameCurrency(first, second);
         return first.amount < second.amount*CrossRate(second,first);
     }
     public static Money operator +(Money first, Money second) {
        return new Money(first.Amount + second.Amount*CrossRate(second,first), first.Currency);
     }
     public static Money Add(Money first, Money second) {
         return first + second;
     }
     public static Money operator -(Money first, Money second) {
          return new Money(first.Amount - second.Amount*CrossRate(second,first), first.Currency);
     }
     public static Money Subtract(Money first, Money second) {
         return first - second;
     }
     public static implicit operator Money(decimal amount) {
         return new Money(amount);
     }
     public static implicit operator Money(long amount) {
         return new Money(amount);
     }
     public override bool Equals(object obj) {
         return (obj is Money) && Equals((Money)obj);
     }
     public override int GetHashCode() {
         return amount.GetHashCode() ^ currency.GetHashCode();
     }
     private static void AssertSameCurrency(Money first, Money second) {
         if (first.ISOCurrencySymbol != second.ISOCurrencySymbol) 
             throw new InvalidOperationException("Money type mismatch.");
     }
     public bool Equals(Money other) {
         if (object.ReferenceEquals(other, null)) return false;
         return ((ISOCurrencySymbol == other.ISOCurrencySymbol) && (amount == other.amount));
     }
     public static bool operator ==(Money first, Money second) {
         if (object.ReferenceEquals(first, second)) return true;
         if (object.ReferenceEquals(first, null) || object.ReferenceEquals(second, null)) return false;
         return (first.Currency == second.Currency && first.Amount == second.Amount);
    }
     public static bool operator !=(Money first, Money second) {
         return !first.Equals(second);
     }
     public static Money operator *(Money money, decimal value) {
         if (money == null) throw new ArgumentNullException("money");
         return new Money(Decimal.Floor(money.Amount * value), money.Currency);
     }
     public static Money Multiply(Money money, decimal value) {
         return money * value;
     }
     public static Money operator /(Money money, decimal value) {
         if (money == null) throw new ArgumentNullException("money");
         return new Money(money.Amount / value, money.Currency);
     }
     public static Money Divide(Money first, decimal value) {
         return first / value;
     }
     public Money Copy() {
         return new Money(Amount,Currency);
     }
     public Money Clone() {
         return new Money(Currency);
     }
		
	 public Money ToCurrency(string isocode){
		Currency newc=MoneyString.FromString(isocode);
		Money m=new Money(this.Amount*CrossRate(newc,this.Currency),newc);
		return m;
		
	}
	
     public int CompareTo(object obj) {
         if (obj == null) {
             return 1;
         }
         if (!(obj is Money)) {
            throw new ArgumentException("Argument must be money");
         }
         return CompareTo((Money)obj);
     }
     public int CompareTo(Money other) {
         if (this < other) {
             return -1;
         }
         if (this > other) {
             return 1;
         }
         return 0;
     }
     public override string ToString() {
         return Amount.ToString()+ MoneyString.Symbol(currency);
     }
       
} 


	 public class MoneyString   {
        double amount = 0;
		NinjaTrader.Cbi.Currency currency=NinjaTrader.Cbi.Currency.Euro;
        
        public MoneyString(string input)
        {

            try
            {
				if(string.IsNullOrEmpty(input)) return;
				// string is number currency
				string scur="";
				string numberpart="";
				foreach(char c in input){
				 if(char.IsSymbol(c)) scur+=c;
				 else if(char.IsLetter(c)) scur+=c;
				 else numberpart+=c;
				}
				
				
				foreach(NinjaTrader.Cbi.Currency cur in Enum.GetValues(typeof(NinjaTrader.Cbi.Currency))){
					
					string tmp=CurrencyConversion.ISO(cur);
					
					if(scur.ToUpper().Contains(tmp.ToUpper())){
						currency=cur;
						break;
					}
					if(scur.Contains(Symbol(cur))){
						currency=cur;
						break;
					}
					
					
				}
                amount = Convert.ToDouble(numberpart,CultureInfo.CurrentCulture);
    
            }
            catch { }
        }
		
	
		
        public static MoneyString Zero
        {

            get { return new MoneyString("0"+" ???"); }
        }

        public double Amount
        {
            get
            {
                return amount;

            }

        }
		
		 public NinjaTrader.Cbi.Currency Currency
        {
            get
            {
                return currency;

            }

        }

        public string String
        {

            get
            {
                string tmp = amount.ToString() +" "+Symbol(currency);
                return tmp;
            }

        }
		
		public static NinjaTrader.Cbi.Currency FromString(string s){
			
			if(s=="AUD") return NinjaTrader.Cbi.Currency.AustralianDollar;
			if(s=="GBP") return NinjaTrader.Cbi.Currency.BritishPound;
			if(s=="£") return NinjaTrader.Cbi.Currency.BritishPound;
			if(s=="EUR") return NinjaTrader.Cbi.Currency.Euro;
			if(s=="€") return NinjaTrader.Cbi.Currency.Euro;
			if(s=="HKD") return NinjaTrader.Cbi.Currency.HongKongDollar;
			if(s=="INR") return NinjaTrader.Cbi.Currency.IndianRupee;
			if(s=="₹") return NinjaTrader.Cbi.Currency.IndianRupee;
			if(s=="JPY") return NinjaTrader.Cbi.Currency.JapaneseYen;
			if(s=="¥") return NinjaTrader.Cbi.Currency.JapaneseYen;
            if (s == "CAD") return NinjaTrader.Cbi.Currency.CanadianDollar;
			if(s=="KRW") return NinjaTrader.Cbi.Currency.KoreanWon;
			if(s=="SEK") return NinjaTrader.Cbi.Currency.SwedishKrona;
			if(s=="MXN") return NinjaTrader.Cbi.Currency.MexicanPeso;
			if(s=="CHF") return NinjaTrader.Cbi.Currency.SwissFranc;
			if(s=="USD") return NinjaTrader.Cbi.Currency.UsDollar;
			if(s=="$") return NinjaTrader.Cbi.Currency.UsDollar;
			
			if(s=="BRL") return NinjaTrader.Cbi.Currency.BrazilianReal;
			if(s=="R$") return NinjaTrader.Cbi.Currency.BrazilianReal;
			if(s=="SGD") return NinjaTrader.Cbi.Currency.SingaporeDollar;
			if(s=="CNY") return NinjaTrader.Cbi.Currency.ChinaYuan;
			if(s=="MYR") return NinjaTrader.Cbi.Currency.MalaysiaRinggit;
			if(s=="THB") return NinjaTrader.Cbi.Currency.ThailandBaht;
			if(s=="TWD") return NinjaTrader.Cbi.Currency.TaiwanNewDollar;
			if(s=="NZD") return NinjaTrader.Cbi.Currency.NewZealandDollar;
			if(s=="ZAR") return NinjaTrader.Cbi.Currency.SouthAfricanRand;
			if(s=="CZK") return NinjaTrader.Cbi.Currency.CzechRepublicKoruna;
			if(s=="Kč") return NinjaTrader.Cbi.Currency.CzechRepublicKoruna;
            if(s == "NOK") return NinjaTrader.Cbi.Currency.NorwayKrone;
			if(s=="kr")  return NinjaTrader.Cbi.Currency.NorwayKrone;
			if(s=="DKK") return NinjaTrader.Cbi.Currency.DenmarkKrone;
			if(s=="kr.") return NinjaTrader.Cbi.Currency.DenmarkKrone;
			if(s=="HUF") return NinjaTrader.Cbi.Currency.HungaryForint;
			if(s=="Ft") return NinjaTrader.Cbi.Currency.HungaryForint;
			if(s=="PLN") return NinjaTrader.Cbi.Currency.PolandZloty;
			if(s=="TRY") return NinjaTrader.Cbi.Currency.TurkeyLira;
			if(s=="ILS") return NinjaTrader.Cbi.Currency.IsraeliShekel;
			if(s=="RUB") return NinjaTrader.Cbi.Currency.RussiaRuble;
			if(s=="₽")  return NinjaTrader.Cbi.Currency.RussiaRuble;
			
			return NinjaTrader.Cbi.Currency.Euro;
			
		}
		
		
			
        public static bool IsPrefix(NinjaTrader.Cbi.Currency currency)
        {
            if (currency == NinjaTrader.Cbi.Currency.BritishPound) return true;
            if (currency == NinjaTrader.Cbi.Currency.CanadianDollar) return true;
            if (currency == NinjaTrader.Cbi.Currency.SwissFranc) return true;
            if (currency == NinjaTrader.Cbi.Currency.UsDollar) return true;
            if (currency == NinjaTrader.Cbi.Currency.JapaneseYen) return true;
            if (currency == NinjaTrader.Cbi.Currency.IsraeliShekel) return true;
            if (currency == NinjaTrader.Cbi.Currency.MalaysiaRinggit) return true;
            if (currency == NinjaTrader.Cbi.Currency.ChinaYuan) return true;
            if (currency == NinjaTrader.Cbi.Currency.SouthAfricanRand) return true;
              if (currency == NinjaTrader.Cbi.Currency.HongKongDollar) return true;

            return false;
        }

		public static string Symbol(NinjaTrader.Cbi.Currency currency){
			string cur =CurrencyConversion.ISO(currency);
			if(cur=="EUR") return "€";// "EUR";
			if(cur=="USD") return "$";  //prefix
            // CHF  prefix
			if(cur=="GBP") return  "£"; //prefix
			if(cur=="JPY") return "¥"; // prefix  "YEN";
            if (cur == "RUB") return "₽";
            if (cur == "ILS") return "₪"; // prefix
            if (cur == "CZK") return "Kč";
            if (cur == "DKK") return "kr.";
            if (cur == "BRL") return "R$";
            if (cur == "TWD")  return "NT$";
            if (cur == "ZAR") return "R"; //prefix
            if (cur == "TRY") return "₺";//prefix
            if (cur == "PLN")  return "zł";
            //if (cur == "CNY") return  "¥"; //prefix same as JPY
            if (cur == "MYR") return "RM"; //prefix
            if (cur == "NOK") return "kr";
            if (cur == "INR") return "₹";
            if (cur == "HUF") return "Ft";
            return cur;
			
		}
	}
 
 }
 
 
 

using System;
using System.Diagnostics;
using System.Globalization;


namespace Zweistein {

	   public class FractionString
    {
        decimal num = 0;
        decimal den = 1;
       char _sp='/';
		public FractionString(string input):this(input,'/'){
			
		}
        public FractionString(string input,char sp)
        {
           _sp=sp;
            try
            {
				if(string.IsNullOrEmpty(input)) return;
                string[] part = input.Split(new char[] { sp });
                if (part != null && part.Length > 0){
					part[0]=part[0].Replace(",",CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
					part[0]=part[0].Replace(".",CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
					num = Convert.ToDecimal(part[0],CultureInfo.CurrentCulture);
				}
                if (part != null && part.Length > 1) {
					part[1]=part[1].Replace(",",CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
					part[1]=part[1].Replace(".",CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
					den = Convert.ToDecimal(part[1],CultureInfo.CurrentCulture);
				}
            }
            catch { }
        }
		
		public decimal Nominator {
			get {return num;}	
			
		}
		
		public decimal Denominator {
				get {return den;}	
		}
		
        public  FractionString Zero
        {

            get { return new FractionString("0"+_sp+"1"); }
        }

        public static decimal GCD(decimal a, decimal b)
        {
            if (b == 0)
                return a;
            return GCD(b, a % b);
        }

        public double Double
        {
            get
            {
                if (den == 0 && num>=0) return double.PositiveInfinity;
                if (den == 0 && num < 0) return double.NegativeInfinity;

                return (double)(num / den);

            }

        }

        public string String
        {

            get
            {
                string tmp = num.ToString() + _sp + den.ToString();
                return tmp;
            }

        }

    }
	
	
}
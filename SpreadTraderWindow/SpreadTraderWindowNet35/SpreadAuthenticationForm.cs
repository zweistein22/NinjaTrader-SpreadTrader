using System;
using System.IO;
using Infralution.Licensing;

namespace Zweistein
{
    class SpreadAuthenticationForm : System.Windows.Forms.Form
    {
        const string LICENSE_PARAMETERS =
    @"<AuthenticatedLicenseParameters>
	  <EncryptedLicenseParameters>
	    <ProductName>SpreadTrader_8</ProductName>
	    <RSAKeyValue>
	      <Modulus>sxtdEsjJVyMIypZfhjtkxMfQGlHbbV9HePk4KWmYBWYip/jqF8OwywndYwmYfgWz7KubVBk4aZwZSYb0XKqD91XMpGvcXzDJIoS+2MAR/LjPBG6XHJpGc8bmjNnx8aAW4aaD+I4P7X/WdHNgxVf72LCcP4CebYDymCPRSQ0Z4eM=</Modulus>
	      <Exponent>AQAB</Exponent>
	    </RSAKeyValue>
	    <DesignSignature>K53FzyVo9P7jgOJraW0rXV8YNKAljLydo+pU8oT6i4WEQVwWOWtHP8Hh7lpBGz3bTzqNjk+6uYx4zUB9T1AvkJySNasR/PFagMy0UKbY6dSesun33DAjPyasGuAQbLuVcCbzd4ly3Nj1JonqUbx0XP7S8TX2l2SOlagtNSLhm+g=</DesignSignature>
	    <RuntimeSignature>Tko38zYomXlv66dsllerQQ+E0Q3fP8GtlgPGKzOZCHHcyh+IMfCaFdBn4Rcq49y7Igi7omxaDv9UEBRIVkqZ3wM7IKf4hRwHW6jrSb1YCK5zkqo8RgsMS7nYuz9pLolBP5OyVY4QtH9DmxG5KY7oDbLxGJrNCQ02fVjCfyfywIE=</RuntimeSignature>
	    <KeyStrength>7</KeyStrength>
	  </EncryptedLicenseParameters>
	  <AuthenticationServerURL>http://nthelperzweistein.dyndns.ws:2142/AuthenticationService.asmx</AuthenticationServerURL>
	  <ServerRSAKeyValue>
	    <Modulus>sXsdyMJaftPcLxzghEy3Sw8/aIvt2Ht8v9CVumloGuUFgC/a2RdBhy72/L5Ey18egaX5L3IJNkattJPXeRpVfAW15WzslcNq3pu50YsrS0yBR4nMPdqBaY89xh6nLr7uand2Aj9gHXUcxuifTuflQ8Dj4eWtfiLfR5cqylC285E=</Modulus>
	    <Exponent>AQAB</Exponent>
	  </ServerRSAKeyValue>
	</AuthenticatedLicenseParameters>";


        static internal string LICENSE_FILE = "Zweistein.SpreadTrader8.lic|0";
        static private bool UpdateLicenseDetails(AuthenticatedLicense license,NinjaTrader.NinjaScript.StrategyBase S)
        {
            string s = "EXPIRY:";//20090911
            int index = 0;
            DateTime expiry = DateTime.MinValue;
           	index = license.ProductInfo.IndexOf(s);
            if (index >= 0)
            {
                string[] sub = (license.ProductInfo.Substring(index + s.Length)).Split(new char[] { ' ', ';', ',' }, StringSplitOptions.None);
                if (sub[0].Length >= 8)
                {
                    int year = Convert.ToInt32(sub[0].Substring(0, 4));
                    int month = Convert.ToInt32(sub[0].Substring(4, 2));
                    int day = Convert.ToInt32(sub[0].Substring(6, 2));
                    expiry = new DateTime(year, month, day);
                }
                if (sub[0].Length == 8 || DateTime.Now < expiry)
                {
                   LICENSE_FILE = LICENSE_FILE.Replace("|0", "|1");
					return true;
                }

            }
            
			if(S.Account.Name=="Sim101" || S.Account.Name=="Backtest" || S.Account.Name=="Replay101") return true;
           // ninjaok = true;
            return false;
			
			
        }

        internal static bool ninjaok = false;
    
			
        public static bool _AuthenticatedLicense(NinjaTrader.NinjaScript.StrategyBase S)
        {
           
           // if (ninjaok) return true;
            string[] results = LICENSE_FILE.Split(new char[] { '|' }, StringSplitOptions.None);
            string licence_file = results[0];
            if (results[1] == "1") return true;
           
            AuthenticatedLicenseProvider provider = new AuthenticatedLicenseProvider();
            string basepath = NinjaTrader.Core.Globals.UserDataDir;
            basepath += "bin\\Custom\\";
           
                AuthenticatedLicense license = null;
                try
                {
                    FileInfo f = new FileInfo(basepath + licence_file);

                    if (f != null && f.Length > 0) license = provider.GetLicense(LICENSE_PARAMETERS, basepath + licence_file, false);
                }
                catch {
                   
                }   

            if (license == null)
            {
                AuthenticatedLicenseProvider.SetParameters(LICENSE_PARAMETERS);
                AuthenticatedLicenseInstallForm licenseForm = new AuthenticatedLicenseInstallForm();
                licenseForm.Text = "Zweistein SpreadTrader";
                license = licenseForm.ShowDialog(basepath + licence_file, null);
            }
            if (license != null) provider.ValidateLicense(license);

            if (license != null && license.Status == AuthenticatedLicenseStatus.Valid)
            {
                if (!UpdateLicenseDetails(license,S))
                {
                    System.Windows.Forms.MessageBox.Show("License error:" + license.ProductInfo, licence_file);
                    return false;
                }
            }
            else return false;
          //  LICENSE_FILE = LICENSE_FILE.Replace("|0", "|1");
            ninjaok = true;
            return true;
        }


    }
}


using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Web;


namespace Zweistein
{

    public partial class Diagnostics
    {
        static string defaulttrace = "Zweistein";
        static string tracedir = "trace\\";
        public static void Trace(string msg)
        {
            Trace(msg, defaulttrace);
        }

      
        public static void Trace(string msg, string name, bool bfullname)
        {
            string p = Process.GetCurrentProcess().ProcessName + ":";
            string ninjadir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NinjaTrader 8\\";
            if (!Directory.Exists(ninjadir + tracedir))
            {
                Directory.CreateDirectory(ninjadir + tracedir);
            }
            DateTimeFormatInfo dtfi = CultureInfo.CurrentCulture.DateTimeFormat;
            try
            {
                string filename = name + "." + DateTime.Now.Date.ToString("yyyyMMdd") + ".txt";
                if (bfullname) filename = name;
                StreamWriter ftpw = new StreamWriter(ninjadir + tracedir + filename, true);
                string dt = DateTime.Now.ToString(dtfi.LongTimePattern) + "." + DateTime.Now.Millisecond.ToString("D3");
                if (bfullname) dt = DateTime.Now.ToUniversalTime().ToString() + "." + DateTime.Now.Millisecond.ToString("D3");
                // StreamWriter sw = new StreamWriter(filename, false);
                ftpw.WriteLine(dt + " \t" + p + " \t" + msg);
                ftpw.Close();
            }
            catch { }


        }


        static void Trace(string msg, string name)
        {
         	
			Trace(msg, name, false);
		}


       
    
     
       
    }

 

}

//#define NET35

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Markup;
using NinjaTrader.NinjaScript;
using NinjaTrader.NinjaScript.Strategies;
using NinjaTrader.Cbi;
using System.Collections.ObjectModel;
using System.Windows.Controls.WpfPropertyGrid;
using System.ComponentModel.DataAnnotations;
#if NET35
using Microsoft.Windows.Controls;
#else
using System.Xaml;
using System.Windows.Baml2006;
#endif
namespace Zweistein
{


    public enum ClickedButton
    {
        Close = 0,
        GoLong,
        GoShort,
        Reverse,
    }
    public class SpreadOrderTicket
    {
        double _limit;
        int _units;
        MarketPosition _position;
        public static SpreadOrderTicket Create(string s)
        {
            string[] tok = s.Split(new char[] { '@' });
            if (tok == null || tok.Length != 2) return null;
            int iresult = 0;
            if (!Int32.TryParse(tok[0], out iresult)) return null;
            if (iresult == 0) return null;
            double dresult = 0;
            if (!Double.TryParse(tok[1], out dresult)) return null;
            MarketPosition position = MarketPosition.Long;
            if (iresult < 0) position = MarketPosition.Short;

            return new SpreadOrderTicket(position, Math.Abs(iresult), dresult);
        }
        public SpreadOrderTicket(MarketPosition position, int units, double limit)
        {

            _limit = limit;
            _units = units;
            _position = position;
        }

        public Zweistein.ClickedButton ClickedButton
        {
            get
            {
                if (_position == MarketPosition.Long) return Zweistein.ClickedButton.GoLong;
                if (_position == MarketPosition.Short) return Zweistein.ClickedButton.GoShort;
                return Zweistein.ClickedButton.Close;

            }

        }

        public bool EntryConditionSatisfied(double price)
        {
            if (_position == MarketPosition.Long)
            {
                if (price <= _limit) return true;
            }

            if (_position == MarketPosition.Short)
            {
                if (price >= _limit) return true;
            }
            return false;
        }

        public int Units
        {
            get
            {
                return _units;
            }
        }

        public string DisplayString
        {
            get
            {
                string s = "";
                if (_position == MarketPosition.Short) s += "-";
                s += _units.ToString() + "@" + _limit.ToString();
                return s;
            }
        }
    }

    /// <summary>
    /// Interaction logic for SpreadTraderWindow.xaml
    /// </summary>
    public partial class SpreadTraderWindow : Window
    {

       
		static SolidColorBrush QtyLongBackgroundBrush;
		static SolidColorBrush QtyShortBackgroundBrush;

        public ObservableCollection<SpreadOrderTicket> tickets { get; set; }

        
        FlowDocument doc  = new FlowDocument();

        SpreadExitStrategy _strategy;

     
        public void FillGrid()
        {

            
            Parameters.SelectedObject = _strategy;
     //Denis Vuyka
			foreach (PropertyItem p in Parameters.Properties)
            {
				try {
              		 DisplayAttribute v = p.GetAttribute<DisplayAttribute>();
					if(v==null) continue;
					if(v.GroupName=="RuntimeEditable") continue;
				}
				catch{}
				p.IsBrowsable=false;
				p.IsExpanded=true;
				
				//if (p.GetAttribute<DisplayAttribute>() .CategoryName != "Spread") p.IsBrowsable = false;
            }
            

            if (tickets == null) tickets = new ObservableCollection<SpreadOrderTicket>();
            opentickets.ItemsSource = tickets;

        }

        public void SetPositionTextAndValue(string txt,double val)
        {
		    this.btnPosition.Content = txt;
            this.btnValue.Content = val.ToString("0.00");

        }

        public void SetUnrealizedPNL(double pnl)
        {
            if (pnl > 0) unrealizedPNL.Foreground = QtyLongBackgroundBrush;
            else unrealizedPNL.Foreground = QtyShortBackgroundBrush;
            unrealizedPNL.Content = pnl.ToString("0.00");

        }


        public void SetDetailsPnl(double TotalRealized,string priceprefix,double currentprice,List<double> pnlticket,List<string> lines)
        {
            doc.Blocks.Clear();
            Paragraph p = new Paragraph(new Bold(new Run("Realized=" + TotalRealized.ToString("0.00")+"\r\t" + priceprefix + currentprice.ToString("0.00"))));
            doc.Blocks.Add(p);
          	
            for(int i = 0; i < pnlticket.Count; i++)
            {
                 p = new Paragraph(new Run("PNL=" + pnlticket[i].ToString("0.00") + lines[i]));
                doc.Blocks.Add(p);


            }
            

            realizedPNL.Document = doc;
        }
        


        public void fromStrategy(SpreadExitStrategy strategy)
        {
            _strategy = strategy;

           
            try { 
            btnGoLong.Click += new RoutedEventHandler(strategy.GoLong_Click);
            btnGoShort.Click += new RoutedEventHandler(strategy.GoShort_Click);
            btnReverse.Click += new RoutedEventHandler(strategy.Reverse_Click);
            btnClose.Click += new RoutedEventHandler(strategy.Close_Click);

            
           }
        catch{}
            
        }

       



#if NET35 == false
        public static SpreadTraderWindow CreateFromBaml(Assembly localAssembly)
        {
            string assname = localAssembly.FullName;
            MemoryStream ms = new MemoryStream();
            using (MemoryStream fstream = new MemoryStream(baml,false))
              //using (FileStream fstream = new FileStream(@"Properties\\SpreadTraderWindow.baml", FileMode.Open))
            {
                long len = fstream.Length;
                byte[] buffer = new byte[1024];
                len-=fstream.Read(buffer, 0, 40);
                ms.Write(buffer, 0, 39);
                int i = buffer[39];
                len-= fstream.Read(buffer, 0, i);
                byte[] bytes = Encoding.Default.GetBytes(assname);
                byte b = (byte) bytes.Length;
                ms.WriteByte(b);
                ms.Write(bytes, 0, bytes.Length);
                while(len>0)
                {
                    long chunk = (len > 1024L) ? 1024L : len;
                    len -= fstream.Read(buffer, 0,(int) chunk);
                    ms.Write(buffer, 0, (int)chunk);
                }

            }
            ms.Seek(0, SeekOrigin.Begin);

            XamlReaderSettings settings = new XamlReaderSettings();
            settings.LocalAssembly = localAssembly;
            Baml2006Reader reader = new Baml2006Reader(ms, settings);
            SpreadTraderWindow obj = System.Windows.Markup.XamlReader.Load(reader) as SpreadTraderWindow;

            try

            {
                LinearGradientBrush btnbackgroundBrush = Application.Current.TryFindResource("SuperDom.ButtonBackground") as LinearGradientBrush;

                obj.btnReverse.Background = btnbackgroundBrush;
                obj.btnClose.Background = btnbackgroundBrush;
                obj.btnGoLong.Background = btnbackgroundBrush;
                obj.btnGoShort.Background = btnbackgroundBrush;
				QtyLongBackgroundBrush = Application.Current.TryFindResource("BasicEntry.PositionQtyLongBackground") as SolidColorBrush;
        	    QtyShortBackgroundBrush = Application.Current.TryFindResource("BasicEntry.PositionQtyShortBackground") as SolidColorBrush;
        		SolidColorBrush PnLBackgroundBrush = Application.Current.TryFindResource("BasicEntry.PnLBackground") as SolidColorBrush;
                obj.unrealizedPNL.Background = PnLBackgroundBrush;

            }
            catch { }

            
            return obj;



        }

#endif
        public SpreadTraderWindow()
        {

        }
        
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            HwndSource hwndSource = PresentationSource.FromVisual(this) as HwndSource;

            if (hwndSource != null)
            {
                hwndSource.AddHook(HwndSourceHook);
            }

        }

        private bool allowClosing = false;

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32.dll")]
        private static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        private const uint MF_BYCOMMAND = 0x00000000;
        private const uint MF_GRAYED = 0x00000001;

        private const uint SC_CLOSE = 0xF060;

        private const int WM_SHOWWINDOW = 0x00000018;
        private const int WM_CLOSE = 0x10;
        private const int GWL_STYLE = -16;

        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private IntPtr HwndSourceHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WM_SHOWWINDOW:
                    {
                        SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
                        /*IntPtr hMenu = GetSystemMenu(hwnd, false);
                        if (hMenu != IntPtr.Zero)
                        {
                            EnableMenuItem(hMenu, SC_CLOSE, MF_BYCOMMAND | MF_GRAYED);
                        }
                        */
                    }
                    break;
                case WM_CLOSE:
                    if (!allowClosing)
                    {
                        handled = true;
                    }
                    break;
            }
            return IntPtr.Zero;
        }


      
        public void Destroy()
        {

            allowClosing = true;
            Close();
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
         
            Window w = sender as Window;
            _strategy.Left = w.Left;
            _strategy.Top = w.Top;
        }
      
        public void DeleteTicket(SpreadOrderTicket sot)
        {
            lock (tickets)
            {
                try
                {
                    tickets.Remove(sot);
     
                }
                catch { }
            }

        }

        public void AddTicket(SpreadOrderTicket sot)
        {
            lock (tickets)
            {
                try
                {
                    tickets.Add(sot);
                 
                }
                catch { }
            }

        }

        private void btnAddTicket_Click(object sender, RoutedEventArgs e)
        {
           
            SpreadOrderTicket sot = SpreadOrderTicket.Create(txtTicket.Text);
            if (sot != null) AddTicket(sot);

        }

       

        private void btnDelTicket_Click(object sender, RoutedEventArgs e)
        {
            
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    SpreadOrderTicket tmp = row.DataContext as SpreadOrderTicket;
                    if (tmp != null) DeleteTicket(tmp);
                        break;
                }
        }

       
    }
}

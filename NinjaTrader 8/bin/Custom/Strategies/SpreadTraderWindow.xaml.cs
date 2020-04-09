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
using System.Windows.Automation;
#if NET35
using Microsoft.Windows.Controls;
#else
using System.Xaml;
using System.Windows.Baml2006;
#endif
using System.ComponentModel;

namespace Zweistein
{
	

    public enum ClickedButton
    {
        Close = 0,
        GoLong,
        GoShort,
        Reverse,
    }
	
		public class ClickhandlerArgs {
		ClickedButton _clicked;
		int _units;
		Guid _guid;
	   // NinjaTrader.Cbi.Currency _currency;
		
		public ClickhandlerArgs(ClickedButton clicked,int units,Guid guid/*,NinjaTrader.Cbi.Currency currency*/){
			_clicked=clicked;
			_units=units;
			_guid=guid;
		//	_currency=currency;
		}
		
		public ClickhandlerArgs(ClickedButton clicked,int units/*,NinjaTrader.Cbi.Currency currency*/){
			_clicked=clicked;
			_units=units;
			//_currency=currency;
		}
		
		public Zweistein.ClickedButton ClickedButton {
			get {return _clicked;}	
			
		}
		
		public int Units {
			get {return _units;}	
		}
		
		public Guid Guid {
			get {return _guid;}
			set { _guid=value;}
		}
		/*
		public NinjaTrader.Cbi.Currency Currency {
			get {return _currency;}
			set { _currency=value;}
		}
		*/
		
	}
    public class SpreadOrderTicket:ICloneable
    {
        double _limit;
        int _units;
        MarketPosition _position;
		string _ieh;
		Guid _guid;
		
		public object Clone(){
			SpreadOrderTicket sot=new SpreadOrderTicket();
			sot._ieh=this._ieh;
			sot._guid=this._guid;
			sot._units=this._units;
			sot._limit=this._limit;
			sot._position=this._position;
			return sot;
		}
		
        public static SpreadOrderTicket Create(string s)
        {
			string param4=null;
            string[] tok = s.Split(new char[] { '@','|' });
            if (tok == null || tok.Length < 2) return null;
            int iresult = 0;
            if (!Int32.TryParse(tok[0], out iresult)) return null;
            if (iresult == 0) return null;
            double dresult = 0;
            if (!Double.TryParse(tok[1], out dresult)) return null;
			if(tok.Length>2) param4=tok[2];
            MarketPosition position = MarketPosition.Long;
            if (iresult < 0) position = MarketPosition.Short;

            return new SpreadOrderTicket(position, Math.Abs(iresult), dresult,param4);
        }
		
		public SpreadOrderTicket(){
		}
        public SpreadOrderTicket(MarketPosition position, int units, double limit,string ieh)
        {
			 _guid=Guid.NewGuid();
			_ieh=ieh;
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
		
		public Guid Guid {
			get {return _guid;}
			set {_guid=value;}
		
		}

        public string DisplayString
        {
            get
            {
                string s = "";
                if (_position == MarketPosition.Short) s += "-";
                s += _units.ToString() + "@" + _limit.ToString();
				if(_ieh!=null) s+="|"+_ieh;
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

        StrategyBase _strategy;
		//SpreadExitStrategy _strategy;
		
		private Zweistein.SpreadExitStrategyPtr2Members ptr2members = null;
		bool IsBrowsableBarsRequiredToTrade=false;
		bool IsExpandedBarsRequiredToTrade=true;
     
        void FillGrid(List<SpreadOrderTicket> Tickets)
        {
          
		  	Parameters.SelectedObject = _strategy;
			foreach (PropertyItem p in Parameters.Properties)
            {
				try {
              		 DisplayAttribute v = p.GetAttribute<DisplayAttribute>();
					if(v==null) continue;
					if(v!=null && v.GroupName=="RuntimeEditable") continue;
					
				}
				catch{}
				p.IsBrowsable=false;
				p.IsExpanded=true;
		   }
		//	Parameters.PropertyValueChanged+= Parameters_PropertyValueChanged;
		 
			if (tickets == null) tickets = new ObservableCollection<SpreadOrderTicket>();
			tickets.Clear();
			foreach(SpreadOrderTicket sot in Tickets){
				tickets.Add((SpreadOrderTicket)sot.Clone());
			}
			
            opentickets.ItemsSource = tickets;
			btnAddTicket.IsEnabled=true;

        }
		
		
		 private void Parameters_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
		//	_strategy.Print("Parameters_PropertyValueChanged: "+e.Property.Name);
            
        }
		
        void SetPositionTextAndValue(string txt,double val)
        {
		    this.btnPosition.Content = txt;
            this.btnValue.Content = val.ToString("0.00");

        }

        void SetUnrealizedPNL(double pnl)
        {
            if (pnl > 0) unrealizedPNL.Foreground = QtyLongBackgroundBrush;
            else unrealizedPNL.Foreground = QtyShortBackgroundBrush;
            unrealizedPNL.Content = currencyIsPrefix?currencySymbol+" "+pnl.ToString("0.00"):pnl.ToString("0.00")+" "+currencySymbol;

        }


        void SetDetailsPnl(double TotalRealized,string priceprefix,double currentprice,List<double> pnlticket,List<string> lines)
        {
            doc.Blocks.Clear();
            Paragraph p = new Paragraph(new Bold(new Run("Realized=" + TotalRealized.ToString("0.00")+currencySymbol+"\r\t" + priceprefix + currentprice.ToString("0.00"))));
           for(int i = 0; i < pnlticket.Count; i++)
            {
				p.Inlines.Add("\rPNL=");
				if(currencyIsPrefix) p.Inlines.Add(currencySymbol);
				p.Inlines.Add(new Run(pnlticket[i].ToString("0.00")){ Foreground = pnlticket[i]>0?QtyLongBackgroundBrush:QtyShortBackgroundBrush });
                p.Inlines.Add(currencyIsPrefix?"":" "+currencySymbol+lines[i]);
            }
  			doc.Blocks.Add(p);
            realizedPNL.Document = doc;
        }
        


      //  public void fromStrategy(SpreadExitStrategy strategy)
		 void fromStrategy(StrategyBase strategy)
        {
            _strategy = strategy;
			ptr2members=new Zweistein.SpreadExitStrategyPtr2Members(strategy);
			
            try { 
              
            btnGoLong.Click += new RoutedEventHandler(ptr2members.GoLong_Click);
            btnGoShort.Click += new RoutedEventHandler(ptr2members.GoShort_Click);
            btnReverse.Click += new RoutedEventHandler(ptr2members.Reverse_Click);
            btnClose.Click += new RoutedEventHandler(ptr2members.Close_Click);
			if(_strategy.Account.Name==NinjaTrader.Cbi.Account.SimulationAccountName){
				Brush bg= NinjaTrader.Core.Globals.TradingOptions.SimulationColor;
				Background=bg;
			}
			if(_strategy.Account.Provider==Provider.InteractiveBrokers && _strategy.Account.Name.StartsWith("DU")){
				
				Brush bg= NinjaTrader.Core.Globals.TradingOptions.SimulationColor;
				Background=bg;
			}
			
           }
        catch{}
            
        }

        

#if NET35==false
         static SpreadTraderWindow CreateFromBaml(Assembly localAssembly)
        {
			SpreadTraderWindow obj=null;
			try

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
            obj = System.Windows.Markup.XamlReader.Load(reader) as SpreadTraderWindow;

            
                LinearGradientBrush btnbackgroundBrush = Application.Current.TryFindResource("SuperDom.ButtonBackground") as LinearGradientBrush;
				
                obj.btnReverse.Background = btnbackgroundBrush;
                obj.btnClose.Background = btnbackgroundBrush;
                obj.btnGoLong.Background = btnbackgroundBrush;
                obj.btnGoShort.Background = btnbackgroundBrush;
				QtyLongBackgroundBrush = Application.Current.TryFindResource("BasicEntry.PositionQtyLongBackground") as SolidColorBrush;
        	    QtyShortBackgroundBrush = Application.Current.TryFindResource("BasicEntry.PositionQtyShortBackground") as SolidColorBrush;
        		SolidColorBrush PnLBackgroundBrush = Application.Current.TryFindResource("BasicEntry.PnLBackground") as SolidColorBrush;
                obj.unrealizedPNL.Background = PnLBackgroundBrush;
				
				obj.btnAddTicket.IsEnabled=false;

            }
            catch(Exception ex) {
				Zweistein.Diagnostics.Trace(ex.Message);	
			}

            
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
		private const int WM_SYSTEMMENU = 0xa4;
        private const int WP_SYSTEMMENU = 0x02;
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
			   	case WM_SYSTEMMENU:
                    {
                        if (wParam.ToInt32() == WP_SYSTEMMENU)
                        {

                            ShowContextMenu();
                            handled = true;
                        }

                    }
                    break;
            }
            return IntPtr.Zero;
        }

	
        
      
        public void Destroy()
        {
			/*
			if(Parameters!=null && Parameters.Properties!=null) {
				foreach( PropertyItem p in Parameters.Properties){
					if(p.Name=="BarsRequiredToTrade") {
						p.IsBrowsable=IsBrowsableBarsRequiredToTrade;
						p.IsExpanded = IsExpandedBarsRequiredToTrade;
					}
				}
				
				Parameters.SelectedObject=null;	
				
			}
			*/
		    allowClosing = true;
            Close();
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
         
            Window w = sender as Window;
			if(_strategy!=null) {
            PropertyInfo pi=_strategy.GetType().GetProperty("Left");
				pi.SetValue(_strategy, w.Left);
				pi=_strategy.GetType().GetProperty("Top");
				pi.SetValue(_strategy, w.Top);
           
			}
        }
   
		
			
		
		
        public void DeleteTicket(SpreadOrderTicket sot)
        {
			
			
            lock (tickets)
            {
                try
                {
					tickets.Remove(sot);
		         }
                catch (Exception ex) {
				_strategy.Print(ex.Message);	
				}
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
           object ieh= ptr2members.IExitHandlingInstance();
			string txt="";
			txt+=txtTicket.Text;
		    if(ieh!=null) txt=txtTicket.Text+"|"+ptr2members.IExitHandlingInstance().ToString();
			
            SpreadOrderTicket sot = SpreadOrderTicket.Create(txt);
            if (sot != null) AddTicket(sot);

        }

       

        private void btnDelTicket_Click(object sender, RoutedEventArgs e)
        {
            
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    SpreadOrderTicket tmp = row.DataContext as SpreadOrderTicket;
                    if (tmp != null) Dispatcher.Invoke(new Action(()=> DeleteTicket(tmp)));
                        break;
                }
        }
    }
}

#region Using declarations
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Gui;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Gui.SuperDom;
using NinjaTrader.Gui.Tools;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript.Indicators;
using NinjaTrader.NinjaScript.DrawingTools;
using System.Windows.Controls.WpfPropertyGrid;
using Test.Zweistein.Spreads;
using System.Windows.Threading;
using Infragistics.Windows.DataPresenter;
using System.Reflection;

#endregion

namespace Test.Zweistein.Spreads
{
    public enum LegEntryType
    {
        Market = 0,
        JoinBidOrAsk,
       // Payup1Tick
    }
	public class BWindow:Window {
		internal PropertyGrid _pg;
		bool destroycalled=false;
		NinjaTrader.Cbi.Currency _currency;
		public BWindow(){
		}
		private bool _componentloaded=false;
		public void setCurrency(NinjaTrader.Cbi.Currency currency){
			_currency=currency;
			
		}
		public void InitializeComponent(){
			
			if(_componentloaded) return;
			_pg=new PropertyGrid();
			this.Content=_pg;
			Height=450;
			Width=200;
				
			_componentloaded=true;
		}
		public void FillPG(NinjaTrader.NinjaScript.StrategyBase _strategy){
			
			_pg.SelectedObject=_strategy;
			foreach (PropertyItem p in _pg.Properties)
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
	}
   }
}
//This namespace holds Strategies in this folder and is required. Do not change it. 
namespace NinjaTrader.NinjaScript.Strategies
{
	public class ShowWpfPropertyGridError2 : Strategy
	{
		BWindow _a=null;
		LegEntryType leg1entrytype=LegEntryType.Market;
		    private NinjaTrader.Cbi.Instrument leg2instrument;
		
		// int tid=0;
        //Infragistics.Windows.DataPresenter.XamDataGrid xamdg;
        bool bconfig=false;
		bool flag=false;
        protected override void OnStateChange()
		{
			int threadid=System.Threading.Thread.CurrentThread.ManagedThreadId;
			//Print("State="+State.ToString()+", id="+this.Id.ToString()+", mtid="+threadid.ToString());
			if (State == State.SetDefaults)
			{
				Description									= @"Enter the description for your new custom Strategy here.";
				Name										= "ShowWpfPropertyGridError2";
				Calculate									= Calculate.OnBarClose;
				EntriesPerDirection							= 1;
				EntryHandling								= EntryHandling.AllEntries;
				IsExitOnSessionCloseStrategy				= true;
				ExitOnSessionCloseSeconds					= 30;
				IsFillLimitOnTouch							= false;
				MaximumBarsLookBack							= MaximumBarsLookBack.TwoHundredFiftySix;
				OrderFillResolution							= OrderFillResolution.Standard;
				Slippage									= 0;
				StartBehavior								= StartBehavior.WaitUntilFlat;
				TimeInForce									= TimeInForce.Gtc;
				TraceOrders									= false;
				RealtimeErrorHandling						= RealtimeErrorHandling.StopCancelClose;
				StopTargetHandling							= StopTargetHandling.PerEntryExecution;
				BarsRequiredToTrade							= 20;
				// Disable this property for performance gains in Strategy Analyzer optimizations
				// See the Help Guide for additional information
				IsInstantiatedOnEachOptimizationIteration	= true;
				TradingHoursSerializable="Default 24 x 7";
				StartBehavior = StartBehavior.ImmediatelySubmit;
			}
			else if (State == State.Configure)
			{
				
				//Print("State="+State.ToString()+", BarsInProgress="+BarsInProgress.ToString());
				AddDataSeries(strLeg2Instrument,BarsPeriod.BarsPeriodType,BarsPeriod.Value);
           		
				bconfig=true;
				/*
				NinjaTrader.Gui.NinjaScript.StrategiesGrid sg=Zweistein.NinjaTraderExtension.StrategiesGrid();
				foreach(CommandBinding cb in sg.CommandBindings)
                {
                    if(cb.Command == NinjaTrader.Gui.NinjaScript.StrategyCommands.EnableDisableSingleStrategyCommand)
                    {
                       cb.CanExecute += Cb_CanExecute;
                    }
                }

               // xamdg=Zweistein.NinjaTraderExtension.StrategiesNTGrid();
             */
                    
			}
			else if(State==State.DataLoaded){
				if (_a == null) 
				{
					Core.Globals.RandomDispatcher.Invoke(new Action(() => 
					{
						_a = new BWindow();
						if (_a != null)
						{
							_a.Dispatcher.Invoke(new Action(()=> _a.InitializeComponent()));	
							_a.Dispatcher.Invoke(new Action(()=> _a.Show()));	
						   _a.Dispatcher.Invoke(new Action(()=> _a.setCurrency(Account.Denomination)));

						}
						
						flag = true;
					}));
				}
				
			}
			
			else if(State==State.Transition){
				
			//	Print("State="+State.ToString()+", BarsInProgress="+BarsInProgress.ToString());
				
				if(_a!=null) {
					_a.Dispatcher.Invoke(new Action(() =>_a.FillPG(this)));
					
				}
				
				
			}
		    else if(State==State.Terminated ){
				
				CloseWindow();
				if(bconfig){
					//Print("State="+State.ToString()+", BarsInProgress="+BarsInProgress.ToString());
					/*
					NinjaTrader.Gui.NinjaScript.StrategiesGrid sg=Zweistein.NinjaTraderExtension.StrategiesGrid();
				foreach(CommandBinding cb in sg.CommandBindings)
                {
                    if(cb.Command == NinjaTrader.Gui.NinjaScript.StrategyCommands.EnableDisableSingleStrategyCommand)
                    {
                       cb.CanExecute -= Cb_CanExecute;
                    }
                }
					*/
				}
				
				
			}
		}
/*
        private void Cb_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
			if(e.Parameter==null) return;
			e.CanExecute=true;
		    object[]  parameter= (object[]) e.Parameter ;
		
			
         	NinjaTrader.Gui.NinjaScript.StrategiesGridEntry ge=   parameter[0] as NinjaTrader.Gui.NinjaScript.StrategiesGridEntry;
					
			System.Windows.Controls.CheckBox cb=parameter[1] as System.Windows.Controls.CheckBox;
			Print("Cb_PreviewExecuted: BarsInProgress="+BarsInProgress.ToString()+" Strategy.State:"+ge.Strategy.State.ToString()+" "+cb.IsChecked.ToString());
			
			if(ge.Strategy==this && ge.Strategy.State==State.Realtime && cb.IsChecked==false ) {
				// Print("our strategy, CloseWindow()");
				//CloseWindow();
			}
			
        }

  */      
		
		public void CloseWindow(){
			
			bool basync=false;
			
			if (basync && _a!=null) 
				{
					
					_a.Dispatcher.InvokeAsync(new Action(()=> _a.Close()));	
				
				}
			
			if (!basync && _a!=null && flag) 
				{
					
					_a.Dispatcher.Invoke(new Action(()=> _a.Close()));	
					
					_a=null;
					flag=false;
				}
		
				
		}
		
		protected override void OnBarUpdate()
		{
			//Add your custom strategy logic here.
		}
		
		
		
		public int AA {get;set;}
		
		[Display( Name = "Leg1 Entry Type", Description = "", GroupName = "RuntimeEditable", Order = 3)]
		public LegEntryType Leg1EntryType
        {
            get {return leg1entrytype; }
            set { 	leg1entrytype = value; }
        }
			
		
		[Browsable(false)]
		public string strLeg2Instrument {get;set;}
		[EditorBrowsable(EditorBrowsableState.Always)]
    	[XmlIgnore]
    	//[Display(GroupName = "NinjaScriptDataSeries", Name = "Instrument 2", Order = 100, ResourceType = typeof (Resource))]
		[Display( Name = "Instrument 2", Description = "Leg 2 o Spread", GroupName = "Data Series", Order = 0)]
		public  NinjaTrader.Cbi.Instrument Leg2Instrument
        {
            get {return leg2instrument;}
            set {
				leg2instrument=value;
				if(value!=null) strLeg2Instrument=leg2instrument.FullName;
                }
		}
	}
}

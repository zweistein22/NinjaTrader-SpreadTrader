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
using System.Windows.Documents;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
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
using Zweistein.Spreads;
using Zweistein;
using System.Diagnostics;
#endregion


//This namespace holds Strategies in this folder and is required. Do not change it. 
namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class SpreadExitStrategy : Strategy //NinjaTrader.Gui.NinjaScript.StrategyRenderBase
    {
		/*
        private Indicator indicator;

        public SpreadExitStrategy()
        {
            lock (NinjaScripts)
                NinjaScripts.Add(indicator = new Indicator { Parent = this });
        }
		*/ 
        private NinjaTrader.Cbi.Instrument leg2instrument;

        
		LegEntryType leg1entrytype=LegEntryType.Market;
		LegEntryType leg2entrytype=LegEntryType.Market;
		LegExitType leg1exittype=LegExitType.JoinBidOrAsk;
		LegExitType leg2exittype=LegExitType.JoinBidOrAsk;
		string pricestring="Price=";
		private int leg1Unit=1;
		private int leg2Unit=1;
		string strratio= "1:1";
		double leg1pricemultiplier = 1.0;
        double leg2pricemultiplier = 1.0;
		private SpreadTraderWindow stw=null;
		double realizedpnl=0;
		double netunits=0;
		
		PNLCalculatedBy pnlby=PNLCalculatedBy.Last;
			double price0=0;
		double price1=0;
		double pnl0=0;
		double pnl1=0;
		Queue<SingleSpread> spreads=new Queue<SingleSpread>();
		
		bool baccount_exeupdate=false;
	
		List<SpreadOrderTicket> _tickets= new List<SpreadOrderTicket>();
		
		protected override void OnStateChange()
		{
			try {
            if (State == State.SetDefaults)
            {
				if(TraceOrders) Print(this.ToString()+", State="+State.ToString());
			    Description = @"Base Strategy for SpreadTrader, do no edit!";
                Name = "SpreadExitStrategy";
				Zweistein.SpreadTraderWindow.StrategySetDefaults(this);
				        	
				Units=1;
				Calculate=Calculate.OnPriceChange;
				base.BarsRequiredToTrade = 0;		
				Left=-1;
				Top=-1;
		    }
            else if (State == State.Configure)
            {
				AddDataSeries(strLeg2Instrument,BarsPeriod.BarsPeriodType,BarsPeriod.Value);
				Zweistein.Money.UpdateRates();
			}
			else if(State==State.DataLoaded){
				if(stw==null) {
                    string tmp = "";
                    for(int i = 0; i < 3; i++){
                            tmp = (string) DisplayName.Clone();
                            if (tmp.Contains("?"))
                            {
                                Zweistein.NinjaTraderLog.Process("SpreadExitstrategy.OnStateChange():", "tmp=" + tmp, LogLevel.Warning, LogCategories.Strategy);
                                System.Threading.Thread.Sleep(500);
                            }
                            else break;
                   }
                    stw =Zweistein.SpreadTraderWindow.StrategyDataLoaded(this,Left,Top,spreads,tmp);
				   if(TraceOrders) Print("stw="+stw!=null?tmp+":"+stw.ToString():"null");
				}
			}
			else if(State == State.Transition ){
				if(string.IsNullOrEmpty(FirstLoadUTC) ){
					FirstLoadUTC = DateTime.Now.ToUniversalTime().ToString("o");
               }
				if(TraceOrders) {
					Print(State.ToString()+" "+FirstLoadUTC);
					Print(spreads.Count.ToString()+" Spreads found");
				}
				baccount_exeupdate=true;
                Account.ExecutionUpdate += Account_ExecutionUpdate;
                if (stw!=null) Zweistein.SpreadTraderWindow.StrategyTransition(stw,spreads,Tickets);
				
			}
			else if(State == State.Terminated){
				if(Account!=null &&  baccount_exeupdate==true)  Account.ExecutionUpdate-=Account_ExecutionUpdate;
				Zweistein.SpreadTraderWindow.StrategyTerminated(stw,spreads,Tickets);
			
			}
			
			}
			catch(Exception ex){
			Print("State.:"+ex.Message);
			}
			
		}
		
		 private void Account_ExecutionUpdate(object sender, ExecutionEventArgs e)
        {
			if(e.Execution==null) return;
			if(e.Execution.Instrument==null) return;
			if(Instrument==null) return;
			if(Leg2Instrument==null) return;
			
	        if(!(e.Execution.Instrument.FullName==Instruments[0].FullName || e.Execution.Instrument.FullName== Instruments[1].FullName))
            {
                Zweistein.NinjaTraderLog.Process("Account_ExecutionUpdate:e.Execution.Instrument.FullName=", e.Execution.Instrument.FullName, LogLevel.Warning, LogCategories.Strategy);
                Zweistein.NinjaTraderLog.Process("Account_ExecutionUpdate:         Instrument[0].FullName=", Instruments[0].FullName, LogLevel.Warning, LogCategories.Strategy);
                Zweistein.NinjaTraderLog.Process("Account_ExecutionUpdate:         Instrument[1].FullName=", Instruments[1].FullName, LogLevel.Warning, LogCategories.Strategy);

                return;

            }
            bool isours = false;
            lock (spreads) {
                if (spreads != null){
                    foreach (SingleSpread spread in spreads)
                    {
                        if (spread.IsOurExecution(e.Execution))
                        {
                            isours = true;
                            break;
                        }
                    }
                }
            }
            if(!isours)  {
                lock (spreads)
                {
                    spreads.Clear();
                }
			      Zweistein.NinjaTraderLog.Process("Account_ExecutionUpdate:","spreads.Clear()", LogLevel.Information, LogCategories.Strategy);
                  Zweistein.NinjaTraderExtension.DisableStrategy(this);
			  
		      }
			
		}

     

        public override string DisplayName
		{
 			 get { 
				 string inst="Spread "+QuantityRatio+" ";
			     if(Instrument!=null && !string.IsNullOrEmpty(Instrument.FullName)){
                    if (Leg1PriceDisplayMultiplier != 1) inst += " (" + Leg1PriceDisplayMultiplier.ToString() + ")";
                    inst +=Instrument.FullName;

			     }
			     else inst+="?";
			     inst+=" - ";
			     if(strLeg2Instrument != null  && !string.IsNullOrEmpty(strLeg2Instrument)){
                    if (Leg2PriceDisplayMultiplier != 1) inst += " (" + Leg2PriceDisplayMultiplier.ToString() + ")";
                    inst += strLeg2Instrument;
			     }
			     else inst+="?";
				 return inst;
            }
		}
 


	   public virtual object IExitHandlingInstance(){
			return null;
		
		}
	   
	   
	   public override object Clone(){
		   
		   SpreadExitStrategy ons=null;
		  
		   	ons= base.Clone() as SpreadExitStrategy;
				   
		    if(ons==null) {
				string error="SpreadExitStrategy.Clone() failed, duplicate versions in memory? Restart NinjaTrader to fix!";
				Log(error,LogLevel.Error);
                IsEnabled=false;
				throw new ArgumentException(error);
			}
			
			Queue<SingleSpread> tmp=new Queue<SingleSpread>();
			// we want deep copy of Spread
			foreach(SingleSpread s in this.Spreads){
				tmp.Enqueue((SingleSpread)s.Clone());
			}
			ons.Spreads= tmp.ToArray();
			ons.QuantityRatio=this.QuantityRatio;
			ons.Leg1EntryType=this.Leg1EntryType;
			ons.Leg2EntryType=this.Leg2EntryType;
			ons.Leg1ExitType=this.Leg1ExitType;
			ons.Leg2ExitType=this.Leg2ExitType;
			//ons.Leg2Instrument=this.Leg2Instrument;  done only by strLeg2Instrument!!!
			ons.FirstLoadUTC=this.FirstLoadUTC;
			ons.Units=this.Units;
			ons.strLeg2Instrument=this.strLeg2Instrument;
			ons.Left=this.Left;
			ons.Top=this.Top;
			ons.Realizedpnl=this.Realizedpnl;
			List<SpreadOrderTicket> sot=new List<SpreadOrderTicket>();
			foreach(var v in this.Tickets) sot.Add((SpreadOrderTicket)v.Clone());
			ons.Tickets=sot;
				
			return ons;
		}
	   
	   protected override void OnOrderUpdate(Order order, double limitPrice, double stopPrice, int quantity, int filled, double averageFillPrice, OrderState orderState, DateTime time, ErrorCode error, string comment)
{
 			try {
				foreach(SingleSpread spread in spreads){
					spread.OnOrderUpdate(order);
				}
				CleanupQueue();
			}
			catch(Exception e){
				if(TraceOrders) Print(e.Message+" STACKTRACE: "+e.StackTrace);	
			}
}

	 
	   
	   	void CleanupQueue(){
			if(State!=State.Realtime) return;
			realizedpnl+=Zweistein.SpreadTraderWindow.StrategyUpdateSpreads(spreads);
			netunits=Zweistein.SpreadTraderWindow.StrategySetPositionAndEntryLabels(stw,spreads);
			
		}
		bool bspal=false;
		protected override void OnBarUpdate()
		{
			if(State!=State.Realtime) return;
		
			if(!bspal) {
				bspal=true;
				if(TraceOrders){foreach(SingleSpread s in spreads) Print(s.ToString());}
				 netunits=Zweistein.SpreadTraderWindow.StrategySetPositionAndEntryLabels(stw,spreads);
			}
           
			
			double val=DisplayMarketPrice(); // keep it here, updates internal data
			Zweistein.SpreadTraderWindow.UpdateForm(stw,spreads,realizedpnl,pricestring,val);
            if (stw == null) throw new ArgumentException("Zweistein.SpreadTrader: stw==null, License error?");
            if (stw.tickets == null) return;
			Zweistein.SpreadTraderWindow.StrategyUpdateTickets(this,stw,spreads,val,strratio,Leg1EntryType,Leg2EntryType,IExitHandlingInstance());
			
		}
		
				
		public double MarketPrice0(int net){
			if(net==0 || pnlby==PNLCalculatedBy.Last)  return Closes[0][0];
#if LEVEL2_DATA	

			if(net>0)	return biddepth[0].AvgFillPrice(-net);
			if(net<0) return askdepth[0].AvgFillPrice(-net);
#endif
			return 0;
			
		}
		
		public double MarketPrice1(int net){
			if(net==0 || pnlby==PNLCalculatedBy.Last)  return Closes[1][0];
#if LEVEL2_DATA	
	
			if(net>0)	return askdepth[1].AvgFillPrice(net);
			if(net<0) return biddepth[1].AvgFillPrice(net);
#endif
			return 0;
		}
		double MarketPrice(){
			
			price0=MarketPrice0((int)netunits*leg1Unit);
			price1=MarketPrice1((int)netunits*leg2Unit);
			
			return  price0*leg1Unit-price1*leg2Unit;
			
			
		}

        double DisplayMarketPrice()
        {

            price0 = MarketPrice0((int)netunits * leg1Unit);
            price1 = MarketPrice1((int)netunits * leg2Unit);

            return leg1pricemultiplier * price0 * leg1Unit - leg2pricemultiplier*price1 * leg2Unit;


        }
		 
			 
		protected override void OnExecutionUpdate(NinjaTrader.Cbi.Execution execution, string executionId, double price, int quantity, MarketPosition marketPosition, string orderId, DateTime time)
{
 			try {
				foreach(SingleSpread spread in spreads){
				spread.OnExecution(execution);
			}
			
			CleanupQueue();
			}
			catch(Exception e){
				if(TraceOrders) Print("ERROR: "+e.Message+" STACKTRACE: "+e.StackTrace);	
			}
}
       



	private void clickcustomhandler(object state) { 
		   
			ClickhandlerArgs cha=(ClickhandlerArgs) state;
			if(TraceOrders) Print("clickcustomhandler("+cha.Units.ToString()+","+cha.ClickedButton.ToString()+")");
			try {
				Zweistein.SpreadTraderWindow.StrategyHandleClickedButton(this,cha,spreads,strratio,leg1entrytype,leg2entrytype,IExitHandlingInstance());
			}
			catch(Exception e){
				if(TraceOrders) Print("ERROR: "+e.Message+ " STACKTRACE:"+e.StackTrace);	
			}
   		}
      public void Close_Click(object sender, System.EventArgs e)
        {
			ClickhandlerArgs A=new ClickhandlerArgs(ClickedButton.Close,this.Units);
			clickcustomhandler(A);
        }
        public void Reverse_Click(object sender, System.EventArgs e)
        {
			ClickhandlerArgs A=new ClickhandlerArgs(ClickedButton.Reverse,this.Units);
			clickcustomhandler(A);
		}
        public void GoShort_Click(object sender, System.EventArgs e)
        {
			ClickhandlerArgs A=new ClickhandlerArgs(ClickedButton.GoShort,this.Units);
			clickcustomhandler(A);
     	}
        public void GoLong_Click(object sender, System.EventArgs e)
        {
			ClickhandlerArgs A=new ClickhandlerArgs(ClickedButton.GoLong,this.Units);
            clickcustomhandler(A);
        }
	
	
	 
	}
}

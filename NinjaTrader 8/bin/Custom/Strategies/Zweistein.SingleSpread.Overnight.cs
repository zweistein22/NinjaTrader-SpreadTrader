#region Using declarations
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using NinjaTrader.NinjaScript.Strategies;
using Zweistein;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.ObjectModel;
using System.Linq;
#endregion


namespace Zweistein.Spreads {
	[Serializable()] 
    public partial class SingleSpread : IDisposable,ISerializable,ICloneable
    {
		List<string>[] exectokens=new List<string>[]{new List<string>(),new List<string>()};
		List<NinjaTrader.Cbi.Execution>[] execs=new List<NinjaTrader.Cbi.Execution>[]{new List<NinjaTrader.Cbi.Execution>(),new List<NinjaTrader.Cbi.Execution>()};
		Queue<Order>[] queue=new Queue<Order>[]{new Queue<Order>(),new Queue<Order>()};
		
		string iehdata="";
		
		
        double[] avg_fill=new double[]{0,0};
		public SingleSpread(SerializationInfo info, StreamingContext ctxt){
			Lots1 = (int)info.GetValue("Lots1", typeof(int));
		    Lots2 = (int)info.GetValue("Lots2", typeof(int));
			MarketPosition = (MarketPosition)info.GetValue("MarketPosition", typeof(MarketPosition));
		    NUnits = (int)info.GetValue("NUnits", typeof(int));
			Leg1Tokens = (List<string>)info.GetValue("Leg1Tokens", typeof(List<string>));
		    Leg2Tokens = (List<string>)info.GetValue("Leg2Tokens", typeof(List<string>));
			IEHData = (string)info.GetValue("IEHData", typeof(string));
			BaseCurrency=(NinjaTrader.Cbi.Currency) info.GetValue("BaseCurrency",typeof(NinjaTrader.Cbi.Currency));
			ConvFactor0=(decimal)info.GetValue("ConvFactor0",typeof(decimal));
			ConvFactor1=(decimal)info.GetValue("ConvFactor1",typeof(decimal));
			Guid=(Guid)info.GetValue("Guid",typeof(Guid));
			//Avg_fill1=(double) info.GetValue("Avg_fill1",typeof(double));
			//Avg_fill2=(double) info.GetValue("Avg_fill2",typeof(double));
			Totalprice0=(double) info.GetValue("Totalprice0",typeof(double));
			Totalprice1=(double) info.GetValue("Totalprice1",typeof(double));
		}
	
		public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
		{
    	//You can use any custom name for your name-value pair. But make sure you
    	// read the values with the same name. For ex:- If you write EmpId as "EmployeeId"
    	// then you should read the same with "EmployeeId"
    		info.AddValue("Lots1", Lots1);
    		info.AddValue("Lots2", Lots2);
			info.AddValue("MarketPosition", MarketPosition);
			info.AddValue("NUnits",NUnits);
			info.AddValue("Leg1Tokens",Leg1Tokens);
			info.AddValue("Leg2Tokens",Leg2Tokens);
			info.AddValue("IEHData",IEHData);
			info.AddValue("BaseCurrency",BaseCurrency);
			info.AddValue("ConvFactor0",ConvFactor0);
			info.AddValue("ConvFactor1",ConvFactor1);
			info.AddValue("Guid",Guid);
			//info.AddValue("Avg_fill1",Avg_fill1);
			//info.AddValue("Avg_fill2",Avg_fill2);
			info.AddValue("Totalprice0",Totalprice0);
			info.AddValue("Totalprice1",Totalprice1);
			}
		
		
		
		
		static DateTime _Now {
			get {
				/*
				try {
				if(NinjaTrader.Globals.ReplayConnection.Status==ConnectionStatus.Connected){
					return NinjaTrader.Cbi.Globals.ReplayConnection.Now;
				}
				}
				catch{}
				*/
				return DateTime.Now;
			}
		}
		
		public object Clone(){
			SingleSpread s = new SingleSpread();
			s.Lots1=this.Lots1;
			s.Lots2=this.Lots2;
			s.MarketPosition=this.MarketPosition;
			s.NUnits=this.NUnits;
			s.Leg1Tokens=this.Leg1Tokens;
			s.Leg2Tokens=this.Leg2Tokens;
			s.IEHData=this.iehdata;
			s.ConvFactor0=this.ConvFactor0;
			s.ConvFactor1=this.ConvFactor1;
			s.Guid=this.Guid;
			s.BaseCurrency=this.BaseCurrency;
			s.Totalprice0=this.Totalprice0;
			s.Totalprice1=this.Totalprice1;
			return s;      // call clone method
   		}

		public SingleSpread(){
				if(exectokens==null) exectokens=new List<string>[]{new List<string>(),new List<string>()};
				if(execs==null) execs=new List<NinjaTrader.Cbi.Execution>[]{new List<NinjaTrader.Cbi.Execution>(),new List<NinjaTrader.Cbi.Execution>()};
				if(queue==null) queue=new Queue<Order>[]{new Queue<Order>(),new Queue<Order>()};
				entry=new HandledOrder[]{new HandledOrder(strategybase),new HandledOrder(strategybase)};
				exit=new HandledOrder[]{new HandledOrder(strategybase),new HandledOrder(strategybase)};

		}



        public void CollectTokens(NinjaTrader.Cbi.Execution execution) {
            lock (exectokens[0])
            {
                if (strategybase.TraceOrders) strategybase.Print("CollectTokens");
                int sign = (execution.MarketPosition == MarketPosition.Long) ? 1 : -1;

                if (entry[0].Order != null && execution.Order.OrderId == entry[0].Order.OrderId)
                {
                    exectokens[0].Add(execution.OrderId);
                    if (positionX == 0) exectokens[0].Clear();
                }

                if (exit[0].Order != null && execution.Order.OrderId == exit[0].Order.OrderId)
                {
                    exectokens[0].Add(execution.OrderId);
                    if (positionX == 0) exectokens[0].Clear();
                }
            }
            lock (exectokens[1])
            {
                if (entry[1].Order != null && execution.Order.OrderId == entry[1].Order.OrderId)
                {
                    exectokens[1].Add(execution.OrderId);
                    if (positionY == 0) exectokens[1].Clear();
                }

                if (exit[1].Order != null && execution.Order.OrderId == exit[1].Order.OrderId)
                {
                    exectokens[1].Add(execution.OrderId);
                    if (positionY == 0) exectokens[1].Clear();
                }
            }
		}
		
	
		
		//NinjaTrader.Cbi.Execution[] netexec;
		
		public double AvgFill(int i){
			
			return avg_fill[i];
		}
		
		
		public void RebuildPositions(NinjaTrader.NinjaScript.StrategyBase strategy){
			
			 DateTime t=NinjaTrader.Core.Globals.Now;
			
			//SetBaseCurrency(strategy.Account.Denomination);
			
			PropertyInfo pi=strategy.GetType().GetProperty("FirstLoadUTC");
			string FirstLoadUTC = (string) pi.GetValue(strategy);
			
			DateTime tmp=DateTime.Parse(FirstLoadUTC);
			DateTime mintime=new DateTime(tmp.Year,tmp.Month,tmp.Day,tmp.Hour,tmp.Minute,tmp.Second);
			mintime=mintime.AddHours(-1);
			
			for(int i=0;i<2;i++){
				 	int n = 0;
            		double avg = 0;
					
				    //if(t.AddDays(-2) <mintime) mintime=t.AddDays(-2);
				 //   strategy.Print("RebuildPositions: mintime:"+ mintime.Kind.ToString() + " t:"+t.Kind.ToString());
				 //   strategy.Print("RebuildPositions:"+ mintime.ToString());
					System.Collections.ObjectModel.Collection<NinjaTrader.Cbi.Execution>  ce=			
					NinjaTrader.Cbi.Execution.DbGet(strategy.Account,strategy.Instruments[i],mintime,t);
			    	foreach(string s in exectokens[i]){
				      NinjaTrader.Cbi.Execution exec=ce.FirstOrDefault(x=>x.OrderId==s);
				      if(exec!=null) {
						if(i==0 && exec.MarketPosition==mleg1 || i==1 && exec.MarketPosition!=mleg1 ) {
							// we add averaging
							n+=exec.Quantity;
                       		avg+=exec.Price*exec.Quantity;
						}
						else {
							n-=exec.Quantity;
                       		avg-=exec.Price*exec.Quantity;
						}
						execs[i].Add(exec);
						strategy.Executions.Add(exec);
						strategy.Positions[i].AddExecution(exec);
   					}
			}
			
			int u=0;
			if(mleg1==MarketPosition.Long) u=1;
			if(mleg1==MarketPosition.Short) u=-1;
			if(i==0) positionX=1*u*n;
			if(i==1) positionY=-1*u*n;
			
			if(n!=0) avg_fill[i]=avg/(double)n;
			else avg_fill[i]=0;
			
			
			
		}
	}
		public override string ToString(){
			string tmp="Spread(Units="+this.NUnits.ToString()+"@";
			tmp+=this.EntryPricePerUnit(1,1).ToString()+")";
			tmp+= "Leg1("+positionX.ToString()+"@"+avg_fill[0].ToString()+")";
			tmp+= ", Leg2("+positionY.ToString()+"@"+avg_fill[1].ToString()+")";
			
			return tmp;
		}
		
		#region properties
		/*
		[Browsable(false)]
		public double Avg_fill1 {
			get {return avg_fill[0];}
			set {avg_fill[0]=value;}
			
		}
			[Browsable(false)]
		public double Avg_fill2 {
			get {return avg_fill[1];}
			set {avg_fill[1]=value;}
			
		}
		*/
		
		
		public List<string> Leg1Tokens {
			get {return exectokens[0];}
			set {
				if(exectokens==null) exectokens=new List<string>[]{new List<string>(),new List<string>()};
				exectokens[0]=value;
			}
		}
		
		public List<string> Leg2Tokens {
			get {return exectokens[1];}
			set {
				if(exectokens==null) exectokens=new List<string>[]{new List<string>(),new List<string>()};
				exectokens[1]=value;
			}
		}
		
		
		
		
		
		public int Lots1 {
			get {return lots1;}
			set {lots1=value;}
			
		}
		public int Lots2 {
			get {return lots2;}
			set {lots2=value;}
			
		}
		
        [Browsable(false)]
        public string IEHData
        {
            get {
				
               
				
				return iehdata; }
            set {
				iehdata=value ; 
			}
        }
		#endregion
		
		
	}
	
	
	
}
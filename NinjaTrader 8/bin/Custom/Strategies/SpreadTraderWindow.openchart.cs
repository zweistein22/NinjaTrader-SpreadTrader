using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using NinjaTrader.NinjaScript;
using NinjaTrader.NinjaScript.Strategies;
using NinjaTrader.Cbi;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;
using System.Windows.Controls.WpfPropertyGrid;
using System.Threading;
using System.Runtime.InteropServices;

namespace Zweistein
{
	

	
	

public partial class SpreadTraderWindow : Window
    {
		
		private void ShowContextMenu()
        {
            if (SystemMenu != null)
            {
                SystemMenu.IsOpen = true;
            }
        }

		
			  public ContextMenu SystemMenu
        {
            get
            {
                ContextMenu ctxmnu = new ContextMenu();
                MenuItem itm1 = new MenuItem();
                ctxmnu.Items.Add(itm1);
                itm1.Header = "open chart";
                itm1.Click += Itm1_Click;
                return ctxmnu;
            }
        }

        private void Itm1_Click(object sender, RoutedEventArgs e)
        {
        
			Zweistein.FractionString f = new Zweistein.FractionString(ptr2members.QuantityRatio, ':');
			int A = (int)f.Nominator;
            int B = (int)f.Denominator;
			string currenttemplate="";
			try {
				 currenttemplate=TemplateExtensions.SpreadChartTemplate(_strategy.InstrumentOrInstrumentList,ptr2members.strLeg2Instrument,
				A,B,ptr2members.Leg1PriceDisplayMultiplier,ptr2members.Leg2PriceDisplayMultiplier,
				ptr2members.PriceString,_strategy.BarsPeriods[0]);
			}
			catch(Exception ex){
				_strategy.Print(ex.Message);
				return;
			}
			NinjaTrader.Gui.ControlCenter cc=Zweistein.NinjaTraderExtension.ControlCenter();
			
			
			 NinjaTrader.Gui.Chart.BarsDialog BD=new  NinjaTrader.Gui.Chart.BarsDialog();
			if(BD!=null) {
				 MethodInfo mi4 = BD.GetType().GetMethod("SaveLastBarsPeriod", BindingFlags.Instance | BindingFlags.NonPublic);
                   mi4.Invoke(BD,new object[]{_strategy.BarsPeriods[0]});
				//_strategy.Print("BD not null");
					BD=null;
			}
				MethodInfo mi=cc.GetType().GetMethod("OnChart",BindingFlags.Instance|BindingFlags.NonPublic);
				mi.Invoke(cc,new object[]{this,null});

                NinjaTrader.Core.Globals.RandomDispatcher.InvokeAsync( () =>
                {
                    NinjaTrader.Gui.Chart.BarsDialog bdlg = null;
                    for (int i = 0; i < 5; i++)
                    {
                        System.Threading.Thread.Sleep(500);
                        bdlg = Zweistein.NinjaTraderExtension.BarsDialog();
                        if (bdlg != null) {
							 MethodInfo mi3 = bdlg.GetType().GetMethod("SaveLastBarsPeriod", BindingFlags.Instance | BindingFlags.NonPublic);
                       		mi3.Invoke(bdlg,new object[]{_strategy.BarsPeriods[0]});
							break;
						}
                    }
                    if (bdlg != null)
                    {
						
						bdlg.Dispatcher.Invoke(new Action(()=>
						{
						    MethodInfo mi2 = bdlg.GetType().GetMethod("AddDataSeries", BindingFlags.Instance | BindingFlags.NonPublic);
                       		mi2.Invoke(bdlg, new object[] { _strategy.Instruments[0] });
							
							FieldInfo fi2=bdlg.GetType().GetField("propertyGrid", BindingFlags.Instance | BindingFlags.NonPublic);
						    PropertyGrid pg= fi2.GetValue(bdlg) as PropertyGrid;
							
							mi2.Invoke(bdlg, new object[] { _strategy.Instruments[1] });
							 
							
							
							FieldInfo fi3 = bdlg.GetType().GetField("cbxTemplates", BindingFlags.Instance | BindingFlags.NonPublic);
                            ComboBox cbxTemplates = fi3.GetValue(bdlg) as ComboBox;
                         
							int selectedindex=-1;
							int i=-1;
							foreach( object o in  cbxTemplates.ItemsSource){
								i++;
                                NinjaTrader.Gui.Chart.ChartTemplateWrapper w = o as NinjaTrader.Gui.Chart.ChartTemplateWrapper;
                                 if(w==null) continue;
								if (w.DisplayName == currenttemplate) { selectedindex=i; break; }
							}
                            if(selectedindex>=0) cbxTemplates.Dispatcher.Invoke(()=> {
												cbxTemplates.SelectedIndex= selectedindex;
							});
							
							//private readonly BarsProperties preSelected;
							//internal PropertyGrid propertyGrid;
							 FieldInfo fi = bdlg.GetType().GetField("btnOk",BindingFlags.Instance|BindingFlags.NonPublic);
							 Button b = fi.GetValue(bdlg) as Button;
							MethodInfo mi3 = bdlg.GetType().GetMethod("OnOk", BindingFlags.Instance | BindingFlags.NonPublic);
			
							if(b!=null) {
								bdlg.Dispatcher.InvokeAsync(() =>
								{
									mi3.Invoke(bdlg,new object[] { b,null});
								});
								
						
							}
						}
						));
					}

                });
        }

		
		
	}
}

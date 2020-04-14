# NinjaTrader-SpreadTrader
<a href="Zweistein-spreadtrader.pdf">Zweistein-spreadtrader.pdf</a>
Add on for Spread Display and DOM  with exit strategies for Spreads for www.NinjaTrader.com NinjaTrader 8.

To install SpreadTrader please follow these steps.

1.) Install Additional references in NinjaTrader 8:

 *ProgramFiles*\NinjaTrader 8\bin64\System.Windows.Controls.WpfPropertyGrid.dll
 
 C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Drawing.Design.dll
 
 C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Drawing.dll
 
 C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Data.dll
 
 *ProgramFiles*\NinjaTrader 8\bin64\InfragisticsWPF4.v15.1.dll
 
 *ProgramFiles*\NinjaTrader 8\bin64\InfragisticsWPF4.Editors.v15.1.dll
 
 recompile ninjascript must go through without errors.
 
 2.) Add files in the NinjaTrader 8\ subdirectories to your PC  in  ..\My Documents\NinjaTrader 8\
 Use the same directory structure as here on github and just copy the files to the same locations to your local pc.
 Possible errors are naming conflicts, most likely a Spread indicator. Remove your conflicting scripts.
 
 3.) Read <a href="Zweistein-Spreadtrader.pdf">Zweistein-spreadtrader.pdf</a> and set up NinjaTrader 8 marketdata properly. The script works as a strategy and marketdata will only arrive when markets are open for both instruments of a leg.
 
 Enjoy
 
 Andreas



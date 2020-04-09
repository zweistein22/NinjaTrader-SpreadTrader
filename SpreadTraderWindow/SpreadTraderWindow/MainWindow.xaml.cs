using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Zweistein
{

   
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

      
        SpreadTraderWindow stw = null;
        public MainWindow()
        {
            
           InitializeComponent();
         
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            //if (stw==null) stw = SpreadTraderWindow.CreateFromBaml(assembly);

            //Zweistein.Diagnostics.doSnoopWindow(this);
            if (stw == null)
            {
                stw = new SpreadTraderWindow();
                NinjaTrader.NinjaScript.Strategies.SpreadExitStrategy strategy = new NinjaTrader.NinjaScript.Strategies.SpreadExitStrategy();
                stw.fromStrategy(strategy);
                stw.InitializeComponent();
               
                stw.FillGrid();

                
            }
            if (stw!=null) stw.Show();
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (stw != null) stw.Destroy();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var w = new Window1();
            w.Show();
        }
    }
}

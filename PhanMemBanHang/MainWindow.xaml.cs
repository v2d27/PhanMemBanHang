using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PhanMemBanHang
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static TextBlock ActiveTabID = null;
        private static string ActiveTabUid = "Tab3";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainFrameLoaded(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new QuetMaVach();
        }

        private void Tab_MouseUp(object sender, MouseButtonEventArgs e)
        {

            TextBlock tab = (TextBlock)sender;
            if (ActiveTabID == null)
            {
                TabActive.Background = Brushes.Transparent;
                TabActive.Foreground = Brushes.DimGray;
            }
            if (ActiveTabID != null)
            {
                ActiveTabID.Background = Brushes.Transparent;
                ActiveTabID.Foreground = Brushes.DimGray;
            }

            tab.Background = Brushes.Green;
            tab.Foreground = Brushes.White;
            ActiveTabID = tab;
            switch (tab.Uid)
            {
                case "Tab1":
                    MainFrame.Content = new BarcodeScanPage();
                    break;
                case "Tab2":
                    MainFrame.Content = new NhapDuLieu();
                    break;
                case "Tab3":
                    MainFrame.Content = new QuetMaVach();
                    break;
                case "Tab4":
                    MainFrame.Content = new ThongKe();
                    break;
            }
        }

        private void Tab_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock tab = (TextBlock)sender;
            if (tab == ActiveTabID) return;
            if(ActiveTabID == null)
            {
                if(tab.Uid == ActiveTabUid)
                {
                    return;
                }
            }
            tab.Background = Brushes.WhiteSmoke;
        }

        private void Tab_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBlock tab = (TextBlock)sender;
            if (tab == ActiveTabID) return;
            if (ActiveTabID == null)
            {
                if (tab.Uid == ActiveTabUid)
                {
                    return;
                }
            }
            tab.Background = Brushes.Transparent;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
            if (tab == ActiveTabID)
            {
                return;
            }

            if (ActiveTabID == null)
            {
                TabActive.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2D3E50"));
                TabActive.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFC8D9E9"));
            }
            if (ActiveTabID != null)
            {
                ActiveTabID.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF2D3E50"));
                ActiveTabID.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFC8D9E9"));
            }

            ActiveTabID = tab;
            ActiveTabID.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF27AE61"));
            ActiveTabID.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFECFFEF"));

            // Remove back entry
            while (MainFrame.NavigationService.RemoveBackEntry() != null) ;
            foreach(Process a in Process.GetProcessesByName("CefSharp.BrowserSubprocess"))
            {
                a.Kill();
            }

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
                case "Tab5":
                    MainFrame.Content = new LichCupDienPage();
                    break;
                case "Tab6":
                    MainFrame.Content = new ThiTruongPage();
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
            tab.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF27AE61"));
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
            tab.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFC8D9E9"));
        }
    }
}

using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Media;

namespace PhanMemBanHang
{
    /// <summary>
    /// Interaction logic for ThongKe.xaml
    /// </summary>
    public partial class ThongKe : Page
    {
        public ThongKe()
        {
            InitializeComponent();

            double[] money = Statistics.Chart.Money7days();
            myTodayMoney.Text = money[0].ToString("0,0");
            double total = 0;
            for (int i = 0; i < 7; i++) total += money[i];
            myTotalMoney.Text = total.ToString("0,0");

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Tổng thu nhập",
                    Foreground = Brushes.White,
                    Values = new ChartValues<double> {money[6], money[5], money[4], money[3], money[2], money[1], money[0]},
                    //PointGeometry = DefaultGeometries.Triangle,
                    PointGeometrySize = 15
                }
            };
            Labels = new[] { "6 ngày trước", "5 ngày trước", "4 ngày trước", "3 ngày trước", "2 ngày trước", "Hôm qua", "Hôm nay" };
            YFormatter = value => value.ToString("0,0 VNĐ");
            //modifying any series values will also animate and update the chart
            //SeriesCollection[0].Values.Add(1000);

            //PieChart
            PointLabel = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            double[] v = Statistics.Chart.Bill5days();
            total = 0;
            for(int i = 0; i < 5; i++)
            {
                pieChart.Series[i].Values[0] = v[i];
                total += v[i];
            }
            myTodayBill.Text = v[0].ToString();
            myTotalBill.Text = Statistics.Count().ToString();

            DataContext = this;
        }

        public Func<ChartPoint, string> PointLabel { get; set; }

        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartpoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
    }

    public class Statistics
    {
        private static string StatisticsFile = @".\sources\statistics.manager";
        private static string ReadData()
        {
            if (File.Exists(StatisticsFile) == false)
            {
                File.Create(StatisticsFile);
                return "";
            }
            return File.ReadAllText(StatisticsFile);
        }

        /// <summary>
        /// Return number of today bills in interger type.
        /// </summary>
        /// <returns></returns>
        public static int TodayCount()
        {
            DateTime date = DateTime.Now;
            string NowDate = String.Format("{0:dd/MM/yyyy}", date);
            string data = ReadData();
            string pattern = "bill=.*value=\"(.+?)\".*date=\"" + NowDate + ".*";

            MatchCollection m = Regex.Matches(data, pattern, RegexOptions.Multiline);
            /*
            for (int i = 0; i < m.Count; i++)
            {
                MessageBox.Show(m[i].ToString());
            }
            */
            return m.Count;
        }

        /// <summary>
        /// Return total bills in interger type.
        /// </summary>
        /// <returns></returns>
        public static int Count()
        {
            string data = ReadData();
            string pattern = "bill={(.+?)}";
            MatchCollection m = Regex.Matches(data, pattern, RegexOptions.Multiline);
            return m.Count;
        }

        /// <summary>
        /// Return today money in double type.
        /// </summary>
        /// <returns></returns>
        public static double TodayMoney()
        {
            DateTime date = DateTime.Now;
            string NowDate = String.Format("{0:dd/MM/yyyy}", date);
            string data = ReadData();
            string pattern = "bill=.*value=\"(.+?)\".*date=\"" + NowDate + ".*";
            MatchCollection m = Regex.Matches(data, pattern, RegexOptions.Multiline);
            double Money = 0;
            for (int i = 0; i < m.Count; i++)
            {
                //MessageBox.Show(m[i].Groups[1].Value.ToString());
                Money += Convert.ToDouble(m[i].Groups[1].Value);
            }
            return Money;
        }

        /// <summary>
        /// Return total money in double type.
        /// </summary>
        /// <returns></returns>
        public static double Money()
        {
            string data = ReadData();
            string pattern = "bill=.*value=\"(.+?)\"";
            MatchCollection m = Regex.Matches(data, pattern, RegexOptions.Multiline);
            double Money = 0;
            for (int i = 0; i < m.Count; i++)
            {
                //MessageBox.Show(m[i].Groups[1].Value.ToString());
                Money += Convert.ToDouble(m[i].Groups[1].Value);
            }
            return Money;
        }

        /// <summary>
        /// Add new bill to databases.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <param name="date"></param>
        /// <param name="time"></param>
        public static void Add(string id, string value, string date = "", string time = "")
        {
            if(date == "" || date == null)
            {
                DateTime dt = DateTime.Now;
                date = String.Format("{0:dd/MM/yyyy}", dt);
                time = String.Format("{0:HH:mm:ss}", dt);
            }
            //bill={id="02", value="15000", date="05/06/1997", time="21:05:19"}
            string result = "bill={id=\"" + id + "\", value=\"" + value + "\", date=\"" + date + "\", time=\"" + time + "\"}";

            File.WriteAllText(StatisticsFile, ReadData() + "\n" + result);
        }

        public class Chart
        {
            /// <summary>
            /// Date format dd/MM/yyyy. Return total money in a day.
            /// </summary>
            /// <param name="date"></param>
            /// <returns></returns>
            private static double CountDayMoney(string FileData, string date)
            {
                string pattern = "bill=.*value=\"(.+?)\".*date=\"" + date + ".*";
                MatchCollection m = Regex.Matches(FileData, pattern, RegexOptions.Multiline);
                double Money = 0;
                for (int i = 0; i < m.Count; i++)
                {
                    Money += Convert.ToDouble(m[i].Groups[1].Value);
                }
                return Money;
            }
            public static double[] Money7days()
            {
                double[] result = new double[7];
                string data = ReadData();
                for(int i = 0; i < 7; i++)
                {
                    DateTime dt = DateTime.Now.Date.AddDays(-i);
                    string date = String.Format("{0:dd/MM/yyyy}", dt);
                    result[i] = CountDayMoney(data, date);
                }
                return result;
            }


            /// <summary>
            /// Date format dd/MM/yyyy. Return total bill in a day.
            /// </summary>
            /// <param name="data"></param>
            /// <param name="date"></param>
            /// <returns></returns>
            private static int CountDayBill(string data, string date)
            {
                string pattern = "bill=.*value=\"(.+?)\".*date=\"" + date + ".*";
                MatchCollection m = Regex.Matches(data, pattern, RegexOptions.Multiline);
                return m.Count;
            }
            public static double[] Bill5days()
            {
                double[] result = new double[5];
                string data = ReadData();

                for (int i = 0; i < 5; i++)
                {
                    DateTime dt = DateTime.Now.Date.AddDays(-i);
                    string date = String.Format("{0:dd/MM/yyyy}", dt);
                    result[i] = Convert.ToDouble(CountDayBill(data, date));
                }
                return result;
            }
        }
    }

}

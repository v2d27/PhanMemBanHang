using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
using System.Windows.Threading;

namespace PhanMemBanHang
{
    /// <summary>
    /// Interaction logic for QuetMaVach.xaml
    /// </summary>
    public partial class QuetMaVach : Page
    {
        #region All variable use for printing
        private static string[] danhsachhang = { "success! ok", "May be" };
        public string[] DanhSachHang
        {
            get { return danhsachhang; }
            set { danhsachhang = value; }
        }
        private static bool isPrintPreview = false;
        public bool IsPrintPreview
        {
            get { return isPrintPreview; }
            set { isPrintPreview = value; }
        }
        private static string __0 = "OK";
        private static string __1 = "";
        private static string __2 = "";
        private static string __3 = "";
        private static string __4 = "";
        public string DonHangSo
        {
            get { return __0; }
            set { __0 = value; }
        }
        public string TongSoTien
        {
            get { return __1; }
            set { __1 = value; }
        }
        public string TienKhachDua
        {
            get { return __2; }
            set { __2 = value; }
        }
        public string TienConDu
        {
            get { return __3; }
            set { __3 = value; }
        }
        public string NgayBan
        {
            get { return __4; }
            set { __4 = value; }
        }
        #endregion
        #region Variable used in local class
        public DataTable DataExcel = new DataTable();
        public class Data
        {
            public int A { get; set; }
            public string B { get; set; }
            public string C { get; set; }
            public string D { get; set; }
            public string E { get; set; }
            public string Barcode { get; set; }
        }
        private bool IsExporting = false;
        public double TONGSOTIEN = 0;
        #endregion
        #region AutoCompleteComboBox Properties
        /// <summary>
        /// Gets or sets the country list.
        /// </summary>
        /// <value>The country list.</value>
        public List<string> CountryList { get; set; }

        /// <summary>
        /// Gets or sets the selected country.
        /// </summary>
        /// <value>The selected country.</value>
        public string SelectedCountry { get; set; }

        #endregion

        public QuetMaVach()
        {
            InitializeComponent();
            DataExcel = ImportSheet(@".\sources\DataBaseExcel.xlsx");

            //ShowDateTime
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

            //Cap nhat so don hang
            CapNhatSoDonHang();


            //Test data
            /*
            for (int i = 0; i < DataExcel.Rows.Count; i++)
            {
                dataGrid1.Items.Add(
                    new Data {
                        A = i+1,
                        B = DataExcel.Rows[i][1].ToString(),
                        C = "1",
                        D = VND.ConvertToString(DataExcel.Rows[i][2].ToString()),
                        E = VND.ConvertToString(DataExcel.Rows[i][2].ToString()),
                        Barcode = DataExcel.Rows[i][3].ToString()
                    });
            }
            */

            this.DataContext = this;

            CountryList = new List<string>();
            for(int i = 0; i < DataExcel.Rows.Count; i++)
            {
                CountryList.Add(DataExcel.Rows[i][1].ToString());
            }
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            bool found = false;
            for (int i = 0; i < DataExcel.Rows.Count; i++)
            {
                string BarCode = DataExcel.Rows[i][3].ToString();
                if (BarCode == barcode.Text)
                {
                    string tenhang = DataExcel.Rows[i][1].ToString();
                    string giatien = DataExcel.Rows[i][2].ToString();
                    dataGrid1.Items.Add(new Data
                    {
                        A = dataGrid1.Items.Count + 1,
                        B = tenhang,
                        C = "1",
                        D = VND.ConvertToString(giatien),
                        E = VND.ConvertToString(giatien),
                        Barcode = BarCode
                    });
                    TONGSOTIEN += VND.ConvertToNumber(giatien);
                    TongTien.Text = VND.ConvertToString(TONGSOTIEN);

                    dataGrid1.ScrollIntoView(dataGrid1.Items[dataGrid1.Items.Count-1]);

                    found = true;
                    break;
                }
            }
            barcode.SelectAll();
            barcode.Focus();
            if (!found)  MessageBox.Show("Không tìm thấy mã vạch: \n\"" + barcode.Text + "\"\n\nVui lòng cập nhật cơ sở dữ liệu để thanh toán sản phẩm này.", "Thông báo");
        }

        public static DataTable ImportSheet(string fileName)
        {
            var datatable = new DataTable();
            var workbook = new XLWorkbook(fileName);
            var xlWorksheet = workbook.Worksheet(1);
            var range = xlWorksheet.Range(xlWorksheet.FirstCellUsed(), xlWorksheet.LastCellUsed());

            var col = range.ColumnCount();
            var row = range.RowCount();

            //if a datatable already exists, clear the existing table 
            datatable.Clear();
            for (var i = 1; i <= col; i++)
            {
                var column = xlWorksheet.Cell(1, i);
                datatable.Columns.Add(column.Value.ToString());
            }

            var firstHeadRow = 0;
            foreach (var item in range.Rows())
            {
                if (firstHeadRow != 0)
                {
                    var array = new object[col];
                    for (var y = 1; y <= col; y++)
                    {
                        array[y - 1] = item.Cell(y).Value;
                    }

                    datatable.Rows.Add(array);
                }
                firstHeadRow++;
            }
            return datatable;
        }

        //* Read data from DataGrid
        public string getDataGrid(int x, int y)
        {
            string str = (dataGrid1.Columns[y].GetCellContent(dataGrid1.Items[x]) as TextBlock).Text;
            if (str == String.Empty)
            {
                return (string)"";
            }
            return str;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            string result = String.Format("{0:dd/MM/yy HH:mm:ss}", dt);
            time.Text = result;
        }

        private void CapNhatSoDonHang()
        {
            int total = Statistics.Count() + 601;
            donhangso.Text = total.ToString("000");
        }

        private void ButtonPrint_MouseUp(object sender, RoutedEventArgs e)
        {
            IsExporting = true;
            //MessageBox.Show("row.count = " + dataGrid1.Items.Count + "\ncol.count = " + dataGrid1.Columns.Count);
            if (dataGrid1.Items.Count <= 0)
            {
                MessageBox.Show("Bạn chưa nhập sản phẩm nào. Vui lòng nhập vào ít nhất một sản phẩm.", "Thông báo");
                return;
            }
            string[] result = new string[dataGrid1.Items.Count];
            int totalrow = dataGrid1.Items.Count;
            int totalcol = dataGrid1.Columns.Count;
            string dataRow = "";

            for (int irow = 0; irow < totalrow; irow++)
            {
                dataGrid1.SelectedIndex = irow;
                dataGrid1.ScrollIntoView(dataGrid1.Items[irow]);

                dataRow  = Convert.ToString(irow + 1) + "|"; //STT
                dataRow += getDataGrid(irow, 1) + "|"; //Ten san pham
                dataRow += getDataGrid(irow, 2) + "|"; //So luong
                dataRow += getDataGrid(irow, 4) + "|"; //Thanh tien
                result[irow] = dataRow;
            }

            DanhSachHang = result; //=> Collect all DataGrid items before call printpage windows
            DonHangSo = donhangso.Text;
            TongSoTien = VND.ConvertToString(TONGSOTIEN);
            TienKhachDua = VND.ConvertToString(TienKhachTra.Text);
            TienConDu = (TienDu.Text.Substring(0, 1) == "-" ? "-" : "") +  VND.ConvertToString(TienDu.Text);
            NgayBan = time.Text;

            Statistics.Add(donhangso.Text, TONGSOTIEN.ToString());

            if (((Button)sender).Name == "ButtonPrintPreview")
            {
                IsPrintPreview = true;
            }
            else
            {
                IsPrintPreview = false;
            }
            //myAutoTenHang.autoTextBox.Text

            Window page = new WindowPrintPage();
            page.Show();

            IsExporting = false;
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsExporting) return;
            object item = dataGrid1.SelectedItem;
            if (item != null){
                myAutoTenHang.autoTextBox.Text = (dataGrid1.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                SoLuong.Text = (dataGrid1.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
                string giatien = (dataGrid1.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text;
                GiaTien.Text = VND.ConvertToNumber(giatien).ToString();
            }
        }

        private void InputBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            object item = dataGrid1.SelectedItem;
            if (item == null)
            {
                MessageBox.Show("Không tìm thấy sản phẩm nào để điều chỉnh. Vui lòng chọn một sản phẩm để điều chỉnh giá.", "Văn Đức > Thông báo");
                return;
            }

            switch (((TextBox)sender).Name)
            {
                case "TenHang":
                    (dataGrid1.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text = myAutoTenHang.autoTextBox.Text;
                    break;
                case "SoLuong":
                    double soluong = VND.ConvertToNumber(SoLuong.Text);
                    double giatien = VND.ConvertToNumber((dataGrid1.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text);
                    double thanhtien = soluong * giatien;

                    //update tong so tien
                    TONGSOTIEN -= VND.ConvertToNumber((dataGrid1.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text);
                    TONGSOTIEN += thanhtien;
                    TongTien.Text = VND.ConvertToString(TONGSOTIEN);

                    //update soluong + thanh tien
                    (dataGrid1.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text = SoLuong.Text;
                    (dataGrid1.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text = VND.ConvertToString(thanhtien);

                    //update tien du
                    double tientra = VND.ConvertToNumber(TienKhachTra.Text);
                    double result = tientra - TONGSOTIEN;
                    string negative = "";
                    if (result < 0) negative = "-";
                    TienDu.Text = negative + VND.ConvertToString(result);

                    break;
                case "GiaTien":
                    double soluong2 = VND.ConvertToNumber((dataGrid1.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text);
                    double giatien2 = VND.ConvertToNumber(GiaTien.Text);
                    double thanhtien2 = soluong2 * giatien2;

                    //update tong so tien
                    TONGSOTIEN -= VND.ConvertToNumber((dataGrid1.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text);
                    TONGSOTIEN += thanhtien2;
                    TongTien.Text = VND.ConvertToString(TONGSOTIEN);

                    //update giatien + thanh tien
                    (dataGrid1.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text = VND.ConvertToString(giatien2);
                    (dataGrid1.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text = VND.ConvertToString(thanhtien2);

                    //update tien du
                    double tientra2 = VND.ConvertToNumber(TienKhachTra.Text);
                    double result2 = tientra2 - TONGSOTIEN;
                    string negative2 = "";
                    if (result2 < 0) negative2 = "-";
                    TienDu.Text = negative2 + VND.ConvertToString(result2);

                    //fix TextBox GiaTien
                    GiaTien.Text = VND.ConvertToString(giatien2);
                    break;
            }

        }

        private void InputBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void HoaDonMoi_MouseUp(object sender, RoutedEventArgs e)
        {
            while (dataGrid1.Items.Count != 0)
            {
                dataGrid1.Items.RemoveAt(0);
            }
            TONGSOTIEN = 0;

            myAutoTenHang.autoTextBox.Text = "";
            SoLuong.Text = "0";
            GiaTien.Text = "0đ";
            TongTien.Text = "0đ";
            TienDu.Text = "0đ";
            TienKhachTra.Text = "0đ";
            CapNhatSoDonHang();
            barcode.Focus();
        }

        private void BtnSua_Clicked(object sender, RoutedEventArgs e)
        {
            if (IsExporting) return;
            object item = dataGrid1.SelectedItem;
            if (item == null)
            {
                MessageBox.Show("Không tìm thấy sản phẩm nào để điều chỉnh. Vui lòng chọn một sản phẩm để điều chỉnh giá.", "Văn Đức > Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            
            int soluong = Convert.ToInt32(SoLuong.Text);
            double giaban = VND.ConvertToNumber(GiaTien.Text);
            double thanhtien = giaban * soluong;
            TONGSOTIEN -= VND.ConvertToNumber((dataGrid1.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text);
            TONGSOTIEN += thanhtien;

            (dataGrid1.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text = myAutoTenHang.autoTextBox.Text;
            (dataGrid1.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text = SoLuong.Text;
            (dataGrid1.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text = VND.ConvertToString(giaban);
            (dataGrid1.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text = VND.ConvertToString(thanhtien);

            //Update tong so tien
            TongTien.Text = VND.ConvertToString(TONGSOTIEN);

            //update tien du
            double tientra = VND.ConvertToNumber(TienKhachTra.Text);
            double result = tientra - TONGSOTIEN;
            string negative = "";
            if (result < 0) negative = "-";
            TienDu.Text = negative + VND.ConvertToString(result);

            MessageBox.Show("Đã điều chỉnh xong!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtXoa_Clicked(object sender, RoutedEventArgs e)
        {
            object item = dataGrid1.SelectedItem;
            if (item == null)
            {
                MessageBox.Show("Không có sản phẩm nào bị xoá. Vui lòng chọn một sản phẩm.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            TONGSOTIEN -= VND.ConvertToNumber((dataGrid1.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text);
            TongTien.Text = VND.ConvertToString(TONGSOTIEN);

            double tientra = VND.ConvertToNumber(TienKhachTra.Text);
            double result = tientra - TONGSOTIEN;
            string negative = "";
            if (result < 0) negative = "-";
            TienDu.Text = negative + VND.ConvertToString(result);

            dataGrid1.Items.Remove(item);
        }

        private void TienKhachTra_KeyUp(object sender, KeyEventArgs e)
        {
            double tientra = VND.ConvertToNumber(TienKhachTra.Text);
            double result = tientra - TONGSOTIEN;
            string negative = "";
            if (result < 0) negative = "-";
            TienDu.Text = negative + VND.ConvertToString(result);

            if(e.Key == Key.Enter)
            {
                TienKhachTra.Text = VND.ConvertToString(TienKhachTra.Text);
                ButtonPrint.Focus();
            }
        }

        private void TienKhachTra_LostFocus(object sender, RoutedEventArgs e)
        {
            TienKhachTra.Text = VND.ConvertToString(TienKhachTra.Text);
        }

        private void BtnThem_Clicked(object sender, RoutedEventArgs e)
        {
            double soluong = VND.ConvertToNumber(SoLuong.Text);
            double giatien = VND.ConvertToNumber(GiaTien.Text);
            string tenhang = myAutoTenHang.autoTextBox.Text;
            if (tenhang == null || tenhang == "") tenhang = "";
            dataGrid1.Items.Add(new Data
            {
                A = dataGrid1.Items.Count + 1,
                B = tenhang,
                C = soluong.ToString(),
                D = VND.ConvertToString(giatien),
                E = VND.ConvertToString(soluong * giatien),
                Barcode = ""
            });
            dataGrid1.ScrollIntoView(dataGrid1.Items[dataGrid1.Items.Count-1]);
        }

        private void myAutoTenHang_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return; 
            string findString = myAutoTenHang.autoTextBox.Text;
            bool found = false;
            for(int i = 0; i < DataExcel.Rows.Count; i++)
            {
                if(DataExcel.Rows[i][1].ToString() == findString) //tenhang columns
                {
                    SoLuong.Text = "1";
                    GiaTien.Text = DataExcel.Rows[i][2].ToString();
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                SoLuong.Text = "1";
                GiaTien.Text = "";
            }

        }
    }


    /// <summary>
    /// QuetMaVach.xaml.cs
    /// </summary>
    public static class VND
    {
        public static string ConvertToString(int Value)
        {
            string str = Convert.ToString(Value) + ".";
            return ConvertToString(str);
        }

        public static string ConvertToString(double Value)
        {
            string str = Convert.ToString(Value);
            return ConvertToString(str);
        }

        public static string ConvertToString(string Value)
        {
            string result = "đ";
            string str = Value;
            if(Value == null || Value == "")
            {
                return "0" + result;
            }

            const int GroupLength = 3;
            int iSeperator = Value.IndexOf(".");
            if(iSeperator > 0)
            {
                string after = str.Substring(iSeperator , str.Length - iSeperator);
                string newAfter = "";
                char[] arrAfter = after.ToCharArray();
                for(int i = 0; i < arrAfter.Length; i++)
                {
                    int ASCII = Convert.ToInt32(arrAfter[i]);
                    if (ASCII >= 48 && ASCII <= 57)
                    {
                        newAfter += arrAfter[i].ToString();
                    }
                }
                for(int i = 0; newAfter.Length < 3; i++)
                {
                    newAfter += "0";
                }
                
                result = "." + newAfter + "đ";
                str = str.Substring(0, iSeperator);
            }
            
            char[] arr = str.ToCharArray();
            int len = arr.Length;
            int count = 0;
            for (int i = len-1; i >= 0; i--)
            {
                int ASCII = Convert.ToInt32(arr[i]);
                if (ASCII >= 48 && ASCII <= 57)
                {
                    result = arr[i].ToString() + result;
                    count++;
                    if(count % GroupLength == 0 && i != 0)
                    {
                        result = "," + result;
                    }
                }
            }
            if (result.Length == 1) result = "0" + result;
            if(result[0].ToString() == ",")
            {
                result = result.Substring(1, result.Length - 1);
            }
            return result;
        }

        public static double ConvertToNumber(string str)
        {
            string temp = "";
            if(str == null || str == "")
            {
                return 0;
            }
            char[] arr = str.ToCharArray();
            for(int i = 0; i < arr.Length; i++)
            {
                if(arr[i].ToString() == ".")
                {
                    temp += ".";
                    continue;
                }
                int ASCII = Convert.ToInt32(arr[i]);
                if(ASCII >= 48 && ASCII <= 57)
                {
                    temp += arr[i].ToString();
                }
            }
            if (temp == "") temp = "0";
            return Convert.ToDouble(temp);
        }

    }
}

using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class BarcodeScanPage : Page
    {
        private static string DataBaseFile = @".\sources\DataBaseExcel.xlsx";
        DataTable DataExcel = new DataTable();

        public BarcodeScanPage()
        {
            InitializeComponent();
            DataExcel = ImportSheet(DataBaseFile);
        }

        /// <summary>
        /// Return row index of Barcode
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private int IsBarcodeExist(string str)
        {
            int count = DataExcel.Rows.Count;
            while (count-- > 0)
            {
                if(str == DataExcel.Rows[count][3].ToString())
                {
                    return count;
                }
            }
            return -1;
        }

        private void SaveBarcode()
        {
            int indexexist = IsBarcodeExist(MaVach.Text);
            if (indexexist >= 0)
            {
                if(MessageBox.Show("Mã vạch của sản phẩm này đã tồn tại. Bạn có muốn cập nhật giá và tên sản phẩm không?",
                    "Quét mã vạch", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    Message.Text = "> Đang cập nhật...";
                    Message.Foreground = Brushes.Gray;
                    DataExcel.Rows[indexexist][1] = TenSanPham.Text;
                    DataExcel.Rows[indexexist][2] = GiaTien.Text;
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(DataExcel, "HangHoa");
                        wb.SaveAs(DataBaseFile);
                        wb.Dispose();
                    }
                    Message.Text = "> Đã cập nhật " + TenSanPham.Text;
                    Message.Foreground = Brushes.Green;
                }
                else
                {
                    Message.Text = "";
                }
                MaVach.Focus();
                MaVach.Text = "";
                return;
            }

            Message.Text = "> Đang cập nhật...";
            Message.Foreground = Brushes.Gray;
            var arr = new object[4];
            arr[0] = DataExcel.Rows.Count + 1;
            arr[1] = TenSanPham.Text;
            arr[2] = GiaTien.Text;
            arr[3] = MaVach.Text;
            DataExcel.Rows.Add(arr);
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(DataExcel, "HangHoa");
                wb.SaveAs(DataBaseFile);
                wb.Dispose();
            }

            Message.Text = "> Đã cập nhật " + TenSanPham.Text;
            Message.Foreground = Brushes.Green;
            MaVach.Focus();
            MaVach.Text = "";
        }

        private void InputMaVach_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                switch (((TextBox)sender).Name)
                {
                    case "MaVach":
                        int index = IsBarcodeExist(MaVach.Text);
                        if (index > 0)
                        {
                            TenSanPham.Text = DataExcel.Rows[index][1].ToString();
                            GiaTien.Text = DataExcel.Rows[index][2].ToString();
                        }

                        TenSanPham.Focus();
                        TenSanPham.SelectAll();
                        break;
                    case "TenSanPham":
                        GiaTien.Focus();
                        GiaTien.SelectAll();
                        break;
                    case "GiaTien":
                        SaveBarcode();
                        break;
                }
            }
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
            workbook.Dispose();
            return datatable;
        }




        private void InputMaVach_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MaVach.Focus();
            MaVach.SelectAll();
        }

        private void Save_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SaveBarcode();
            }
        }

        private void Save_MouseUp(object sender, MouseButtonEventArgs e)
        {
            SaveBarcode();
        }

        private void Refresh_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MaVach.Text = "";
            TenSanPham.Text = "";
            GiaTien.Text = "";
            MaVach.Focus();
            MaVach.SelectAll();
        }
    }
}

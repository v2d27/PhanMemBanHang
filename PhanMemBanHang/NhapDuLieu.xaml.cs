using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ClosedXML.Excel;
using System.Data;

namespace PhanMemBanHang
{
    /// <summary>
    /// Interaction logic for NhapDuLieu.xaml
    /// </summary>
    public partial class NhapDuLieu : Page
    {
        private static string DataBaseFile = @".\sources\DataBaseExcel.xlsx";
        public class Data
        {
            public int A { get; set; }
            public string B { get; set; }
            public string C { get; set; }
            public string D { get; set; }
        }
        DataTable DataExcel = new DataTable();

        public NhapDuLieu()
        {
            InitializeComponent();
        }

        private void dgDuLieu_Loaded(object sender, RoutedEventArgs e)
        {
            QueryData_dgDuLieu(DataBaseFile); ;
        }

        private void QueryData_dgDuLieu(string sFile)
        {
            while (dgDuLieu.Items.Count != 0)
            {
                dgDuLieu.Items.RemoveAt(0);
            }
            DataExcel.Clear();
            DataExcel = ImportSheet(sFile);
            int count = DataExcel.Rows.Count;
            for(int i = 0; i < count; i++)
            {
                dgDuLieu.Items.Add(new Data {
                    A = i + 1,
                    B = DataExcel.Rows[i][1].ToString(),
                    C = VND.ConvertToString(DataExcel.Rows[i][2].ToString()),
                    D = DataExcel.Rows[i][3].ToString()
                });
            }
        }

        private void CapNhatGia()
        {
            object item = dgDuLieu.SelectedItem;
            if (item == null)
            {
                MessageBox.Show("Không tìm thấy sản phẩm nào để điều chỉnh. Vui lòng chọn một sản phẩm để điều chỉnh giá.", "Văn Đức > Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string tenhang = TenHang.Text;
            string giaban = GiaTien.Text;
            string barcode = BarCode.Text;

            //Save to Excel file
            int index = dgDuLieu.SelectedIndex;
            if(index >= 0)
            {
                DataExcel.Rows[index][1] = tenhang;
                DataExcel.Rows[index][2] = giaban;
                DataExcel.Rows[index][3] = barcode;
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(DataExcel, "HangHoa");
                    wb.SaveAs(DataBaseFile);
                    wb.Dispose();
                }

                (dgDuLieu.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text = tenhang;
                (dgDuLieu.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text = VND.ConvertToString(giaban);
                (dgDuLieu.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text = barcode;

                GiaTien.Text = VND.ConvertToString(giaban);
                MessageBox.Show("Đã lưu vào cơ sở dữ liệu của hệ thống. ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private string getData(int hang, int cot)
        {
            string str = (dgDuLieu.Columns[cot].GetCellContent(dgDuLieu.Items[hang]) as TextBlock).Text;
            if (str == String.Empty)
            {
                return (string)"";
            }
            return str;
        }

        /// <summary>
        /// Return row index of Barcode
        /// </summary>
        /// <param name="str"></param>
        /// <returns>Return index of position. If not found return -1.</returns>
        private int IsBarcodeExist(string str)
        {
            int count = DataExcel.Rows.Count;
            while (count-- > 0)
            {
                if (str == DataExcel.Rows[count][3].ToString())
                {
                    return count;
                }
            }
            return -1;
        }

        private void BtnCapNhatGia_MouseUp(object sender, MouseButtonEventArgs e)
        {
            CapNhatGia();
        }

        private void TenHang_KeyUp(object sender, KeyEventArgs e)
        {
            object item = dgDuLieu.SelectedItem;
            if (item == null) return;

            (dgDuLieu.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text = TenHang.Text;
            (dgDuLieu.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text = VND.ConvertToString(GiaTien.Text);
            (dgDuLieu.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text = BarCode.Text;

            if(e.Key == Key.Enter)
            {
                CapNhatGia();
            }
            
        }

        private void dgDuLieu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object item = dgDuLieu.SelectedItem;
            if (item == null) return;
            TenHang.Text = (dgDuLieu.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
            GiaTien.Text = VND.ConvertToString((dgDuLieu.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text);
            BarCode.Text = (dgDuLieu.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text;
        }
    }
}

using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace PhanMemBanHang
{
    /// <summary>
    /// Interaction logic for WindowPrintPage.xaml
    /// </summary>
    public partial class WindowPrintPage : Window
    {
        QuetMaVach quet = new QuetMaVach();
        private static string[] danhsachHang;
        private static int danhsachSLHang;

        public WindowPrintPage()
        {
            InitializeComponent();
            danhsachHang = quet.DanhSachHang;
            danhsachSLHang = danhsachHang.Length;
            queryData();
            donhangso.Text = quet.DonHangSo;
            tongsotien.Text = quet.TongSoTien;
            tiencondu.Text = quet.TienConDu;
            khachhangtra.Text = quet.TienKhachDua;
            datetime.Text = quet.NgayBan;
            
        }

        private void queryData()
        {
            string temp;
            int iSeperator = 0;
            int LastSeperator = 0;
            TableRow row;
            TableCell cell;

            for(int i = 0; i < danhsachSLHang; i++)
            {
                iSeperator = 0;
                LastSeperator = 0;
                var rowGroup = table1.RowGroups.FirstOrDefault();
                if (rowGroup != null)
                {
                    row = new TableRow();

                    iSeperator = danhsachHang[i].IndexOf("|");
                    iSeperator = danhsachHang[i].IndexOf("|", iSeperator + 1);
                    temp = danhsachHang[i].Substring(LastSeperator, iSeperator).Replace("|", ". ");
                    cell = new TableCell();
                    cell.Blocks.Add(new Paragraph(new Run(temp)));
                    row.Cells.Add(cell);

                    LastSeperator = iSeperator;
                    iSeperator = danhsachHang[i].IndexOf("|", iSeperator + 1);
                    temp = danhsachHang[i].Substring(LastSeperator + 1, iSeperator - LastSeperator - 1);
                    cell = new TableCell();
                    cell.Blocks.Add(new Paragraph(new Run(temp)));
                    row.Cells.Add(cell);

                    LastSeperator = iSeperator;
                    iSeperator = danhsachHang[i].IndexOf("|", iSeperator + 1);
                    temp = danhsachHang[i].Substring(LastSeperator + 1, iSeperator - LastSeperator - 1);
                    cell = new TableCell();
                    cell.Blocks.Add(new Paragraph(new Run(temp)));
                    row.Cells.Add(cell);

                    rowGroup.Rows.Add(row);
                }
            }

        }
             
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Print();
        }
        private void Print()
        {
            PrintDialog pd = new PrintDialog();
            //if (pd.ShowDialog() != true) return;

            flowDocument.PageHeight = pd.PrintableAreaHeight;
            flowDocument.PageWidth = pd.PrintableAreaWidth;

            IDocumentPaginatorSource idocument = flowDocument as IDocumentPaginatorSource;

            pd.PrintDocument(idocument.DocumentPaginator, "Hoá đơn bán lẻ số " + quet.DonHangSo);
            this.Close();
            MessageBox.Show("Hoá đơn đã được xuất ra máy in.\nĐơn hàng số: " + quet.DonHangSo, "In hoá đơn");
        }

        private void FlowDocument_Loaded(object sender, RoutedEventArgs e)
        {
            if (quet.IsPrintPreview == false)
            {
                Print();
            }
        }
    }
}

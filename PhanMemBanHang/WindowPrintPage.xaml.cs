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


            //Export bill to image
            if (!Directory.Exists(@".\HoaDonDaXuat")) Directory.CreateDirectory(@".\HoaDonDaXuat");
            string todayFolder = @".\HoaDonDaXuat\Hoa Don Ngay " + quet.NgayBan.Substring(0, 8).Replace("/", ".");
            if (!Directory.Exists(todayFolder)) Directory.CreateDirectory(todayFolder);

            string filename = todayFolder + @"\HoaDonSo" + quet.DonHangSo + "_" + quet.NgayBan.Replace(" ", "-").Replace("/", ".").Replace(":", ".") + ".jpg";
            SaveDocumentPagesToImages(idocument, filename);
        }
        public void SaveDocumentPagesToImages(IDocumentPaginatorSource document, string filename)
        {
            if (string.IsNullOrEmpty(filename)) return;

            MemoryStream[] streams = null;
            try
            {
                int pageCount = document.DocumentPaginator.PageCount;
                DocumentPage[] pages = new DocumentPage[pageCount];
                for (int i = 0; i < pageCount; i++)
                    pages[i] = document.DocumentPaginator.GetPage(i);

                streams = new MemoryStream[pages.Count()];

                for (int i = 0; i < pages.Count(); i++)
                {
                    DocumentPage source = pages[i];
                    streams[i] = new MemoryStream();

                    RenderTargetBitmap renderTarget =
                       new RenderTargetBitmap((int)source.Size.Width,
                                               (int)1500,
                                               96, // WPF (Avalon) units are 96dpi based
                                               96,
                                               System.Windows.Media.PixelFormats.Default);

                    renderTarget.Render(source.Visual);

                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();  // Choose type here ie: JpegBitmapEncoder, etc
                    encoder.QualityLevel = 100;
                    encoder.Frames.Add(BitmapFrame.Create(renderTarget));

                    encoder.Save(streams[i]);

                    FileStream file = new FileStream(filename, FileMode.CreateNew);
                    file.Write(streams[i].GetBuffer(), 0, (int)streams[i].Length);
                    file.Close();

                    streams[i].Position = 0;
                }
            }
            catch (Exception e1)
            {
                throw e1;
            }
            finally
            {
                if (streams != null)
                {
                    foreach (MemoryStream stream in streams)
                    {
                        stream.Close();
                        stream.Dispose();
                    }
                }
            }
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

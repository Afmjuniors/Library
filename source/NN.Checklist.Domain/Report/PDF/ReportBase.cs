using System;
using System.Collections.Generic;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TDCore.Domain.Exceptions;
using NN.Checklist.Domain.DTO.Response;
using TDCore.Globalization;
using TDCore.DependencyInjection;

namespace NN.Checklist.Domain.Report.PDF
{
    public class ReportBase: iTextSharp.text.pdf.PdfPageEventHelper
    {
        // This is the contentbyte object of the writer
        public PdfContentByte cb;

        // we will put the final number of pages in a template
        public PdfTemplate headerTemplate, footerTemplate;

        // this is the BaseFont we are going to use for the header / footer
        public BaseFont bf = null;

        // This keeps track of the creation time
        public DateTime PrintTime = DateTime.Now;
        private Document document;


        private float totalHeight;
        private string Path;
        public string FileName;
        private PdfWriter Writer;
        public List<PdfPCell> SubTitle;
        public List<PdfPCell> SubTitleLocal;
        private string PrintUser;
        private AuthenticatedUserDTO User;

        /// <summary>
        /// Name: ReportBase
        /// Description: Constructor method that receives title, subTitle, path, fileName, userName, vertical, user as parameter and creates a pdf, assembles it and defines the events.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public ReportBase(string title, string subTitle, string path, string fileName, string userName, bool vertical, AuthenticatedUserDTO user) : base()
        {
            try
            {
                SubTitle = new List<PdfPCell>();
                SubTitleLocal = new List<PdfPCell>();
                Path = path;
                FileName = fileName;
                Title = title;
                if (!string.IsNullOrEmpty(subTitle))
                {
                    AddSubTitle(subTitle);
                }                
                document = new Document();
                PrintUser = userName;
                User = user;

                // Montando o PDF

                var dir = new DirectoryInfo(Path);
                if (!dir.Exists)
                {
                    dir.Create();
                }


                Writer = PdfWriter.GetInstance(document, new FileStream(path + "/" + fileName, FileMode.Create));
                // Definindo os eventos
                Writer.PageEvent = this; 
                // Abrindo e editando o PDF

                #region Configurações do Grid Principal.

                if (!vertical)
                {
                    document.SetPageSize(PageSize.A4.Rotate());
                }
                document.SetMargins(0, 0, 100, 60 + totalHeight);
                document.Open();

                document.NewPage();
                #endregion
            }
            catch (Exception e) { throw new DomainException(e.Message); }
        }

        #region Properties
        public string Title
        {
            get;
            set;
        }

        #endregion

        #region UserCode

        /// <summary>
        /// Name: AddPageBreak
        /// Description: No-return method that adds page break.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public void AddPageBreak()
        {
            document.NewPage();
        }

        /// <summary>
        /// Name: AddBody
        /// Description: Non-returning method that takes as a parameter body and adds the body.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public void AddBody(IElement body)
        {
            try
            {
                document.Add(body);
            }
            catch (Exception e) 
            { 
                throw new DomainException(e.Message); 
            }
        }

        /// <summary>
        /// Name: AddSubTitle
        /// Description: Non-returning method that takes subTitle as a parameter and adds a subtitle.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public void AddSubTitle(string subTitle)
        {
            try
            {
                var ftTitle1 = FontFactory.GetFont("Calibri", 16, Font.BOLD);
                var ftTitle2 = FontFactory.GetFont("Calibri", 13, Font.BOLD);
                Paragraph line1 = new Paragraph(subTitle, ftTitle2);
                line1.Alignment = Element.ALIGN_CENTER;


                var cel1 = new PdfPCell(line1);
                cel1.HorizontalAlignment = Element.ALIGN_CENTER;
                cel1.VerticalAlignment = Element.ALIGN_MIDDLE;
                cel1.Border = 0;

                cel1.Colspan = 3;
                SubTitle.Add(cel1);
            }
            catch (Exception e) { throw new DomainException(e.Message); }
        }

        /// <summary>
        /// Name: AddSubTitleLocal
        /// Description: Non-returning method that takes subTitle as a parameter and adds a local subtitle.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public void AddSubTitleLocal(string subTitle)
        {
            try
            {
                var ftTitle = FontFactory.GetFont("Calibri", 16, Font.BOLD);
                var ftTitle2 = FontFactory.GetFont("Calibri", 13, Font.BOLD);
                Paragraph line1 = new Paragraph(subTitle, ftTitle2);
                line1.Alignment = Element.ALIGN_CENTER;


                var cel1 = new PdfPCell(line1);
                cel1.HorizontalAlignment = Element.ALIGN_CENTER;
                cel1.VerticalAlignment = Element.ALIGN_MIDDLE;
                cel1.Border = 0;

                cel1.Colspan = 3;

                SubTitleLocal = null;
                SubTitleLocal = new List<PdfPCell>();
                SubTitleLocal.Add(cel1);
            }
            catch (Exception e) { throw new DomainException(e.Message); }
        }

        /// <summary>
        /// Name: AddSubTitleSmall
        /// Description: Non-returning method that takes subTitle as a parameter and adds a small subtitle.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public void AddSubTitleSmall(string subTitle)
        {
            try
            {
                var ftTitle1 = FontFactory.GetFont("Calibri", 10, Font.BOLD);
                var ftTitle2 = FontFactory.GetFont("Calibri", 7, Font.BOLD);
                Paragraph line1 = new Paragraph(subTitle, ftTitle2);
                line1.Alignment = Element.ALIGN_CENTER;


                var cel1 = new PdfPCell(line1);
                cel1.HorizontalAlignment = Element.ALIGN_CENTER;
                cel1.VerticalAlignment = Element.ALIGN_MIDDLE;
                cel1.Border = 0;

                cel1.Colspan = 3;
                SubTitle.Add(cel1);
            }
            catch (Exception e)
            {
                throw new DomainException(e.Message);
            }
        }

        /// <summary>
        /// Name: OnOpenDocument
        /// Description: event when document opened.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public override void OnOpenDocument(PdfWriter writerE, Document documentE)
        {
            try
            {
                PrintTime = DateTime.Now;
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb = Writer.DirectContent;
                headerTemplate = cb.CreateTemplate(20, 100);
                footerTemplate = cb.CreateTemplate(100, 100);
            }
            catch (Exception e) { throw new DomainException(e.Message); }
        }

        /// <summary>
        /// Name: OnEndPage
        /// Description: event when oage ends.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public override void OnEndPage(iTextSharp.text.pdf.PdfWriter writerE, iTextSharp.text.Document documentE)
        {
            try
            {
                IGlobalizationService lang = ObjectFactory.GetSingleton<IGlobalizationService>();
                base.OnEndPage(this.Writer, this.document);

                iTextSharp.text.Font baseFontNormal = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);

                iTextSharp.text.Font baseFontBig = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);

                //Create PdfTable object
                PdfPTable pdfTab = new PdfPTable(3);
                PdfPTable tabMainTitle = new PdfPTable(3);
                PdfPTable tabTituloRodape = new PdfPTable(3);
                
                var ftTitle1 = FontFactory.GetFont("Calibri", 16, Font.BOLD);
                var ftTitle2 = FontFactory.GetFont("Calibri", 13, Font.BOLD);
                var ftTabelTitle = FontFactory.GetFont("Calibri", 12, Font.BOLD);
                var ftTabelBold = FontFactory.GetFont("Calibri", 11, Font.BOLD);
                var ftMsgFinal = FontFactory.GetFont("Calibri", 10, Font.ITALIC);
                var ftBody = FontFactory.GetFont("Calibri", 10, Font.NORMAL);
                var ftBodyBold = FontFactory.GetFont("Calibri", 10, Font.BOLD);
                var ftFooter = FontFactory.GetFont("Calibri", 5, Font.BOLD);

                // Texto do cabeçalho...
                PdfPCell celTitleText = new PdfPCell(new Phrase(Title, ftTitle1));
                celTitleText.VerticalAlignment = Element.ALIGN_MIDDLE;
                celTitleText.HorizontalAlignment = Element.ALIGN_MIDDLE;
                celTitleText.BorderWidth = 0;
                celTitleText.PaddingBottom = 15;

                // Imagem do cabeçalho...
                iTextSharp.text.Image logoNovo = iTextSharp.text.Image.GetInstance(AppDomain.CurrentDomain.BaseDirectory + "Images/LogoNN_G.png");
                logoNovo.UseVariableBorders = false;
                logoNovo.ScalePercent(30f);
                PdfPCell celTitleImage = new PdfPCell(logoNovo);
                celTitleImage.HorizontalAlignment = Element.ALIGN_CENTER;
                celTitleImage.VerticalAlignment = Element.ALIGN_MIDDLE;
                celTitleImage.BorderWidth = 1;
                celTitleImage.Top = 30;
                celTitleImage.Left = 680;

                // Imagem do rodapé...
                iTextSharp.text.Image logoApta = iTextSharp.text.Image.GetInstance(AppDomain.CurrentDomain.BaseDirectory + "Images/logo-apta.png");                
                PdfPCell celTituloImagemFooter = new PdfPCell(logoApta);
                celTituloImagemFooter.HorizontalAlignment = Element.ALIGN_CENTER;
                celTituloImagemFooter.VerticalAlignment = Element.ALIGN_MIDDLE;
                celTituloImagemFooter.BorderWidth = 0;
                celTituloImagemFooter.PaddingTop = 10;
                logoApta.ScalePercent(40f);
                celTituloImagemFooter.PaddingBottom = 30;


                //Row 1
                PdfPCell pdfCell2 = new PdfPCell(new Phrase("Relatório emitido em " + DateTime.Now.ToShortDateString()));


                String text = "Página " + (Writer.PageNumber != 0 ? Writer.PageNumber : 1) + " de ";


                //Add paging to header
                {
                    cb.BeginText();
                    cb.SetFontAndSize(bf, 12);
                    cb.SetTextMatrix(document.PageSize.GetRight(200), document.PageSize.GetTop(35));
                    cb.EndText();
                    float len = bf.GetWidthPoint(text, 12);
                }
                //Add paging to footer
                {
                    float len = 0;

                    cb.BeginText();

                    var marginRightFooter = 320;
                    if (PrintUser.Length >= 51)
                    {
                        PrintUser = PrintUser.Substring(0, 50);
                        marginRightFooter = 500;
                    }
                    else if (PrintUser.Length >= 45)
                    {
                        marginRightFooter = 470;
                    }
                    else if (PrintUser.Length >= 30)
                    {
                        marginRightFooter = 400;
                    }

                    cb.SetFontAndSize(bf, 9);
                    cb.SetTextMatrix(document.PageSize.GetRight(marginRightFooter), document.PageSize.GetBottom(15));
                    cb.ShowText("Relatório emitido por " + PrintUser + " em " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " + text);
                    cb.EndText();
                    len = bf.GetWidthPoint("Relatório emitido por " + PrintUser + " em " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " + text, 9);
                    cb.AddTemplate(footerTemplate, document.PageSize.GetRight(marginRightFooter) + len, document.PageSize.GetBottom(15));                    
                }
                //Row 2

                PdfPCell pdfCell5 = new PdfPCell(celTitleText);
                pdfCell5.Colspan = 3;
                PdfPCell pdfCell6 = new PdfPCell();
                pdfCell6.PaddingBottom = 30;
                PdfPCell pdfCell7 = new PdfPCell(celTitleImage);
                pdfCell7.Colspan = 3;
                PdfPCell pdfCelApta = new PdfPCell(celTituloImagemFooter);
                pdfCelApta.Colspan = 3;
                
                //set the alignment of all three cells and set border to 0
                pdfCell2.HorizontalAlignment = Element.ALIGN_RIGHT;
                pdfCelApta.HorizontalAlignment = Element.ALIGN_LEFT;

                pdfCell5.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfCell6.HorizontalAlignment = Element.ALIGN_RIGHT;
                pdfCell7.HorizontalAlignment = Element.ALIGN_RIGHT;

                pdfCell2.VerticalAlignment = Element.ALIGN_BOTTOM;
                pdfCell5.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell6.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfCell7.VerticalAlignment = Element.ALIGN_TOP;
                pdfCelApta.VerticalAlignment = Element.ALIGN_MIDDLE;

                //pdfCell1.Border = 0;
                pdfCell2.Border = 0;
                pdfCell5.Border = 0;
                pdfCell6.Border = 0;
                pdfCell7.Border = 0;
                pdfCell7.PaddingRight = 20;
                pdfCelApta.Border = 0;


                //add all three cells into PdfTable

                tabMainTitle.AddCell(pdfCell5);
                pdfTab.AddCell(pdfCell7);                
                tabTituloRodape.AddCell(pdfCelApta);                

                foreach (PdfPCell c in SubTitle)
                {
                    tabMainTitle.AddCell(c);
                }

                foreach (PdfPCell c in SubTitleLocal)
                {
                    tabMainTitle.AddCell(c);
                }


                pdfTab.TotalWidth = document.PageSize.Width;// - 80f;
                pdfTab.WidthPercentage = 99;
                tabMainTitle.TotalWidth = document.PageSize.Width - 20;// - 80f;
                tabMainTitle.WidthPercentage = 90;
                tabTituloRodape.TotalWidth = document.PageSize.Width - 20;// - 80f;
                tabTituloRodape.WidthPercentage = 90;


                //call WriteSelectedRows of PdfTable. This writes rows from PdfWriter in PdfTable
                //first param is start row. -1 indicates there is no end row and all the rows to be included to write
                //Third and fourth param is x and y position to start writing
                pdfTab.WriteSelectedRows(-1, -10, -5, document.PageSize.Height - 5, Writer.DirectContent);
                tabMainTitle.WriteSelectedRows(-1, -1, 10, document.PageSize.Height - 15, Writer.DirectContent);
                tabTituloRodape.WriteSelectedRows(0, 1, 10, 35, Writer.DirectContent);
                tabTituloRodape.WriteSelectedRows(1, 2, 20, 23, Writer.DirectContent);

                //Criando borda na página inteira...
                var content = Writer.DirectContent;
                var pageBorderRect = new Rectangle(document.PageSize);
                pageBorderRect.Left += document.LeftMargin;
                pageBorderRect.Right -= document.RightMargin;
                pageBorderRect.Top -= document.TopMargin - 190;
                pageBorderRect.Bottom += document.BottomMargin - 50;
                content.SetColorStroke(BaseColor.BLACK);
                content.Rectangle(pageBorderRect.Left + 5, pageBorderRect.Bottom - 5, pageBorderRect.Width - 10, pageBorderRect.Height - 90);
                content.Stroke();
            }
            catch (Exception e)
            {
                throw new DomainException(e.Message);
            }

        }

        /// <summary>
        /// Name: OnCloseDocument
        /// Description: event when document is closed
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public override void OnCloseDocument(PdfWriter writerE, Document documentE)
        {
            try
            {

                base.OnCloseDocument(Writer, document);

                headerTemplate.BeginText();
                headerTemplate.SetFontAndSize(bf, 12);
                headerTemplate.SetTextMatrix(0, 0);
                headerTemplate.EndText();

                footerTemplate.BeginText();
                footerTemplate.SetFontAndSize(bf, 9);
                footerTemplate.SetTextMatrix(1, 0);
                footerTemplate.ShowText(((Writer.PageNumber != 0 ? Writer.PageNumber : 1)).ToString());
                footerTemplate.EndText();
            }
            catch (Exception e)
            {
                throw new DomainException(e.Message);
            }

        }

        /// <summary>
        /// Name: CloseDocument
        /// Description: Method that closes the document.
        /// Created by: wazc Programa Novo 2022-09-08 
        /// </summary>
        public string CloseDocument()
        {
            try
            {
                document.Close();
                return Path + "\\" + FileName;
            }
            catch (Exception e)
            {
                throw new DomainException(e.Message);
            }
        }

        #endregion
    }
}

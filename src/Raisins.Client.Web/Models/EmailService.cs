using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Configuration;

using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Diagnostics;
using System.IO;
using System.Drawing.Imaging;

namespace Raisins.Client.Web.Models
{
    public static class EmailService
    {
        public const int NUMBER_OF_COLUMNS = 3;

        public const int NUMBER_OF_ITEMS_PER_PAGE = 21; // Old ticket code for handling two-column tickets. Previous value is 8

        public const int TICKETS_PER_COLUMN = 3; // Old ticket code for handling two-column tickets. Previous value is 2

        public const int X_PIXELS_SPACING = 0;
        
        public const int Y_PIXELS_SPACING = 0;

        public const string TICKET_RANGE_STRING = "Your ticket number(s) are from {0} - {1}.";
        
        public static string[] TD_COLSPAN = new string[3] { "<td>", "<td colspan='3'>", "<td colspan='2'>" };

        public static string SMTPHost { get { return ConfigurationManager.AppSettings["app.smtpHost"]; } }

        public static string FromAddress { get { return ConfigurationManager.AppSettings["app.fromAddress"]; } }

        public static string EmailSubject { get { return ConfigurationManager.AppSettings["app.emailSubject"]; } }

        public static string BaseTicketImage { get { return ConfigurationManager.AppSettings["app.baseTicketImage"]; } }


        /// <summary>
        /// Break a <see cref="List{T}"/> into multiple chunks. The <paramref name="list="/> is cleared out and the items are moved
        /// into the returned chunks.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list to be chunked.</param>
        /// <param name="chunkSize">The size of each chunk.</param>
        /// <returns>A list of chunks.</returns>
        public static List<List<T>> BreakIntoChunks<T>(List<T> list, int chunkSize)
        {
            if (chunkSize <= 0)
            {
                throw new ArgumentException("chunkSize must be greater than 0.");
            }

            List<List<T>> retVal = new List<List<T>>();

            while (list.Count > 0)
            {
                int count = list.Count > chunkSize ? chunkSize : list.Count;
                retVal.Add(list.GetRange(0, count));
                list.RemoveRange(0, count);
            }

            return retVal;
        }

        public static void SendEmail(TicketModel[] tickets, string toAddress, PaymentModel payment)
        {

            try
            {
                tickets.ToList<TicketModel>().Clear();
                using (MailMessage email = new MailMessage())
                {
                    email.Subject = EmailSubject;
                    email.IsBodyHtml = true;
                    email.Body = FormatEmailBody(tickets, payment.Name);
                    email.To.Add(toAddress);
                    email.From = new MailAddress(FromAddress);

                    //Old code supporting ticket images as attachments

                    //string[] fileNames = CreateTicketImages(tickets);
                    //foreach (string fileName in fileNames)
                    //{
                    //    email.Attachments.Add(new Attachment(fileName));
                    //}


                    email.Attachments.Add(new Attachment(CreatePDFFile(tickets)));

                    using (SmtpClient client = new SmtpClient(SMTPHost, 2525))
                    {
                        client.Send(email);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;   
            }
        }

        public static string CreatePDFFile(TicketModel[] tickets)
        {
            string filename = String.Format("D:\\Tickets\\Food for Hungry Minds Ticket Purchase_{0}.pdf", Guid.NewGuid().ToString("D").ToUpper());
            PdfDocument pdf = new PdfDocument();
            pdf.Info.Title = "Tickets from the Food for Hungry Minds High School Education";
            XGraphics gfx = XGraphics.FromPdfPage(pdf.AddPage());
            
            //XRect rect = new XRect(new XPoint(), gfx.PageSize);
            //rect.Inflate(-10, -15);
            //XFont font = new XFont("Georgia", 14, XFontStyle.Bold);
            //gfx.DrawString(String.Format(TICKET_RANGE_STRING, tickets[0].TicketCode, tickets[tickets.Length - 1].TicketCode), font, XBrushes.MidnightBlue, rect, XStringFormats.TopCenter);

            double x = 0;//135;
            double y = 0;//95;
            double yIncrement = 0;
            StringFormat stringFormatFar = new StringFormat();
            stringFormatFar.Alignment = StringAlignment.Far;

            StringFormat stringFormatCenter = new StringFormat();
            stringFormatCenter.Alignment = StringAlignment.Center;
            stringFormatCenter.LineAlignment = StringAlignment.Center;

            TicketModel ticket;

            //iTextSharp.text.Document document = new iTextSharp.text.Document();
            //iTextSharp.text.pdf.PdfWriter.GetInstance(document, new FileStream(filename, FileMode.Create));
            //document.Open();

            for (int i = 0; i < tickets.Length; i++)
            {
                if ((i + 1) % NUMBER_OF_ITEMS_PER_PAGE == 1 && i > TICKETS_PER_COLUMN)
                {
                    gfx = XGraphics.FromPdfPage(pdf.AddPage());
                    yIncrement = 0;
                    x = X_PIXELS_SPACING;//135;
                    y = Y_PIXELS_SPACING + (yIncrement); //95;

                }

                ticket = tickets[i];

                

                using (Image ticketBitmap = new Bitmap(BaseTicketImage))
                {

                    using (Graphics g = Graphics.FromImage(ticketBitmap))
                        {
                            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                            g.DrawString(ticket.TicketCode, new Font("Georgia", 10), Brushes.Black, new Rectangle(0, 0, 325, 35), stringFormatFar);
                            g.DrawString(ticket.Name + "\n(1 Ticket)", new Font("Georgia", 10), Brushes.Black, new RectangleF(0, 0, 400, 200), stringFormatCenter);
                            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                        }

                        // Old ticket code for handling two-column tickets. TICKETS_PER_COLUMN is 2.

                        if (i % TICKETS_PER_COLUMN == 0)
                        {
                            x = X_PIXELS_SPACING;
                            y = Y_PIXELS_SPACING + (yIncrement);
                            yIncrement += 110;//ticketBitmap.PhysicalDimension.Height;
                        }
                        else
                        {
                            x += 180; //X_PIXELS_SPACING + ticketBitmap.PhysicalDimension.Width;
                        }

                        //MemoryStream imageStream = new MemoryStream();
                        //ticketBitmap.Save(imageStream, ImageFormat.Jpeg);

                        //byte[] imageContent = new Byte[imageStream.Length];
                        //imageStream.Position = 0;
                        //imageStream.Read(imageContent, 0, (int)imageStream.Length);

                        //iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imageContent);
                    

                        //x = 135;
                        gfx.DrawImage(ticketBitmap, x, y, 180, 110);
                        //y += ticketBitmap.PhysicalDimension.Height;
                        //document.Add(image);
                        
                    }
                
            }
            //document.Close();

            pdf.Save(filename);
            //Uncomment next line if needed to see PDF file already
            Process.Start(filename);

            return filename;
        }

        public static string FormatEmailBody(TicketModel[] tickets, string paymentName)
        {
            //string[] fileNames = CreateTicketImages(tickets);
            StringBuilder sb = new StringBuilder();

            sb.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>");
            sb.Append("<html><head><title>Tickets Purchased from the Food for Hungry Minds School</title></head>");
            sb.Append("<body>");
            sb.Append("<p>");
            sb.Append("Dear " + paymentName + ",");
            sb.Append("</p>");

            sb.Append("<p>");
            sb.Append("Thank you for supporting the Food for Hungry Minds School. Your donation will be used for the high school scholarship funds of the HMS students.");
            sb.Append("</p>");

            sb.Append("<p>");
            sb.Append("Attached are the tickets. Kindly take note of the number on each ticket's upper-right hand corner.  This number is the unique identifier for the ticket you purchased which will be included during the raffling of the prizes on Dec 15 2010 (internal voters) and Dec 23 2010 (external voters).");
            sb.Append("</p>");

            sb.Append("<p>");
            sb.Append("Again, our sincerest thanks for your kind donation.");
            sb.Append("</p>");

            sb.Append("<p>");
            sb.Append("Regards,");
            sb.Append("</p>");

            sb.Append("<p>");
            sb.Append("The Food for Hungry Minds Committee, Manila");
            sb.Append("</p>");

            sb.Append("<p>");
            sb.Append("This is an auto-generated email. For inquiries, Please visit our web site at <a href='www.foodforhungryminds.org/'> http://www.foodforhungryminds.org/</a>.");
            sb.Append("</p>");

            //sb.Append("<table>");

            //int ticketCount = tickets.Length;
            
            //if (ticketCount > 0)
            //{

            //    for (int i = 0; i < ticketCount; i++)
            //    {
            //        TicketModel ticket = tickets[i];

            //        if ((i + 1) % NUMBER_OF_COLUMNS == 1)
            //        {
            //            sb.Append("<tr>");
            //        }

            //        string tdColspan = TD_COLSPAN[0];
            //        if (i + 1 > NUMBER_OF_COLUMNS && i + 1 == ticketCount)
            //        {
            //            tdColspan = TD_COLSPAN[(i + 1) % NUMBER_OF_COLUMNS];
            //        }

            //        sb.Append(tdColspan);

            //        //string imageFileName = CreateTicketImage(ticket.TicketCode, ticket.Name);
                    
            //        //sb.Append("<img src='" + imageFileName + "' alt='" + imageFileName + "' />");
            //        sb.Append("<img src='" + fileNames[i] + "' alt='" + fileNames[i] + "' />");

            //        sb.Append("</td>");

            //        if ((i + 1) % NUMBER_OF_COLUMNS == 0 || i + 1 == ticketCount)
            //        {
            //            sb.Append("</tr>");
            //        }
            //    }

            //}
            //sb.Append("</table>");

            sb.Append("</body>");
            sb.Append("</html>");
            
            return sb.ToString();
        }

        public static string[] CreateTicketImages(TicketModel[] tickets)
        {
            
            List<string> fileNames = new List<string>();
            
            foreach (TicketModel ticket in tickets)
            {
                fileNames.Add(CreateTicketImage(ticket.TicketCode, ticket.Name));
            }
            
            return fileNames.ToArray();
        }

        public static string CreateTicketImage(string ticketCode, string ticketName)
        {
            string fileName = "C:\\Tickets\\"+ ticketCode + ".jpg";

            Image myBitmap = new Bitmap("C:\\ticket1.jpg");
            Graphics g = Graphics.FromImage(myBitmap);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            StringFormat stringFormatFar = new StringFormat();
            stringFormatFar.Alignment = StringAlignment.Far;

            g.DrawString(ticketCode, new Font("Georgia", 12), Brushes.Black, new Rectangle(0, 0, 325, 35), stringFormatFar);

            StringFormat stringFormatCenter = new StringFormat();
            stringFormatCenter.Alignment = StringAlignment.Center;
            stringFormatCenter.LineAlignment = StringAlignment.Center;

            g.DrawString(ticketName +"\n(1 Ticket)", new Font("Georgia", 12), Brushes.Black, new RectangleF(0, 0, 400, 200), stringFormatCenter);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            myBitmap.Save(fileName);
            myBitmap.Dispose();
            g.Dispose();

            return fileName;

        }
    }

}
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

namespace Raisins.Client.Web.Models
{
    public static class EmailService
    {
        public const int NUMBER_OF_COLUMNS = 3;
        
        public static string[] TD_COLSPAN = new string[3] { "<td>", "<td colspan='3'>", "<td colspan='2'>" };

        public static string SMTPHost { get { return ConfigurationManager.AppSettings["app.smtpHost"]; } }

        public static string FromAddress { get { return ConfigurationManager.AppSettings["app.fromAddress"]; } }

        public static string EmailSubject { get { return ConfigurationManager.AppSettings["app.emailSubject"]; } }

        public static void SendEmail(TicketModel[] tickets, string toAddress, PaymentModel payment)
        {

            try
            {
                using (MailMessage email = new MailMessage())
                {
                    email.Subject = EmailSubject;
                    email.IsBodyHtml = true;
                    email.Body = FormatEmailBody(tickets, payment.Name);
                    email.To.Add(toAddress);
                    email.From = new MailAddress(FromAddress);
                    
                    string[] fileNames = CreateTicketImages(tickets);
                    foreach (string fileName in fileNames)
                    {
                        email.Attachments.Add(new Attachment(fileName));
                    }

                    using (SmtpClient client = new SmtpClient(SMTPHost))
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

            Bitmap myBitmap = new Bitmap("C:\\ticket1.jpg");
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
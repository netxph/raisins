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

        public static void SendEmail(TicketModel[] tickets, string toAddress)
        {

            try
            {
                using (MailMessage email = new MailMessage())
                {
                    email.Subject = EmailSubject;
                    email.IsBodyHtml = true;
                    email.Body = FormatEmailBody(tickets);
                    email.To.Add(toAddress);
                    email.From = new MailAddress(FromAddress);

                    using (SmtpClient client = new SmtpClient(SMTPHost))
                    {
                        client.Send(email);
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        public static string FormatEmailBody(TicketModel[] tickets)
        {

            StringBuilder sb = new StringBuilder();

            sb.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>");
            sb.Append("<html><head><title>Tickets Purchased from the Food for Hungry Minds School</title></head>");
            sb.Append("<body>");
            
            sb.Append("<table>");

            int ticketCount = tickets.Length;
            
            if (ticketCount > 0)
            {

                for (int i = 0; i < ticketCount; i++)
                {
                    TicketModel ticket = tickets[i];

                    if ((i + 1) % NUMBER_OF_COLUMNS == 1)
                    {
                        sb.Append("<tr>");
                    }

                    string tdColspan = TD_COLSPAN[0];
                    if (i + 1 > NUMBER_OF_COLUMNS && i + 1 == ticketCount)
                    {
                        tdColspan = TD_COLSPAN[(i + 1) % NUMBER_OF_COLUMNS];
                    }

                    sb.Append(tdColspan);
                    
                    sb.Append("<img src='" + CreateTicketImage(ticket.TicketCode, ticket.Name) + "' />");

                    sb.Append("</td>");

                    if ((i + 1) % NUMBER_OF_COLUMNS == 0 || i + 1 == ticketCount)
                    {
                        sb.Append("</tr>");
                    }
                }

            }
            sb.Append("</table>");

            sb.Append("</body>");
            sb.Append("</html>");
            
            return sb.ToString();
        }

        public static string CreateTicketImage(string ticketCode, string ticketName)
        {
            string fileName = "D:\\"+ ticketCode + ".png";

            Bitmap myBitmap = new Bitmap("D:\\ticket1.jpg");
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
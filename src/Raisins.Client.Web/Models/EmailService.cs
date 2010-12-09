using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;

namespace Raisins.Client.Web.Models
{
    public static class EmailService
    {

        public static void SendEmail(TicketModel[] tickets, string toAddress)
        {

            try
            {
                using (MailMessage email = new MailMessage())
                {
                    email.Subject = "Tickets Purchased from the Food for Hungry Minds School";
                    email.IsBodyHtml = true;
                    email.Body = FormatEmailBody(tickets);
                    email.To.Add(toAddress);
                    email.From = new MailAddress("noreply@navitaire.com");

                    using (SmtpClient client = new SmtpClient("192.168.23.73"))
                    {
                        //client.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
                        client.Send(email);
                    }
                }

            }
            catch (SocketException se)
            {

            }
        }

        public static string FormatEmailBody(TicketModel[] tickets)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>");
            sb.Append(@"<html><head><title>Print</title><link href='/Content/Reset.css' rel='stylesheet' type='text/css' /><link href='/Content/Main.css' rel='stylesheet' type='text/css' /></head>");
            sb.Append(@"<body id='print'><div><div id='printticket'>");
            foreach (TicketModel ticket in tickets)
            {
                sb.Append(@"<div class='item'>");
                sb.Append(@"<span class='code'>");
                sb.Append(ticket.TicketCode);
                sb.Append(@"</span>");
                sb.Append(@"<p class='name'>");
                sb.Append(ticket.Name);
                sb.Append(@"</p>");
                sb.Append(@"<p class='vote'>(1 Ticket)</p>");
                sb.Append(@"</div>");
            }
            sb.Append(@"</div></div></body></html>");
            return sb.ToString();
        }
    }
}
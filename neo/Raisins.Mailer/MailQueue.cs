namespace Raisins.Mailer;

public class MailQueue
{
   public int PaymentID { get; set; }
   public bool Status { get; set; }
   public string To { get; set; }
   public string Name { get; set; }
   public string Beneficiary { get; set; }
   public decimal Amount { get; set; }
   public List<Ticket> Tickets { get; set; }

   public MailQueue()
   {
      To = string.Empty;
      Name = string.Empty;
      Beneficiary = string.Empty;
      Tickets = new List<Ticket>();
   }

}

public class Ticket
{
   public string Code { get; set; }

   public Ticket()
   {
      Code = string.Empty;
   }
}

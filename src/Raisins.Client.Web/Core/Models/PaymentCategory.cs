namespace Raisins.Client.Web.Models
{
    public class PaymentCategory
    {
        public int PlatinumPaymentAmount { get; set; }
        public int PlatinumPaymentVotes { get; set; }

        public int GoldPaymentAmount { get; set; }
        public int GoldPaymentVotes { get; set; }

        public int SilverPaymentAmount { get; set; }
        public int SilverPaymentVotes { get; set; }

        public int BronzePaymentAmount { get; set; }
        public int BronzePaymentVotes { get; set; }
    }
}
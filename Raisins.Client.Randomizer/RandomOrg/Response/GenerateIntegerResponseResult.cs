namespace Raisins.Client.Randomizer.RandomOrg.Response
{
    public class GenerateIntegerResponseResult
    {
        public GenerateIntegerResponseRandom Random { get; set; }
        public int BitsUsed { get; set; }
        public int BitsLeft { get; set; }
        public int RequestsLeft { get; set; }
        public int AdvisoryDelay { get; set; }
    }
}
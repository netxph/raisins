namespace Raisins.Client.Randomizer.RandomOrg.Response
{
    public class GenerateIntegerResponseResult
    {
        public GenerateIntegerResponseRandom random { get; set; }
        public int bitsUsed { get; set; }
        public int bitsLeft { get; set; }
        public int requestsLeft { get; set; }
        public int advisoryDelay { get; set; }
    }
}
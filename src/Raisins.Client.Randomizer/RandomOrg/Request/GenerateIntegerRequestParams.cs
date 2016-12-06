namespace Raisins.Client.Randomizer.RandomOrg.Request
{
    public class GenerateIntegerRequestParams
    {
        public string apiKey { get; set; }
        public int n { get; set; }
        public int min { get; set; }
        public int max { get; set; }
        public bool replacement { get; set; }
        public int @base { get; set; }
    }
}
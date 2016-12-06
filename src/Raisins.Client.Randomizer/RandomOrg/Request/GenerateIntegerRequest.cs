namespace Raisins.Client.Randomizer.RandomOrg.Request
{
    public class GenerateIntegerRequest
    {
        public string jsonrpc { get; set; }
        public string method { get; set; }
        public GenerateIntegerRequestParams @params { get; set; }
        public int id { get; set; }
    }
}

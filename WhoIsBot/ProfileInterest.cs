namespace WhoIsBot
{
    public class ProfileInterest
    {
        public ulong ProfileId { get; set; }
        public string Category { get; set; }
        public string Username { get; set; }
        public double ConfidenceScore { get; set; }
        public string SubCategory { get; set; }
        public string Text { get; set; }
    }
}
namespace API
{
    public class EmailSettings
    {
        public string To { get; set; }
        public string From { get; set; }
        public string SMTPHost { get; set; }
        public int SMTPPort { get; set; }
        public string Password { get; set; }
        public bool SSLEnabled { get; set; }
    }
}

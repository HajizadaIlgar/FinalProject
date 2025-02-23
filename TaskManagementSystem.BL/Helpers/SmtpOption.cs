namespace TaskManagementSystem.BL.Helpers
{
    public class SmtpOption
    {
        public const string Name = "SmtpSettings";
        public string Host { get; set; }
        public int Port { get; set; }
        public string Sender { get; set; }
        public string Password { get; set; }
    }
}

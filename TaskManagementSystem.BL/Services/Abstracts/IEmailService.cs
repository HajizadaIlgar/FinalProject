namespace TaskManagementSystem.BL.Services.Abstracts
{
    public interface IEmailService
    {
        void SendEMail(string email, string subject, string body);
    }
}

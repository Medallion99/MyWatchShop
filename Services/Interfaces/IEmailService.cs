namespace MyWatchShop.Services.Interfaces
{
    public interface IEmailService
    {
        Task<string> SendEmailAsync(string emailRecipient, string subject, string body);
    }
}
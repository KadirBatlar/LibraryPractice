namespace HangFire.Web.Services
{
    interface IMailService
    {
        Task Sender(string userMail, string message);
    }
}
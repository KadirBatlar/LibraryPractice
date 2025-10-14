using HangFire.Web.Services;

namespace HangFire.Web.BackgroundJobs
{
    public class FireAndForgetJobs
    {        
        public static void EmailSendToUserJob(string userMail, string message)
        {
            Hangfire.BackgroundJob.Enqueue<IMailService>(x=>x.Sender(userMail, message));
        }
    }
}
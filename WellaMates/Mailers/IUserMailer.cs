using Mvc.Mailer;
using WellaMates.Models;

namespace WellaMates.Mailers
{ 
    public interface IUserMailer
    {
		MvcMailMessage Welcome();
        MvcMailMessage Contact(string name, string email, string title, string message, string logoPath);
        MvcMailMessage ForgotPassword(string email, string recoverPasswordKey, int userId, string logoPath);

        MvcMailMessage EventResponseNotification(string email, string controller, string sender, Event Event, RefundItemUpdate[] updates, string logoPath);
        MvcMailMessage VisitResponseNotification(string email, string controller, string sender, Visit Visit, RefundItemUpdate[] updates, string logoPath);
        MvcMailMessage MonthlyResponseNotification(string email, string controller, string sender, Monthly monthly, RefundItemUpdate[] updates, string logoPath);

        MvcMailMessage SendEventNotification(string email, string eventUrl, string sender, string eventName, string logoPath);
        MvcMailMessage SendVisitNotification(string email, string visitUrl, string sender, string visitDate, string logoPath);
        MvcMailMessage SendMonthlyNotification(string email, string monthlyUrl, string sender, string month, string logoPath);
    }
}
using System.Collections.Generic;
using System.Net.Mail;
using JetBrains.Annotations;
using Mvc.Mailer;
using WellaMates.Models;

namespace WellaMates.Mailers
{ 
    public sealed class UserMailer : MailerBase, IUserMailer
	{
		public UserMailer()
		{
            MasterName = "_Layout";
		}
		
		public MvcMailMessage Welcome()
		{
			//ViewBag.Data = someObject;
			return Populate(x =>
			{
				x.Subject = "Welcome";
				x.ViewName = "Welcome";
				x.To.Add("some-email@example.com");
			});
		}

        public MvcMailMessage Contact(string name, string email, string title, string message, string logoPath)
        {
            ViewBag.Name = name;
            ViewBag.Email = email;
            ViewBag.Title = title;
            ViewBag.Message = message;
            var mailMessage = Populate(x =>
            {
                x.Subject = "Wella Educação Contato - " + title;
                x.From = new MailAddress("equipe@wellaeducacao.com.br", "Equipe Wella Educação");
                x.ViewName = "Contact";
                x.To.Add("equipe@wellaeducacao.com.br");
                x.To.Add("brunotachinardi@hotmail.com");
            });
            var resources = new Dictionary<string, string>();
            resources["logo"] = logoPath;
            PopulateBody(mailMessage, "Contact", resources);
            return mailMessage;
        }

        public MvcMailMessage ForgotPassword(string email, string recoverPasswordKey, int userId, string logoPath)
		{
            ViewBag.RecoverPasswordKey = recoverPasswordKey;
            ViewBag.UserID = userId;
            var message = Populate(x =>
			{
                x.Subject = "Esqueci Minha Senha";
                x.From = new MailAddress("equipe@wellaeducacao.com.br", "Equipe Wella Educação");
                x.ViewName = "ForgotPassword";
                x.To.Add(email);
            }); 
            var resources = new Dictionary<string, string>();
            resources["logo"] = logoPath;
            PopulateBody(message, "ForgotPassword", resources);
		    return message;
		}

        public MvcMailMessage SendEventNotification(string email, string eventUrl, string sender, string eventName, string logoPath)
        {
            ViewBag.EventUrl = eventUrl;
            ViewBag.Sender = sender;
            ViewBag.EventName = eventName;
            var message = Populate(x =>
            {
                x.Subject = string.Format("Nova requisição de reembolso por {0}: Evento \"{1}\"", sender, eventName);
                x.From = new MailAddress("equipe@wellaeducacao.com.br", "Equipe Wella Educação");
                x.ViewName = "SendEventNotification";
                x.To.Add(email);
            });
            var resources = new Dictionary<string, string>();
            resources["logo"] = logoPath;
            PopulateBody(message, "SendEventNotification", resources);
            return message;
        }

        public MvcMailMessage SendVisitNotification(string email, string visitUrl, string sender, string visitDate, string logoPath)
        {
            ViewBag.VisitUrl = visitUrl;
            ViewBag.Sender = sender;
            ViewBag.VisitDate = visitDate;
            var message = Populate(x =>
            {
                x.Subject = string.Format("Nova requisição de reembolso por {0}: Visita de \"{1}\"", sender, visitDate);
                x.From = new MailAddress("equipe@wellaeducacao.com.br", "Equipe Wella Educação");
                x.ViewName = "SendVisitNotification";
                x.To.Add(email);
            });
            var resources = new Dictionary<string, string>();
            resources["logo"] = logoPath;
            PopulateBody(message, "SendVisitNotification", resources);
            return message;
        }

        public MvcMailMessage SendMonthlyNotification(string email, string monthlyUrl, string sender, string month, string logoPath)
        {
            ViewBag.MonthlyUrl = monthlyUrl;
            ViewBag.Sender = sender;
            ViewBag.Month = month;
            var message = Populate(x =>
            {
                x.Subject = string.Format("Nova requisição de reembolso por {0}: Mensal do Mês de \"{1}\"", sender, month);
                x.From = new MailAddress("equipe@wellaeducacao.com.br", "Equipe Wella Educação");
                x.ViewName = "SendMonthlyNotification";
                x.To.Add(email);
            });
            var resources = new Dictionary<string, string>();
            resources["logo"] = logoPath;
            PopulateBody(message, "SendMonthlyNotification", resources);
            return message;
        }

        public MvcMailMessage EventResponseNotification(string email, [AspMvcController]string controller, string sender, Event Event, RefundItemUpdate[] updates, string logoPath)
        {
            ViewBag.Controller = controller;
            ViewBag.Sender = sender;
            ViewBag.Event = Event;
            ViewBag.Updates = updates;
            var message = Populate(x =>
            {
                x.Subject = string.Format("Resposta de {0} na requisição do evento \"{1}\"", sender, Event.Name);
                x.From = new MailAddress("equipe@wellaeducacao.com.br", "Equipe Wella Educação");
                x.ViewName = "EventResponseNotification";
                x.To.Add(email);
            });
            var resources = new Dictionary<string, string>();
            resources["logo"] = logoPath;
            PopulateBody(message, "EventResponseNotification", resources);
            return message;
        }

        public MvcMailMessage VisitResponseNotification(string email, [AspMvcController]string controller, string sender, Visit Visit, RefundItemUpdate[] updates, string logoPath)
        {
            ViewBag.Controller = controller;
            ViewBag.Sender = sender;
            ViewBag.Visit = Visit;
            ViewBag.Updates = updates;
            var message = Populate(x =>
            {
                x.Subject = string.Format("Resposta de {0} na requisição da visita de \"{1}\"", sender, Visit.Date.ToString("dd/MM/yyyy"));
                x.From = new MailAddress("equipe@wellaeducacao.com.br", "Equipe Wella Educação");
                x.ViewName = "VisitResponseNotification";
                x.To.Add(email);
            });
            var resources = new Dictionary<string, string>();
            resources["logo"] = logoPath;
            PopulateBody(message, "VisitResponseNotification", resources);
            return message;
        }

        public MvcMailMessage MonthlyResponseNotification(string email, [AspMvcController]string controller, string sender, Monthly monthly, RefundItemUpdate[] updates, string logoPath)
        {
            ViewBag.Controller = controller;
            ViewBag.Sender = sender;
            ViewBag.Monthly = monthly;
            ViewBag.Updates = updates;
            var message = Populate(x =>
            {
                x.Subject = string.Format("Resposta de {0} na requisição do mês de \"{1}\"", sender, monthly.Month);
                x.From = new MailAddress("equipe@wellaeducacao.com.br", "Equipe Wella Educação");
                x.ViewName = "MonthlyResponseNotification";
                x.To.Add(email);
            });
            var resources = new Dictionary<string, string>();
            resources["logo"] = logoPath;
            PopulateBody(message, "MonthlyResponseNotification", resources);
            return message;
        }

        public MvcMailMessage CloseRefundNotification(string email, [AspMvcController]string controller, string sender, IRefundOwner refundOwner, string logoPath)
        {
            ViewBag.Controller = controller;
            ViewBag.Sender = sender;
            ViewBag.PaymentDay = refundOwner.Refund.PaymentDate.ToString("dd/MM/yyyy");
            string viewName;
            string subject;
            if (refundOwner is Monthly)
            {
                ViewBag.Monthly = refundOwner;
                viewName = "CloseMonthlyRefundNotification";
                subject = string.Format("Resposta de {0} na requisição do mês de \"{1}\"", sender,
                    ((Monthly) refundOwner).Month);
            }
            else if (refundOwner is Event)
            {
                ViewBag.Event = refundOwner;
                viewName = "CloseEventRefundNotification";
                subject = string.Format("Resposta de {0} na requisição do evento \"{1}\"", sender,
                    ((Event) refundOwner).Name);
            }
            else
            {
                ViewBag.Visit = refundOwner;
                viewName = "CloseVisitRefundNotification";
                subject = string.Format("Resposta de {0} na requisição da visita de \"{1}\"", sender,
                    ((Visit) refundOwner).Date.ToString("dd/MM/yyyy"));
            }
            var message = Populate(x =>
            {
                x.Subject = subject;
                x.From = new MailAddress("equipe@wellaeducacao.com.br", "Equipe Wella Educação");
                x.ViewName = viewName;
                x.To.Add(email);
            });
            var resources = new Dictionary<string, string>();
            resources["logo"] = logoPath;
            PopulateBody(message, viewName, resources);
            return message;
        }
	}
}
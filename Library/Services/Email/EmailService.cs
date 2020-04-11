using System;
using System.IO;
using System.Net.Mail;
using System.Text;
using Library.Data.Models;
using log4net;
using Microsoft.Extensions.Options;

namespace Library.Services.Email
{
	public class EmailService : IEmailService
	{
		private readonly EmailSettings _appSettings;
		private readonly log4net.ILog log = LogManager.GetLogger(typeof(EmailService));
		public EmailService( IOptions<EmailSettings> appSettings  )
		{
			_appSettings = appSettings.Value;
		}

		public void SendEmail( Models.Email.Email email )
		{
			try
			{
				log.Debug($"Sending email: {email.Recipients.EmailAddress}");
				using (var smtp = new SmtpClient())
				{
					smtp.Host = _appSettings.Host;
					smtp.Port = Convert.ToInt32(_appSettings.Port);
					smtp.Credentials = new System.Net.NetworkCredential(_appSettings.User, _appSettings.Password);
					smtp.EnableSsl = true;
					using (var msg = new MailMessage())
					{
						msg.BodyEncoding = Encoding.UTF8;
						msg.Body = null;

						//add html view
						AlternateView htmlview = AlternateView.CreateAlternateViewFromString(email.Message.MessageBody, msg.BodyEncoding, "text/html");
						htmlview.TransferEncoding = System.Net.Mime.TransferEncoding.QuotedPrintable;
						msg.AlternateViews.Add(htmlview);

						msg.Subject = email.Message.Subject;
						msg.From = new MailAddress(email.Message.ReplyAddress, email.Message.FromName);

						msg.To.Add(new MailAddress(email.Recipients.EmailAddress));

						smtp.Send(msg);
					}
					log.Debug("mail sent: success");
				}
				//Update recipient
				email.Recipients.Status = "Sent";
			}
			catch (Exception ex)
			{
				log.Error($"Send Email Error:{ex.Message } {ex?.InnerException?.Message}");
				throw; 
			}
		}
	}
}

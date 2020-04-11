namespace Library.Services.Email
{
	public interface IEmailService
	{
		void SendEmail( Library.Models.Email.Email Email);
	}
}
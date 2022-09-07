namespace PaymentContext.Domain.Services.Email;

public interface IEmailService
{
    void Send(string to, string email, string subject, string body);
}
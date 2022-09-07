namespace PaymentContext.Domain.Repositories.Student;

using Entities;

public interface IStudentRepository
{
    bool DocumentExists(string document);
    bool EmailExists(string email);
    void CreateSubscription(Student student);
}
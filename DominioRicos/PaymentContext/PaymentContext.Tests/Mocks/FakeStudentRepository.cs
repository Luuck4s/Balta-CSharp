using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repositories.Student;

namespace PaymentContext.Tests.Mocks;

public class FakeStudentRepository: IStudentRepository
{
    public bool DocumentExists(string document)
    {
        if (document == "99999999999")
        {
            return true;
        }

        return false;
    }

    public bool EmailExists(string email)
    {
        if (email == "batman@dc.com")
        {
            return true;
        }

        return false;
    }

    public void CreateSubscription(Student student)
    {
        
    }
}
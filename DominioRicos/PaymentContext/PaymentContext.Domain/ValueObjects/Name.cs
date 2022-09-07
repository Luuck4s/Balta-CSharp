using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects;

public class Name : ValueObject
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    public Name(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        

        AddNotifications(
            new Contract<Name>()
                .Requires()
                .IsNotNullOrEmpty(
                    FirstName, 
                    "Name.FirstName", 
                "Invalid First Name"),
                new Contract<Name>()
                .Requires()
                .IsNotNullOrEmpty(
                    LastName, 
                    "Name.LastName", 
                    "Invalid Last Name")
            );
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName}";
    }
}
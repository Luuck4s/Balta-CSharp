﻿using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects;

public class Address : ValueObject
{
    public string Street { get; private set; }
    public string Number { get; private set; }
    public string Neighborhood { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }
    public string ZipCode { get; private set; }

    public Address(string street, string number, string neighborhood, string city, string state, string country,
        string zipCode)
    {
        Street = street;
        Number = number;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;


        AddNotifications(
            new Contract<Address>()
                .Requires()
                .IsGreaterThan(
                    Street, 
                    3, 
                    "Address.Street", 
                    "Street should have at least 3 chars"
                )
                .IsLowerThan(Street, 
                    40, 
                    "Address.Street", 
                    "Street should have no more than 40 chars"
                ));
    }
}
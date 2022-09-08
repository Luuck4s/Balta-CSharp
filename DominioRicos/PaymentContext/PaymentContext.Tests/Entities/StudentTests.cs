using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities;

[TestClass]
public class StudentTests
{
    private readonly Student _student;
    private readonly Subscription _subscription;
    private readonly Document _document;
    private readonly Email _email;
    private readonly Address _address;

    public StudentTests()
    {
        var name = new Name("Bruce", "Wayne");
        _document = new Document("74257108894", EDocumentType.CPF);
        _email = new Email("batman@dc.com");
        _address = new Address(
            "Rua 1",
            "1234",
            "Bairro",
            "Gotham",
            "SP",
            "BR",
            "1340000"
        );
        _student = new Student(
            name,
            _document,
            _email
        );
        _subscription = new Subscription(null);
    }

    [TestMethod]
    public void ShouldReturnErrorWhenHadActiveSubscription()
    {
        var payment = new PayPalPayment(
            "12345678",
            DateTime.Now,
            DateTime.Now.AddDays(5),
            10,
            10,
            _document,
            "bruce",
            _address,
            _email
        );

        _subscription.AddPayment(payment);
        _student.AddSubscription(_subscription);
        _student.AddSubscription(_subscription);

        Assert.IsFalse(_student.IsValid);
    }

    [TestMethod]
    public void ShouldReturnErrorWhenHadSubscriptionHasNoPayment()
    {
        _student.AddSubscription(_subscription);
        Assert.IsFalse(_student.IsValid);
    }

    [TestMethod]
    public void ShouldReturnSuccessWhenHadNoActiveSubscription()
    {
        var payment = new PayPalPayment(
            "12345678",
            DateTime.Now,
            DateTime.Now.AddDays(5),
            10,
            10,
            _document,
            "bruce",
            _address,
            _email
        );
        _subscription.AddPayment(payment);
        _student.AddSubscription(_subscription);
        Assert.IsTrue(_student.IsValid);
    }
}
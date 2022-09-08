using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.Handlers;

[TestClass]
public class SubscriptionHandlerTests
{
    [TestMethod]
    public void ShouldReturnErrorWhenDocumentExists()
    {
        var handler = new SubscriptionHandler(
            new FakeStudentRepository(), 
            new FakeEmailService()
        );
        var command = new CreateBoletoSubscriptionCommand();
        command.FirstName = "Batman";
        command.LastName = "Dc";
        command.Document = "99999999999";
        command.Email = "batma123@dc.com";
        command.BardCode = "123456";
        command.BoletoNumber = "111234";
        command.PaymentNumber = "";
        command.PaidDate = DateTime.Now;
        command.ExpireDate = DateTime.Now.AddDays(1);
        command.Total = 10;
        command.TotalPaid = 10;
        command.Payer = "Batman";
        command.PayerEmail = "batman@dc.com";
        command.PayerDocument = "99999999999";
        command.PayerDocumentType = EDocumentType.CPF;
        command.Street = "Rua 123";
        command.Number = "123";
        command.Neighborhood = "Bairro";
        command.City = "City";
        command.State = "SP";
        command.Country = "BR";
        command.ZipCode = "123456789";
        
        handler.Handle(command);
        Assert.AreEqual(false, handler.IsValid);
    }
}
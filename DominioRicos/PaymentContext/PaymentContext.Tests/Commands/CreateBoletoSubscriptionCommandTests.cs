using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;

namespace PaymentContext.Tests.Commands;

[TestClass]
public class CreateBoletoSubscriptionCommandTests
{
    /*
     * Teste de exemplo
     * (pois dentro desse command não tem regras para serem validadas
     */
    [TestMethod]
    public void ShouldReturnErrorWhenNameIsInvalid()
    {
        var command = new CreateBoletoSubscriptionCommand();
        command.FirstName = String.Empty;
        
        command.Validate();
        
        Assert.AreEqual(false, command.IsValid);
    }
}
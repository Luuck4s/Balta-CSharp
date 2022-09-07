using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Enuns;
using Document = PaymentContext.Domain.ValueObjects.Document;

namespace PaymentContext.Tests.ValueObjects;

[TestClass]
public class DocumentTests
{
    [TestMethod]
    [DataTestMethod]
    [DataRow("34050241000168")]
    [DataRow("05842653000138")]
    [DataRow("76707035000128")]
    public void ShouldReturnErrorWhenCnpjIsInvalid(string cnpj)
    {
        var doc = new Document(cnpj, EDocumentType.CNPJ);
        Assert.IsFalse(doc.IsValid);
    }
    
    [TestMethod]
    [DataTestMethod]
    [DataRow("07243781000194")]
    [DataRow("84004031000100")]
    [DataRow("28677543000167")]
    public void ShouldReturnSuccessWhenCnpjValid(string cnpj)
    {
        var doc = new Document(cnpj, EDocumentType.CNPJ);
        Assert.IsTrue(doc.IsValid);
    }
    
    [TestMethod]
    [DataTestMethod]
    [DataRow("23143017491")]
    [DataRow("10875882278")]
    [DataRow("96236624676")]
    public void ShouldReturnErrorWhenCpfIsInvalid(string cpf)
    {
        var doc = new Document(cpf, EDocumentType.CPF);
        Assert.IsFalse(doc.IsValid);
    }
    
    [TestMethod]
    [DataTestMethod]
    [DataRow("72671640307")]
    [DataRow("56336650316")]
    [DataRow("73854588070")]
    public void ShouldReturnSuccessWhenCpfValid(string cpf)
    {
        var doc = new Document(cpf, EDocumentType.CPF);
        Assert.IsTrue(doc.IsValid);
    }
}
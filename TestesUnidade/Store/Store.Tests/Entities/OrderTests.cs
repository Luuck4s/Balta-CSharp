using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Entities;
using Store.Domain.Enums;

namespace Store.Tests.Entities;

[TestClass]
public class OrderTests
{
    private readonly Order _orderValid;
    private readonly Customer _customer;
    private readonly Product _product;
    private readonly Discount _discountValidWithAmountZero;
    
    public OrderTests()
    {
        _customer = new Customer("Batman", "batman@dc.com");
        _discountValidWithAmountZero = new Discount(0, DateTime.Now);
        _orderValid = new Order(_customer, 0, _discountValidWithAmountZero);
        _product = new Product("Camiseta", 10, true);
    }


    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldSuccessGenerateNumberWhenReceiveAnValidOrder()
    {
        Assert.AreEqual(8, _orderValid.Number.Length);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldStatusOrderWaitingPaymentWhenReceiveAnValidOrder()
    {
        Assert.AreEqual(EOrderStatus.WaitingPayment, _orderValid.Status);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldStatusWaitingDeliveryWhenReceiveAnValidPaymentOrder()
    {
        _orderValid.AddItem(_product, 1);
        _orderValid.Pay(10);
        Assert.AreEqual(10, _orderValid.Total());
        Assert.AreEqual(EOrderStatus.WaitingDelivery, _orderValid.Status);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldStatusCanceledWhenCancelOrder()
    {
        _orderValid.Cancel();
        Assert.AreEqual(EOrderStatus.Canceled, _orderValid.Status);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldFailWhenTryAddAnInvalidOrderItem()
    {
        _orderValid.AddItem(null, 10);
        Assert.AreEqual(0, _orderValid.Items.Count);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldFailWhenTryAddAnInvalidOrderWithQuantityLessThanZero()
    {
        _orderValid.AddItem(_product, 0);
        Assert.AreEqual(0, _orderValid.Items.Count);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow(2)]
    [DataRow(10)]
    [DataRow(8)]
    [TestCategory("Domain")]
    public void ShouldSuccessReturningAnValidTotalOrder(int quantityProduct)
    {
        _orderValid.AddItem(_product, quantityProduct);
        
        Assert.AreEqual(_product.Price * quantityProduct, _orderValid.Total());
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow(5, 10F)]
    [DataRow(4, 20.3F)]
    [DataRow(9, 15.80F)]
    [TestCategory("Domain")]
    public void ShouldSuccessReturningAnValidTotalOrderWithDiscountApplied(int quantityProduct, float discountAmount)
    {
        var discount = new Discount((decimal) discountAmount, DateTime.Now.AddDays(2));
        var order = new Order(_customer, 0, discount);
        order.AddItem(_product, quantityProduct);
        var totalExpected = _product.Price * quantityProduct - discount.Value();
        Assert.AreEqual(totalExpected, order.Total());
    }

    [TestMethod]
    [TestCategory("Domain")]
    [DataRow(5, 10F)]
    [DataRow(4, 20.3F)]
    [DataRow(9, 15.80F)]
    public void ShouldFailWhenTryAppliesAnInvalidDiscount(int quantityProduct, float discountAmount)
    {
        var discount = new Discount((decimal) discountAmount, DateTime.Now.AddDays(-5));
        var order = new Order(_customer, 0, discount);
        order.AddItem(_product, quantityProduct);
        var totalExpected = _product.Price * quantityProduct;
        Assert.AreEqual(totalExpected, order.Total());
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldSuccessReturningAnValidTotalOrderWithDeliveryFeeApplied()
    {
        var order = new Order(_customer, 10, _discountValidWithAmountZero);
        order.AddItem(_product, 1);
        var totalExpected = _product.Price * 1 + 10;
        Assert.AreEqual(totalExpected, order.Total());
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldFailWhenTryOrderWithoutCustomer()
    {
        var order = new Order(null, 0, _discountValidWithAmountZero);
        
        Assert.AreEqual(false, order.IsValid);
    }
}
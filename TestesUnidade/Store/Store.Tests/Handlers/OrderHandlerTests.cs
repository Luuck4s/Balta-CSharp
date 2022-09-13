using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Commands;
using Store.Domain.Handlers;
using Store.Domain.Repositories;
using Store.Tests.Repositories;

namespace Store.Tests.Handlers;

[TestClass]
public class OrderHandlerTests
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IDeliveryFeeRepository _deliveryFeeRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public OrderHandlerTests()
    {
        _customerRepository = new FakeCustomerRepository();
        _deliveryFeeRepository = new FakeDeliveryFeeRepository();
        _discountRepository = new FakeDiscountRepository();
        _orderRepository = new FakeOrderRepository();
        _productRepository = new FakeProductRepository();
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void ShouldFailCreateOrderWithInvalidCustomer()
    {
        var command = new CreateOrderCommand()
        {
            Customer = "",
            ZipCode = "11111111",
            PromoCode = "12345678",
        };
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        
        var orderHandler = new OrderHandler(
            _customerRepository,
            _deliveryFeeRepository,
            _discountRepository,
            _productRepository,
            _orderRepository
        );

        orderHandler.Handle(command);

        Assert.IsFalse(command.IsValid);
        Assert.IsFalse(orderHandler.IsValid);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void ShouldSuccessCreateOrderWithInvalidZipCode()
    {
        var command = new CreateOrderCommand()
        {
            Customer = "12345678911",
            ZipCode = "11111111",
            PromoCode = "12345678",
        };
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        
        var orderHandler = new OrderHandler(
            _customerRepository,
            _deliveryFeeRepository,
            _discountRepository,
            _productRepository,
            _orderRepository
        );

        orderHandler.Handle(command);

        Assert.IsTrue(command.IsValid);
        Assert.IsTrue(orderHandler.IsValid);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void ShouldSuccessCreateOrderWithInvalidDiscount()
    {
        var command = new CreateOrderCommand()
        {
            Customer = "12345678911",
            ZipCode = "123456789",
            PromoCode = "11111111",
        };
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        
        var orderHandler = new OrderHandler(
            _customerRepository,
            _deliveryFeeRepository,
            _discountRepository,
            _productRepository,
            _orderRepository
        );

        orderHandler.Handle(command);

        Assert.IsTrue(command.IsValid);
        Assert.IsTrue(orderHandler.IsValid);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void ShouldFailCreateOrderWithNonexistentOrderItems()
    {
        var command = new CreateOrderCommand()
        {
            Customer = "12345678911",
            ZipCode = "123456789",
            PromoCode = "12345678",
        };
        var orderHandler = new OrderHandler(
            _customerRepository,
            _deliveryFeeRepository,
            _discountRepository,
            _productRepository,
            _orderRepository
        );

        orderHandler.Handle(command);

        Assert.IsFalse(command.IsValid);
        Assert.IsFalse(orderHandler.IsValid);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void ShouldFailCreateOrderWithInvalidCommand()
    {
        var command = new CreateOrderCommand()
        {
            Customer = "",
            ZipCode = "123456789",
            PromoCode = "12345678",
        };
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        
        var orderHandler = new OrderHandler(
            _customerRepository,
            _deliveryFeeRepository,
            _discountRepository,
            _productRepository,
            _orderRepository
        );

        orderHandler.Handle(command);

        Assert.IsFalse(command.IsValid);
        Assert.IsFalse(orderHandler.IsValid);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void ShouldSuccessCreateOrderWithValidCommand()
    {
        var command = new CreateOrderCommand()
        {
            Customer = "12345678911",
            ZipCode = "123456789",
            PromoCode = "12345678",
        };
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        
        var orderHandler = new OrderHandler(
            _customerRepository,
            _deliveryFeeRepository,
            _discountRepository,
            _productRepository,
            _orderRepository
        );

        orderHandler.Handle(command);

        Assert.IsTrue(command.IsValid);
        Assert.IsTrue(orderHandler.IsValid);
    }
}
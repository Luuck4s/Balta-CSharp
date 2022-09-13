using Flunt.Notifications;
using Store.Domain.Commands;
using Store.Domain.Entities;
using Store.Domain.Repositories;
using Store.Domain.Utils;
using Store.Shared.Commands;
using Store.Shared.Handlers;

namespace Store.Domain.Handlers;

public class OrderHandler : 
    Notifiable<Notification>, 
    IHandler<CreateOrderCommand>
{

    private readonly ICustomerRepository _customerRepository;
    private readonly IDeliveryFeeRepository _deliveryFeeRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;

    public OrderHandler(ICustomerRepository customerRepository, IDeliveryFeeRepository deliveryFeeRepository, IDiscountRepository discountRepository, IProductRepository productRepository, IOrderRepository orderRepository)
    {
        _customerRepository = customerRepository;
        _deliveryFeeRepository = deliveryFeeRepository;
        _discountRepository = discountRepository;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
    }

    public ICommandResult Handle(CreateOrderCommand command)
    {
        command.Validate();
        if (!command.IsValid)
        {
            AddNotification("Command", "Invalid Command");
            return new GenericCommandResult(false, "Invalid order", command.Notifications);
        }

        var customer = _customerRepository.Get(command.Customer);

        var deliveryFee = _deliveryFeeRepository.Get(command.ZipCode);

        var discount = _discountRepository.Get(command.PromoCode);

        var products = _productRepository.Get(  ExtractGuids.Extract(command.Items).ToList());
        var order = new Order(customer, deliveryFee, discount);
        foreach (var item in command.Items)
        {
            var product = products.FirstOrDefault(x => x.Id == item.Product);
            order.AddItem(product, item.Quantity);
        }
        
        AddNotifications(order.Notifications);

        if (!IsValid)
        {
            AddNotification("Order", "Invalid Order");
            return new GenericCommandResult(false, "Error on generate order", order);
        }
        
        _orderRepository.Save(order);

        return new GenericCommandResult(true, $"Order {order.Number} success generate", order);
    }
}
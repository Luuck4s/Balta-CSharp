using Flunt.Validations;
using Store.Domain.Enums;
using Store.Shared.Entities;

namespace Store.Domain.Entities;

public class Order : Entity
{
    private List<OrderItem> _items;
    public IReadOnlyCollection<OrderItem> Items => _items.ToArray();
    public Customer Customer { get; private set; }
    public DateTime Date { get; private set; }
    public decimal DeliveryFee { get; private set; }
    public string Number { get; private set; }
    public Discount Discount { get; private set; }
    public EOrderStatus Status { get; private set; }

    public Order(Customer customer, decimal deliveryFee, Discount discount)
    {
        AddNotifications(
            new Contract<Order>()
                .Requires()
                .IsNotNull(customer, "Customer", "Customer is invalid")
        );

        Customer = customer;
        DeliveryFee = deliveryFee;
        Discount = discount;
        Date = DateTime.Now;
        Number = Guid.NewGuid().ToString().Substring(0, 8);
        _items = new List<OrderItem>();
        Status = EOrderStatus.WaitingPayment;
    }
    

    public void AddItem(Product product, int quantity)
    {
        var item = new OrderItem(product, quantity);
        if (item.IsValid)
        {
            _items.Add(item);
        }
    }

    public decimal Total()
    {
        var total = _items.Sum(item => item.Total());

        total += DeliveryFee;
        total -= Discount.Value();

        return total;
    }

    public void Pay(decimal amount)
    {
        if (amount == Total())
        {
            Status = EOrderStatus.WaitingDelivery;
        }
    }

    public void Cancel()
    {
        Status = EOrderStatus.Canceled;
    }
}
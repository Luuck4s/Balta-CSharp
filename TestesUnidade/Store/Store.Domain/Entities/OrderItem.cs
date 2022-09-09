using Flunt.Validations;
using Store.Shared.Entities;

namespace Store.Domain.Entities;

public class OrderItem : Entity
{
    public Product Product { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }

    public OrderItem(Product product, int quantity)
    {
        AddNotifications(
            new Contract<OrderItem>()
                .Requires()
                .IsNotNull(product, "Product", "Invalid Product")
                .IsGreaterThan(quantity, 0, "Quantity", "Quantity can't be zero"));

        Product = product;
        Price = product != null ? product.Price : 0;
        Quantity = quantity;
    }

    public decimal Total()
    {
        return Price * Quantity;
    }
}
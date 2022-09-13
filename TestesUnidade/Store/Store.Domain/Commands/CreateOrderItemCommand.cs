using Flunt.Notifications;
using Flunt.Validations;
using Store.Shared.Commands;

namespace Store.Domain.Commands;

public class CreateOrderItemCommand : Notifiable<Notification>, ICommand
{
    public Guid Product { get; set; }
    public int Quantity { get; set; }

    public CreateOrderItemCommand(Guid product, int quantity)
    {
        Product = product;
        Quantity = quantity;
    }

    public void Validate()
    {
        AddNotifications(
            new Contract<CreateOrderItemCommand>()
                .Requires()
                .IsNotNull(Product, "Product", "Product is invalid")
                .IsGreaterThan(Quantity, 0, "Quantity", "Quantity is invalid")
        );
    }
}
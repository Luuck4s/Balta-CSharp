using Flunt.Notifications;
using Flunt.Validations;
using Store.Shared.Commands;

namespace Store.Domain.Commands;

public class CreateOrderCommand: Notifiable<Notification>, ICommand
{
    public string Customer { get; set; }
    public string ZipCode { get; set; }
    public string PromoCode { get; set; }
    public IList<CreateOrderItemCommand> Items { get; set; }
    
    public CreateOrderCommand()
    {
        Items = new List<CreateOrderItemCommand>();
    }

    public CreateOrderCommand(string customer, string zipCode, string promoCode, IList<CreateOrderItemCommand> items)
    {
        Customer = customer;
        ZipCode = zipCode;
        PromoCode = promoCode;
        Items = items;
    }
    
    public void Validate()
    {
        AddNotifications(new Contract<CreateOrderCommand>()
            .Requires()
            .IsGreaterOrEqualsThan(Customer, 11, "Customer", "Invalid Client")
            .IsGreaterOrEqualsThan(ZipCode, 8, "ZipCode", "Invalid zipcode")
            .IsGreaterThan(Items.Count, 0, "Items", "Invalid Items")
        );
    }
}
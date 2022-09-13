using Store.Domain.Commands;

namespace Store.Domain.Utils;

public static class ExtractGuids
{
    public static IEnumerable<Guid> Extract(IEnumerable<CreateOrderItemCommand> items)
    {
        return items.Select(item => item.Product).ToList();
    } 
}
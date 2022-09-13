using System;
using System.Collections.Generic;
using Store.Domain.Entities;
using Store.Domain.Repositories;

namespace Store.Tests.Repositories;

public class FakeProductRepository: IProductRepository
{
    public IEnumerable<Product> Get(IEnumerable<Guid> ids)
    {
        IList<Product> products = new List<Product>()
        {
            new("Product 1", 10, true),
            new("Product 2", 20, true),
            new("Product 3", 30, true),
            new("Product 4", 10, false),
            new("Product 5", 30, true),
            new("Product 6", 50, true),
            new("Product 7", 40, false),
        };
        return products;
    }
}
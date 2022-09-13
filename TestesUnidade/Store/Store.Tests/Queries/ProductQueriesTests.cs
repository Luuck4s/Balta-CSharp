using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Entities;
using Store.Domain.Queries;

namespace Store.Tests.Queries;

[TestClass]
public class ProductQueriesTests
{
    private readonly IList<Product> _products;

    public ProductQueriesTests()
    {
        _products = new List<Product>()
        {
            new("Product 1", 10, true),
            new("Product 2", 20, true),
            new("Product 3", 30, true),
            new("Product 4", 10, false),
            new("Product 5", 40, false),
        };
    }
    
    [TestMethod]
    [TestCategory("Queries")]
    public void ShouldReturnThreeActiveProducts()
    {
        var result = _products.AsQueryable().Where(ProductQueries.GetActiveProducts());
        
        Assert.AreEqual(3,result.Count());
    }

    [TestMethod]
    [TestCategory("Queries")]
    public void ShouldReturnTwoInactiveProducts()
    {
        var result = _products.AsQueryable().Where(ProductQueries.GetInactiveProducts());
        
        Assert.AreEqual(2,result.Count());
    }
}
using System.Collections.Generic;

namespace DecoratorExample;

public class Product
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
}

public class Root
{
    public List<Product> Products { get; set; }
}

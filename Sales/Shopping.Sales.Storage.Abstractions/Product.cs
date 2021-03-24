using System.Collections.Generic;
using Shopping.Storage.Abstractions;

namespace Shopping.Sales.Storage.Abstractions
{
    public class Product
    {
        public long Id { get; set; }
        public long Name { get; set; }
    }
}
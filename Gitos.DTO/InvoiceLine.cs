using System;
using System.Runtime.Serialization;

namespace Gitos.DTO
{

    public class InvoiceLine
    {
        public string Description { get; set; }
        public int Units { get; set; }
        public decimal Price { get; set; }                                                                  
        public decimal Total { get { return Units * Price; } }
    }
}

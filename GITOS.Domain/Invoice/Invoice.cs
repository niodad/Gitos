using System;
using System.Collections.Generic;
using System.Text;

namespace GITOS.Domain
{
   public class Invoice
    {

        public Company Company { get; set; }
        public Company Client { get; set; }
        public DateTime InvoiceDate { get; set; }
        public List<InvoiceLine> InvoiceLines { get; set; }
        public string InvoiceNumber { get; set; }

    }

    public class Company : Base
    {

        public string Name { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public int NumberKBO { get; set; }
        public string Language { get; set; }

    }

    public class Base
    {
        public Guid GUID { get; set; }
    }

    public class InvoiceLine : IInvoiceLine
    {
        public string Description { get; set; }
        public int Units { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get { return Units * Price; } }
    }
}

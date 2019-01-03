using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Gitos.DTO
{

    public class Invoice
    {
        public Company Creditor { get; set; }
        public Company Debtor { get; set; }
        public DateTime InvoiceDate { get; set; }
        public List<InvoiceLine> InvoiceLines { get; set; }
        public string InvoiceNumber { get; set; }

    }
}

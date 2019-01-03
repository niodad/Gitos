using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Gitos.DTO;
using Gitos.BusinessRules;

namespace InvoiceService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "InvoiceService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select InvoiceService.svc or InvoiceService.svc.cs at the Solution Explorer and start debugging.
    public class InvoiceService : IInvoiceService
    {
        public byte[] CreateInvoicePdf(Invoice invoice, string lang)
        {
            return new InvoicesServer().WriteInvoiceToPdf(invoice,lang);
        }

 
    }
}

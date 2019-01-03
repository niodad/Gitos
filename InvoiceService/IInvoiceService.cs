using Gitos.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace InvoiceService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IInvoiceService" in both code and config file together.
    [ServiceContract]
    public interface IInvoiceService
    {
        [OperationContract]
        byte[] CreateInvoicePdf(Invoice invoice, string lang) ;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Gitos.Tools;

namespace Gitos.BusinessRules
{
    public class InvoicesServer
    {
        public byte[] WriteInvoiceToPdf(DTO.Invoice invoice, string language)
        {

            var currentCulture = System.Threading.Thread.CurrentThread.CurrentCulture;
            try
            {
                if (language=="fr")
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("fr-FR");
                }
            
             
                var templatePath = $@"{Gitos.DTO.FileSettings.Default.GitosTemplates}{invoice.Client.Language}\InvoiceTemplate.html";
                var htmlTemplate = File.ReadAllText(templatePath);
                var css = File.ReadAllText($@"{Gitos.DTO.FileSettings.Default.GitosTemplates}\Invoice.css");

                htmlTemplate = htmlTemplate.Replace("{logoPath}", $@"{Gitos.DTO.FileSettings.Default.GitosTemplates}\Images\Gitos_Logo.png");

               
                var pdfBytes = Gitos.Pdf.PdfTools.WriteHtmlToPdf(htmlTemplate, css);
             
                return pdfBytes;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = currentCulture;
            }

    
        }

        private string GetInvoicesTable(DTO.Invoice invoice)
        {
            var sb = new StringBuilder();

            foreach (var item in invoice.InvoiceLines)
            {
                sb.AppendLine($@"<tr>");
                sb.AppendLine($@"<td>{item.Description}</td>");
                sb.AppendLine($@"<td>{item.Units}</td>");
                sb.AppendLine($@"<td>{item.Price.ToEuroString()}</td>");
                sb.AppendLine($@"<td  style=""text-align:right"">{item.Total.ToEuroString()}</td>");
                sb.AppendLine($@"</tr>");
            }
            return sb.ToString();
        }
    }
}

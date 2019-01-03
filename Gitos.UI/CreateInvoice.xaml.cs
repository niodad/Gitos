using Gitos.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel;
using System.Text;
using System.Windows;
using System.Configuration;
using System.Threading.Tasks;

namespace Gitos.UI
{
    /// <summary>
    /// Interaction logic for CreateInvoice.xaml
    /// </summary>
    public partial class CreateInvoice : Window
    {
        private List<InvoiceLine> _invoiceLines = new List<InvoiceLine>();
        private List<Company> _companies;

        public CreateInvoice()
        {
            InitializeComponent();

  
                using (var client = new HttpClient())
                {
       
                HttpResponseMessage response =  client.GetAsync(@ConfigurationManager
                                            .AppSettings["gitosCompaniesUrl"]).Result;
                var s = response.Content.ReadAsStringAsync().Result;
              _companies =  JsonConvert.DeserializeObject<List<Company>>(s);
                }

                comboBoxCompanies.ItemsSource = _companies;
            }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _invoiceLines.Add(
                new DTO.InvoiceLine
                {
                    Description = textBoxDescription.Text,
                    Units = int.Parse(textBoxNumber.Text),
                    Price = decimal.Parse(textBoxAmount.Text)
                });
                gridInvoiceLines.ItemsSource = null;
                gridInvoiceLines.ItemsSource = _invoiceLines;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void CreateInvoicePdf(object sender, RoutedEventArgs e)
        {
            try
            {

              Invoice invoice = PrepareInvoice();

              await  CreateInvoicePdf(invoice);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async static Task CreateInvoicePdf(Invoice invoice)
        {
            var pdfPath =
                $@"{ConfigurationManager.AppSettings["pathInvoices"]}{invoice.InvoiceDate.Year}\Factuur{invoice.InvoiceNumber}-GITOS.pdf";

          
            using (var client = new HttpClient())
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                HttpResponseMessage invoicePdfResult = await client.PostAsync($@"{ConfigurationManager.AppSettings["createInvoiceUrl"]}", 
                                                    new StringContent(JsonConvert.SerializeObject(invoice), 
                                                                Encoding.Default, 
                                                                "application/json"));
     
                File.WriteAllBytes(pdfPath, invoicePdfResult.Content.ReadAsByteArrayAsync().Result);

                Process.Start(pdfPath);
            }
        }

        private Invoice PrepareInvoice()
        {
            var invoice = new Invoice();

            int NumberOffInvoices = GetNumberInvoicesForYear(invoice);

            invoice.Debtor = (Company)comboBoxCompanies.SelectedItem;

            invoice.InvoiceNumber = $"{invoice.InvoiceDate.Year}{(NumberOffInvoices + 1).ToString("D3")}";

            invoice.Creditor = _companies.FirstOrDefault(c => c.Id == new Guid("936ba57b-b845-47b8-bc92-bdcf13b1a718"));
     

            invoice.InvoiceLines = _invoiceLines;

            return invoice;
        }

        private int GetNumberInvoicesForYear(Invoice invoice)
        {
            invoice.InvoiceDate = DatePickerInvoice.SelectedDate.Value;

            var InvoicesDirectoryPath = 
                $@"{ConfigurationManager.AppSettings["pathInvoices"]}{invoice.InvoiceDate.Year}";

            if (!Directory.Exists(InvoicesDirectoryPath))
            {
                Directory.CreateDirectory(InvoicesDirectoryPath);
            }

            return Directory.GetFiles(InvoicesDirectoryPath)
                            .Count();
}
    }
}
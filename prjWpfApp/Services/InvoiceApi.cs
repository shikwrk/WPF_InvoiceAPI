using prjWpfApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace prjWpfApp.Services
{
    public class InvoiceApi
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<List<InvoiceDTO>> GetInvoiceDataAsync(string selectedCity, int limit)
        {

                limit = Math.Min(limit, 100); 
                string apiUrl = $"https://dataset.einvoice.nat.gov.tw/ods/portal/api/v1/ConsumpInvoiceStatic?hsnNm={Uri.EscapeDataString(selectedCity)}&limit={limit}";
            try
            {
                var response = await _httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var invoiceData = JsonConvert.DeserializeObject<List<InvoiceDTO>>(responseBody);
                return invoiceData ?? new List<InvoiceDTO>();
            }
            catch (Exception ex)
            {
                return new List<InvoiceDTO>();
            }
        }
    }
}

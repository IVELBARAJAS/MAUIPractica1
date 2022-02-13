using appLoginMAUI.Interfaces;
using appLoginMAUI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace appLoginMAUI.Services
{
    public class CreditService : ICredit
    {
        System.Net.Http.HttpClient client;
        string WebAPIUrl = string.Empty;

        public ObservableCollection<Credit> Items
        {
            get; private set;
        }


        public CreditService()
        {
            client = new System.Net.Http.HttpClient();
        }

        public async Task<ObservableCollection<Credit>> CheckForCredit(string token)
        {
            await Task.Delay(1000);
            WebAPIUrl = "https://serviciorestismael.azurewebsites.net/credit";

            var uri = new Uri(WebAPIUrl);
            try
            {
                HttpContent _content = new StringContent(JsonConvert.SerializeObject(token));
                _content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                var response = await client.GetAsync(uri);
                //var response = await client.GetAsync(uri,);

                if (response.IsSuccessStatusCode)
                {
              
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<ObservableCollection<Credit>> (content);
                    return Items;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
            return null;

        }

    }
}

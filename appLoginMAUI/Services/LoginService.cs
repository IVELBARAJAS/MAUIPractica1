using appLoginMAUI.Interfaces;
using appLoginMAUI.Models;
using LoginMAUIBlazor.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace appLoginMAUI.Services
{
    public class LoginService : ILogin
    {

        System.Net.Http.HttpClient client;
        string WebAPIUrl = string.Empty;

        public LoginService()
        {
            client = new System.Net.Http.HttpClient();
        }

        public async Task<Login> Authenticate(UserMin usermin)
        {
            await Task.Delay(100);
            usermin.Password = Functions.GetSHA256(usermin.Password).ToUpper();
            WebAPIUrl = "http://189.254.239.133/LoginAppApi/api/login/autenticar";

            var uri = new Uri(WebAPIUrl);
            try
            {
                HttpContent _content = new StringContent(JsonConvert.SerializeObject(usermin));
                _content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                var response = await client.PostAsync(uri, _content);

                if (response.IsSuccessStatusCode)
                {
                    appLoginMAUI.Models.Login login = new appLoginMAUI.Models.Login();
                    var content = await response.Content.ReadAsStringAsync();
                    login = JsonConvert.DeserializeObject<Login>(content);
                    return login;
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

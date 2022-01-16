using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Text;


namespace Servicios.ClientHttp
{
    public class ClienteHttp
    {
        private readonly IConfiguration _configuration;

        public HttpClient client;

        public ClienteHttp(IConfiguration configuration)
        {
            _configuration = configuration;

            string url = _configuration["ClienteApi:Url"];

            client = new HttpClient()
            {
                BaseAddress = new Uri(url)
            };                      

        }
    }

}

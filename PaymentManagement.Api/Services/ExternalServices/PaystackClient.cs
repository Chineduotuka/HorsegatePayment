using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PaymentManagement.Api.Services.Dtos;

namespace PaymentManagement.Api.Services.ExternalServices
{
    public class PaystackClient
    {
        private readonly string _secretKey;
        private readonly HttpClient _httpClient;

        public PaystackClient(string secretKey)
        {
            _secretKey = secretKey;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://api.paystack.co/");        }

        public async Task<PaystackApiResponse<T>> PostAsync<T>(string endpoint, IDictionary<string, string> data)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _secretKey);
            request.Content = new FormUrlEncodedContent(data);
            var response = await _httpClient.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<PaystackApiResponse<T>>(responseContent);

            return responseObject;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PaymentManagement.Api.Domain;
using PaymentManagement.Api.Repository.Interface;
using PaymentManagement.Api.Services.Dtos;
using PaymentManagement.Api.Services.ExternalServices;
using PaymentManagement.Api.Services.Interface;
using PayStack.Net;
using RestSharp;

namespace PaymentManagement.Api.Services.Implementation
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IPaymentRepository _paymentRepository;
        private readonly ITransactionRepository _transactionRepository;

        public PaymentService(IConfiguration configuration, IMapper mapper, IPaymentRepository paymentRepository, ITransactionRepository transactionRepository)
        {
            _configuration = configuration;
            _mapper = mapper;
            _paymentRepository = paymentRepository;
            _transactionRepository = transactionRepository;
        }
        public async Task<string> CreatePaymentAsync(string userId, PaymentDto paymentDto)
        {
            var client = new PaystackClient(_configuration["Payment:PaystackSK"]);
            var data = new Dictionary<string, string>
            {
                { "amount", (Convert.ToInt32(paymentDto.Amount) * 100).ToString() },
                { "email", paymentDto.Email },
                { "callback_url", paymentDto.CallBackUrl }
            };
            var response = await client.PostAsync<dynamic>("transaction/initialize", data);
            if (!response.Status)
            {
                throw new Exception($"Failed to create payment: {response.Message}");
            }
            var responseUrl = response.Data["authorization_url"];
            var responseReference = response.Data["reference"];
            var payment = _mapper.Map<Payment>(paymentDto);
            payment.CreatedAt = DateTimeOffset.UtcNow;
            await _paymentRepository.InsertAsync(payment);
            
            var transaction = new Transaction {
                PaymentReference = responseReference,
                UserId = userId,
                CreatedAt = DateTimeOffset.Now,
                ExpiredAt = DateTimeOffset.UtcNow.AddDays(30)
            };
            await _transactionRepository.InsertAsync(transaction);
            return responseUrl + " " + responseReference;
        }

        public async Task<string> VerifyPaymentAsync(string userId)
        {
            var userTransaction = _transactionRepository.GetAll().Where(e => e.UserId == userId).LastOrDefault();
            var reference = userTransaction.PaymentReference;
            var client = new RestClient("https://api.paystack.co/");
            var request = new RestRequest($"transaction/verify/{reference}", Method.Get);
            request.AddHeader("Authorization", "Bearer " + "sk_test_80bcfa9030d3af2b0891128fd429a736833b2b63");

            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                // Payment verification successful
                var responseBody = response.Content; // Response body in JSON format
                dynamic json = JsonConvert.DeserializeObject(responseBody);
                string result = json.data.status;
                return result;                                   // Parse the JSON response to get the payment details
            }
            else
            {
                return "Payment verification failed";
            }
        }

        // public async Task<string> GetPaymentByUserId(string reference){
        //     var user = 
        // }
    }
}
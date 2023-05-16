using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PaymentManagement.Api.Services.Dtos;
using PaymentManagement.Api.Services.Implementation;
using PaymentManagement.Api.Services.Interface;

namespace PaymentManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService){
            _paymentService = paymentService;
        }

        [HttpPost("make-payment")]        
        public async Task<IActionResult> CreatePayment([FromBody]PaymentDto paymentDto){

            var userId = "64edf6a8-6eb2-4523-b2c2-4c1c95574dbc";
            var result = await _paymentService.CreatePaymentAsync(userId, paymentDto);
            return Ok(result);
        }

        [HttpGet("Verify-Payment")]        
        public async Task<IActionResult> VerifyPayment([FromQuery]string userId)
        {
            var result = await _paymentService.VerifyPaymentAsync(userId);
            return Ok(result);
        }

        
    }
}
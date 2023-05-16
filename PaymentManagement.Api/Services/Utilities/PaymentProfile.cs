using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PaymentManagement.Api.Domain;
using PaymentManagement.Api.Services.Dtos;

namespace PaymentManagement.Api.Services.Utilities
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile(){
            CreateMap<Payment, PaymentDto>().ReverseMap();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using API = Raisins.Api.Models;
using B = Raisins.Beneficiaries.Models;
using P = Raisins.Payments.Models;
using A = Raisins.Accounts.Models;
using R = Raisins.Roles.Models;

namespace Raisins.Api.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<API.Beneficiary, B.Beneficiary>();
            Mapper.CreateMap<B.Beneficiary, API.Beneficiary>();

            Mapper.CreateMap<API.Payment, P.Payment>()
                .ForMember(s => s.Beneficiary, c => c.MapFrom(m => m.Beneficiary))
                .ForMember(s => s.Currency, c => c.MapFrom(m => m.Currency))
                .ForMember(s => s.Source, c => c.MapFrom(m => m.Source))
                .ForMember(s => s.Type, c => c.MapFrom(m => m.Type));
            Mapper.CreateMap<P.Payment, API.Payment>()
                .ForMember(s => s.Beneficiary, c => c.MapFrom(m => m.Beneficiary))
                .ForMember(s => s.Currency, c => c.MapFrom(m => m.Currency))
                .ForMember(s => s.Source, c => c.MapFrom(m => m.Source))
                .ForMember(s => s.Type, c => c.MapFrom(m => m.Type));

            Mapper.CreateMap<API.Currency, P.Currency>();
            Mapper.CreateMap<P.Currency, API.Currency>();

            Mapper.CreateMap<API.Beneficiary, P.Beneficiary>();
            Mapper.CreateMap<P.Beneficiary, API.Beneficiary>();

            Mapper.CreateMap<API.PaymentSource, P.PaymentSource>();
            Mapper.CreateMap<P.PaymentSource, API.PaymentSource>();

            Mapper.CreateMap<API.PaymentType, P.PaymentType>();
            Mapper.CreateMap<P.PaymentType, API.PaymentType>();

            Mapper.CreateMap<API.Beneficiary, A.Beneficiary>();
            Mapper.CreateMap<A.Beneficiary, API.Beneficiary>();


            Mapper.CreateMap<API.Account, P.Account>();
            Mapper.CreateMap<P.Account, API.Account>();

            Mapper.CreateMap<API.Account, A.Account>()
            .ForMember(s => s.Role, c => c.MapFrom(m => m.Role));
            Mapper.CreateMap<A.Account, API.Account>()
                .ForMember(s => s.Role, c => c.MapFrom(m => m.Role));

            Mapper.CreateMap<API.AccountProfile, A.AccountProfile>()
                .ForMember(s => s.Beneficiaries, c => c.MapFrom(m => m.Beneficiaries));
            Mapper.CreateMap<A.AccountProfile, API.AccountProfile>()
                .ForMember(s => s.Beneficiaries, c => c.MapFrom(m => m.Beneficiaries));

            Mapper.CreateMap<API.Role, A.Role>();
            Mapper.CreateMap<A.Role, API.Role>();

            Mapper.CreateMap<API.Role, R.Role>();
            Mapper.CreateMap<R.Role, API.Role>();
        }
    }
}
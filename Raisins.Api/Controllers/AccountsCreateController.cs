using Raisins.Accounts.Interfaces;
using Raisins.Accounts.Services;
using API = Raisins.Api.Models;
using Raisins.Data.Repository;
using Raisins.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ACCOUNTS = Raisins.Accounts.Models;
using AutoMapper;
using System.Diagnostics;
using Raisins.Accounts;

namespace Raisins.Api.Controllers
{
    public class AccountsCreateController : ApiController
    {
        private readonly IAccountService _service;
        protected IAccountService Service { get { return _service; } }
        public AccountsCreateController() : this( new RestrictAccountService(
            new AccountService(
                new CryptProvider("testing"), 
                new DateProvider(),
                new RestrictAccountRepository(
                    new AccountRepository()))))
        {
        }

        public AccountsCreateController(IAccountService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("AccountsController:service");
            }
            _service = service;
        }

        [HttpGet]
        public ACCOUNTS.Account Get(string userName)
        {
            return Service.Get(userName);
        }

        [HttpPost]
        public HttpResponseMessage Create([FromBody] API.AccountComplete accountComplete)
        {          
            var account = Mapper.Map<API.Account, ACCOUNTS.Account>(accountComplete.Account);
            var profile = Mapper.Map<API.AccountProfile, ACCOUNTS.AccountProfile>(accountComplete.Profile);

            try
            {
                Service.Create(account, profile);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch(InvalidUserException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            
        }

        [HttpPut]
        public HttpResponseMessage Edit([FromBody] API.AccountComplete accountComplete)
        {
            var account = Mapper.Map<API.Account, ACCOUNTS.Account>(accountComplete.Account);
            var profile = Mapper.Map<API.AccountProfile, ACCOUNTS.AccountProfile>(accountComplete.Profile);
            Service.Edit(account, profile);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
   
}

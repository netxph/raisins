using Raisins;
using Raisins.Accounts.Interfaces;
using Raisins.Accounts.Models;
using Raisins.Accounts.Services;
using Raisins.Data;
using Raisins.Data.Repository;
using Raisins.Kernel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Raisins.Api.Controllers
{
    public class AccountsController : ApiController
    {
        private readonly IAccountService _service;
        protected IAccountService Service { get { return _service; } }

        public AccountsController() : this (new AccountService(new CryptProvider("testing"), new DateProvider(), new AccountRepository()))
        {
        }

        public AccountsController(IAccountService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("AccountsController:service");
            }
            _service = service;
        }

        [HttpPost]
        public HttpResponseMessage Login(
            [FromBody]
            Models.Account account)
        {
            var token = Service.Authenticate(account.UserName, account.Password);
            if (string.IsNullOrEmpty(token))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("X-Session-Token", token);

            return response;
        }

        [HttpGet]
        public Token Validate(string encrypted)
        {
            var token = Service.Validate(encrypted);

            return token; 
        }
    }
}

using Raisins.Accounts.Interfaces;
using D = Raisins.Accounts.Models;
using Raisins.Accounts.Services;
using Raisins.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Raisins.Data.Repository;

namespace Raisins.Api.Controllers
{
    public class AccountsAllController : ApiController
    {
        private readonly IAccountService _service;
        protected IAccountService Service { get { return _service; } }
        public AccountsAllController() : this(new AccountService(
            new CryptProvider("testing"),
                new DateProvider(),
                    new RestrictAccountRepository(
                        new AccountRepository())))
        {
        }
        public AccountsAllController(IAccountService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("AccountsAllController:service");
            }
            _service = service;
        }
        [HttpGet]
        public D.Accounts GetAll()
        {         
            return Service.GetAll();
        }
    }
}

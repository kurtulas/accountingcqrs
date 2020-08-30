using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Accounting.Domain.Application.CommandServices;
using Accounting.Domain.Application.QueryServices;
using Accounting.Domain.Business.Accounts.Commands;
using Accounting.Domain.Business.Customers;
using Accounting.Service.ViewModels;
using AutoMapper;
using EventFlow;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Accounting.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountQueryService _accountQueryService;
        private readonly IAccountCommandService _accountCommandService;
        private readonly IMapper _mapper;

        public AccountsController(IMapper mapper,
            IAccountQueryService accountQueryService, IAccountCommandService accountCommandService)
        {
            _mapper = mapper;
            _accountQueryService = accountQueryService;
            _accountCommandService = accountCommandService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(AccountViewModel accountViewModel,
           CancellationToken cancellationToken)
        {
            await _accountCommandService.CreateAccountAsync(accountViewModel.CustomerId,
                accountViewModel.Name, accountViewModel.InitialCredit, cancellationToken);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAccounts(CancellationToken cancellationToken)
        {
            var result = await _accountQueryService.GetAccountsAsync(cancellationToken);
            var dto = result.Select(x => new {
                Id = x.Id.Value,
                CustomerId = x.CustomerId,
                Name = x.Name,
                Balance = x.Balance
            });
            return new JsonResult(dto);
        }
        [HttpGet]
        [Route("CustomerId")]
        public async Task<IActionResult> GetAccountsByCustomerId(string customerId, CancellationToken cancellationToken)
        {            
            var result = await _accountQueryService.GetAccountsByCustomerIdAsync(customerId,cancellationToken);
            var dto = result.Select(x=> new { 
              Id = x.Id.Value,
              CustomerId = x.CustomerId,
              Name = x.Name,
              Balance = x.Balance
            });
            return new JsonResult(dto);
        }


    }
}

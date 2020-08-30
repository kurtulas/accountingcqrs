using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Accounting.Domain.Application.CommandServices;
using Accounting.Domain.Application.QueryServices;
using AutoMapper;
using EventFlow;
using Microsoft.AspNetCore.Mvc;
using Transaction.Service.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Transaction.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionQueryService _transactionQueryService;
        private readonly ITransactionCommandService _transactionCommandService;
        private readonly IMapper _mapper;
 
        public TransactionController(ITransactionCommandService transactionCommandService,
            ITransactionQueryService transactionQueryService)
        {
            _transactionCommandService = transactionCommandService;
            _transactionQueryService = transactionQueryService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction(TransactionViewModel transactionViewModel,
         CancellationToken cancellationToken)
        {
            await _transactionCommandService.CreateTransactionAsync
                (transactionViewModel.CustomerId, transactionViewModel.AccountId, transactionViewModel.Amount, cancellationToken);
            return Ok();
        }


        // GET: api/<TransactionController>
        [HttpGet]
        public async Task<IActionResult> GetTransactions(CancellationToken cancellationToken)
        {
            var result = await _transactionQueryService.GetTransactionsAsync(cancellationToken);
            return new JsonResult(result);
        }

        [HttpGet]
        [Route("AccountId")]
        public async Task<IActionResult> GetTransactionsByAccountId(string accountId, CancellationToken cancellationToken)
        {
            var result = await _transactionQueryService.GetTransactionsByAccountIdAsync(accountId, cancellationToken);
            var dto = result.Select(x => new
            { 
                CustomerId = x.CustomerId,
                AccountId = x.AccountId,
                Amount = x.Amount ,
                Id = x.Id.Value
            }
            );
            return new JsonResult(dto);
        }

    }
}

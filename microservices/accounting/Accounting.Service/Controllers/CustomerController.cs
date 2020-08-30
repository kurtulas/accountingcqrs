using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Accounting.Domain.Application.QueryServices;
using EventFlow;
using Accounting.Service.ViewModels;
using System.Threading;
using Accounting.Domain.Business.Customers.Commands;
using Accounting.Domain.Application.CommandServices;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Accounting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {        
        private readonly ICustomerQueryService _customerQueryService;        
        private readonly ICustomerCommandService _customerCommandService;
        private readonly IMapper _mapper;

        public CustomerController(IMapper mapper ,
            ICustomerQueryService customerQueryService,
            ICustomerCommandService customerCommandService)
        {
            _mapper = mapper;
            _customerQueryService = customerQueryService;
            _customerCommandService = customerCommandService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerViewModel customerViewModel,
            CancellationToken cancellationToken)        
        {
            await _customerCommandService.CreateCustomerAsync
                (customerViewModel.Name, customerViewModel.Surname, customerViewModel.InitialCredit, cancellationToken);
            return Ok();
        }

        [HttpGet]        
        public async Task<IActionResult> GetCustomers(CancellationToken cancellationToken)
        {
            var result = await _customerQueryService.GetCustomersAsync(cancellationToken);            
            var dto = result.Select(x=> new 
            {
                Name = x.Name,
                Surname = x.Surname,
                Id = x.Id.Value
            }).ToList();

            return new JsonResult(dto);
        }

    }
}

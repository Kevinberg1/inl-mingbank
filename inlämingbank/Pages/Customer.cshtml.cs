using inlämingbank.Pages;
using inlämningbank.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AutoMapper;
using inlämingbank.BankAppData;
using inlämingbank.Services;
using inlämningbank.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace inlämningbank.Pages
{
    [Authorize(Roles = "Cashier")]
    public class CustomerModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        public CustomerModel(ICustomerService customerService, IMapper mapper, IAccountService accountService)
        {
            _customerService = customerService;
            _accountService = accountService;
            _mapper = mapper;
        }

        public CustomersViewModel Customer { get; set; }
        public List<AccountsViewModel>  Accounts { get; set; }

        public void OnGet(int customerId)
        {
           var customer = _customerService.GetCustomer(customerId);
           Customer = _mapper.Map<CustomersViewModel>(customer);

            var accounts = _accountService.GetAccounts(customerId);
            Accounts = _mapper.Map<List<AccountsViewModel>>(accounts);

        }
    }
}

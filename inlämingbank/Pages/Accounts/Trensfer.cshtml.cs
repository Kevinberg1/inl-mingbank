using AutoMapper;
using inlämingbank.BankAppData;
using inlämingbank.Pages;
using inlämingbank.Services;
using inlämningbank.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace inlämningbank.Pages.Accounts
{
    public class TrensferModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        public TrensferModel(IAccountService accountService, IMapper mapper, ICustomerService customerService)
        {
            _accountService = accountService;
            _customerService = customerService;
            _mapper = mapper;
        }
        public List<CustomersViewModel> Customers { get; set; }

        public class ToAccountViewModel
        {
            public int ToaccountId { get; set; }

            public string ToGivenname { get; set; }
            
            public string ToLastname { get; set; }
        }
        
        public IEnumerable<ToAccountViewModel> ToAccounts { get; set; }

            public string Q { get; set; }



        [BindProperty]
        public int AccountId { get; set; }

        [BindProperty]
        public int ToAccountId { get; set; } //dit pengarna kommer

        [BindProperty]
        [Range(100, 10000)]
        public decimal Amount { get; set; }

        [BindProperty]
        [Required(ErrorMessage =
            "You forgot to write a comment!")]
        [MinLength(5, ErrorMessage =
            "Comments must be at least 5 characters long")]
        [MaxLength(250, ErrorMessage =
            "OK, thats just too many words")]
        public string Comment { get; set; }

        public void OnGet(int accountId, string q)
        {
            AccountId = accountId;

        }

        public void OnPostSearch(int accountId, string q)
        {
            Q = q;
            AccountId = accountId;
            var AccountsAndCustomers = _customerService.GetAccountForCustomers(q);

            ToAccounts = AccountsAndCustomers.Select(s => new ToAccountViewModel
            {
                ToaccountId = s.AccountId,
                ToGivenname = s.Customer.Givenname,
                ToLastname = s.Customer.Surname,
            });
        }
        public IActionResult OnPostTransfer(int accountId)
        {

            if (ModelState.IsValid)
            {
                var account = _accountService.GetAccount(accountId);
                account.Balance -= Amount;

                _accountService.NewTransfer(accountId, ToAccountId, Amount);


                _accountService.Update(account);
                return RedirectToPage("/Account", new { accountId });

            }

            return Page();
        }
    }
}

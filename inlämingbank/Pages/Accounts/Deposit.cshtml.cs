using inlämingbank.BankAppData;
using inlämingbank.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using inlämningbank.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace inlämningbank.Pages.Accounts
{
    
    [Authorize(Roles = "Cashier")]
    public class DepositModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public DepositModel(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }
        [BindProperty]
        public int AccountId { get; set; }

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


        public void OnGet(int accountId)
        {
           AccountId = _accountService.GetAccount(accountId).AccountId;
        }

        public IActionResult OnPostDeposit(int accountId)
        {
            
            if (ModelState.IsValid)
            {
                var account = _accountService.GetAccount(accountId);
                account.Balance += Amount;

                 _accountService.NewDeposit(accountId, Amount);


                _accountService.Update(account);
                return RedirectToPage("/Account", new {accountId});
            }
            return Page();
        }
    }
}

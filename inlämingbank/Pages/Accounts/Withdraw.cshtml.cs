using AutoMapper;
using inlämingbank.BankAppData;
using inlämingbank.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace inlämningbank.Pages.Accounts
{
    [Authorize(Roles = "Cashier")]
    public class WithdrawModel : PageModel
    {

        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public WithdrawModel(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [BindProperty]
        public int AccountId { get; set; }

        [BindProperty]
        [Range(100, 100000)]
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

        public IActionResult OnPostWithdraw(int accountId)
        {
            var account = _accountService.GetAccount(accountId);
            if (account.Balance < Amount)
            {
                ModelState.AddModelError(
                    "Amount", "You don't have that much money!");
                return Page();
            }


            if (ModelState.IsValid)
            {
                account.Balance -= Amount;

                _accountService.NewWithdraw(accountId, Amount);


                _accountService.Update(account);
                return RedirectToPage("/Account", new { accountId });
            }
            return Page();
        }

    }
}

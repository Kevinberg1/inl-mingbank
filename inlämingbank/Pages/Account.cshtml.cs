using AutoMapper;
using inlämingbank.BankAppData;
using inlämingbank.Pages;
using inlämingbank.Services;
using inlämningbank.Services;
using inlämningbank.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace inlämningbank.Pages
{
    public class AccountModel : PageModel
    {

        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        public AccountModel(IMapper mapper, IAccountService accountService)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        public List<TransationViewModel> Transactions { get; set; }

        public int AccountId { get; set; }
        public void OnGet(int accountId, int pageNo)
        {
            if (pageNo == 0)
                pageNo = 1;
            AccountId = accountId;

            var transactions = _accountService.GetTransactions(accountId, pageNo);
            Transactions = _mapper.Map<List<TransationViewModel>>(transactions.Results);


        }


        public IActionResult OnGetShowMore(int accountId, int pageNo)
        {
            var transactions = _accountService.GetTransactions(accountId, pageNo);
            Transactions = _mapper.Map<List<TransationViewModel>>(transactions.Results);


            return new JsonResult(new { Transactions });
        }

    }
}

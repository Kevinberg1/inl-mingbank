using inlämingbank.Services;
using inlämningbank.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace inlämingbank.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;

        private readonly IAccountService _accountService; //hämtar från service

        private readonly  ICustomerService _customerService;
		public IndexModel(ILogger<IndexModel> logger, IAccountService accountService, ICustomerService customerService)
		{
			_logger = logger;
			_accountService = accountService;
            _customerService = customerService;
        }

        public class TopCustomersViewModel
        {
			public int Customers { get; set; }

			public int Accounts { get; set; }

			public decimal Total { get; set; }

        }

        public TopCustomersViewModel TopCustomersSweden { get; set; } = new();
        public TopCustomersViewModel TopCustomersFinland { get; set; } = new();
		public TopCustomersViewModel TopCustomersNorway { get; set; } = new();
		public TopCustomersViewModel TopCustomersDenmark { get; set; } = new();



		public void OnGet()
        {
            TopCustomersDenmark.Accounts = _accountService.GetAllAccountFromCountryCount("DK");
            TopCustomersSweden.Accounts = _accountService.GetAllAccountFromCountryCount("SE");
            TopCustomersFinland.Accounts = _accountService.GetAllAccountFromCountryCount("FI");
            TopCustomersNorway.Accounts = _accountService.GetAllAccountFromCountryCount("NO");


            TopCustomersDenmark.Total = _accountService.GetAllAccountFromCountryCountTotal("DK");
            TopCustomersSweden.Total = _accountService.GetAllAccountFromCountryCountTotal("SE");
            TopCustomersFinland.Total = _accountService.GetAllAccountFromCountryCountTotal("FI");
            TopCustomersNorway.Total = _accountService.GetAllAccountFromCountryCountTotal("NO");

            TopCustomersDenmark.Customers = _customerService.GetAllCustomersFromCountryCount("DK");
            TopCustomersSweden.Customers = _customerService.GetAllCustomersFromCountryCount("SE");
            TopCustomersFinland.Customers = _customerService.GetAllCustomersFromCountryCount("FI");
            TopCustomersNorway.Customers = _customerService.GetAllCustomersFromCountryCount("NO");
        }
	}
}
using inlämingbank.BankAppData;
using inlämningbank.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace inlämingbank.Pages
{
    public class CustomersModel : PageModel
    {
        private readonly BankAppDataContext _dbContext;
        private readonly ICustomerService _customerService;

        public CustomersModel(BankAppDataContext dbContext, ICustomerService customerService)
        {
            _dbContext = dbContext;
            _customerService = customerService;

        }
        public class CustomersViewModel
        {
            public int CustomerId { get; set; }
            public string Givenname { get; set; }
            public string Surname { get; set; }

            public string NationalId { get; set; }
            public string Streetaddress { get; set; }

            public string City { get; set; }
        }

        public List<CustomersViewModel> Customers { get; set; }


        public void OnGet()
        {
            Customers = _customerService.GetAllCustomers();
        }
    }
}

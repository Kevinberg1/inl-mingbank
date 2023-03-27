using inlämingbank.BankAppData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace inlämingbank.Pages
{
    public class AccountsModel : PageModel
    {
        private readonly BankAppDataContext _dbContext;

        public AccountsModel(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
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
            = new List<CustomersViewModel>();


        public void OnGet()
        {
            Customers = _dbContext.Customers.Select(s => new CustomersViewModel
            {
                CustomerId = s.CustomerId,
                Givenname = s.Givenname,
                Surname = s.Surname,
                NationalId = s.NationalId,
                Streetaddress = s.Streetaddress,
                City = s.City
            }).ToList();

        }
    }
}

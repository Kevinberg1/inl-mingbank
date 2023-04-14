using inlämingbank.BankAppData;
using Microsoft.EntityFrameworkCore;
using static inlämingbank.Pages.CustomersModel;

namespace inlämningbank.Services
{
    public class CustomerService: ICustomerService
    {
        private readonly BankAppDataContext _dbContext;

        public CustomerService(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int GetAllCustomersFromCountryCount(string countryCode)
        {
            var accounts = _dbContext.Customers
                .Include(d => d.Dispositions)
                .SelectMany(a => a.Dispositions.Where(c => c.Customer.CountryCode == countryCode)).Count();
            return accounts;
        }

        public List <CustomersViewModel> GetAllCustomers()
        {
            
            return _dbContext.Customers.Select(s => new CustomersViewModel
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

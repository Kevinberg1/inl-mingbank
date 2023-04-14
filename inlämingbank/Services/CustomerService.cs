using inlämingbank.BankAppData;
using Microsoft.EntityFrameworkCore;
using static inlämingbank.Pages.CustomersModel;

namespace inlämningbank.Services
{
    public class CustomerService : ICustomerService
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

        public List<CustomersViewModel> GetAllCustomers( string sortColumn, string sortOrder, int page)
        {
            {
                var query = _dbContext.Customers
                    .Select(s => new CustomersViewModel

                    {
                        CustomerId = s.CustomerId,
                        Givenname = s.Givenname,
                        Surname = s.Surname,
                        NationalId = s.NationalId,
                        Streetaddress = s.Streetaddress,
                        City = s.City
                    });

                if (string.IsNullOrEmpty(sortOrder))
                    sortOrder = "asc";
                if (string.IsNullOrEmpty(sortColumn))
                    sortColumn = "Givenname";

                if (sortColumn == "Givenname")
                {
                    if (sortOrder == "desc")
                        query = query.OrderByDescending(p => p.Givenname);
                    else
                        query = query.OrderBy(p => p.Givenname);
                }

                if (sortColumn == "CustomerId")
                {
                    if (sortOrder == "desc")
                        query = query.OrderByDescending(p => p.CustomerId);
                    else
                        query = query.OrderBy(p => p.CustomerId);
                }

                if (sortColumn == "NationalId")
                {
                    if (sortOrder == "desc")
                        query = query.OrderByDescending(p => p.NationalId);
                    else
                        query = query.OrderBy(p => p.NationalId);
                }

                if (sortColumn == "City")
                {
                    if (sortOrder == "desc")
                        query = query.OrderByDescending(p => p.City);
                    else
                        query = query.OrderBy(p => p.City);
                }

                if (sortColumn == "Streetaddress")
                {
                    if (sortOrder == "desc")
                        query = query.OrderByDescending(p => p.Streetaddress);
                    else
                        query = query.OrderBy(p => p.Streetaddress);
                }

                return query.ToList();


                //return _dbContext.Customers.Select(s => new CustomersViewModel
                //{
                //    CustomerId = s.CustomerId,
                //    Givenname = s.Givenname,
                //    Surname = s.Surname,
                //    NationalId = s.NationalId,
                //    Streetaddress = s.Streetaddress,
                //    City = s.City
                //}).ToList();
            }
        }
    }
}

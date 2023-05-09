using inlämingbank.BankAppData;
using inlämingbank.Pages;
using inlämningbank.Infrastructure.Paging;
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

        public Customer GetCustomer(int customerId)
        {
            return _dbContext.Customers.First(a => a.CustomerId == customerId);

        }

        public PagedResult<CustomersViewModel> GetAllCustomers(string sortColumn, string sortOrder, int page, string q)
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

                if (!string.IsNullOrEmpty(q))
                {
                    query = query
                        .Where(p => p.Givenname.Contains(q) ||
                                    p.City.Contains(q)
                                    );
                }


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

                return query.GetPaged(page, 50);

            }
        }
        public List<Disposition> GetAccountForCustomers(string q)
        {
            {

                if (!string.IsNullOrEmpty(q))
                {
                    return _dbContext.Dispositions
                        .Include(c => c.Customer)
                        .Include(a => a.Account)
                        .Where(p => p.Customer.Givenname.Contains(q) ||
                                    p.Customer.Surname.Contains(q) ||
                                p.Account.AccountId.ToString().Contains(q)).ToList();
                }

                return new List<Disposition>();
            }
        }
    }
}

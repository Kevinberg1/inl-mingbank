using inlämingbank.BankAppData;
using inlämingbank.Pages;
using inlämningbank.Infrastructure.Paging;


namespace inlämningbank.Services
{
    public interface ICustomerService
    {
       int GetAllCustomersFromCountryCount(string countryCode);

       PagedResult<CustomersViewModel> GetAllCustomers(string sortColumn, string sortOrder, int page, string q);
       List<Disposition> GetAccountForCustomers(string q);
       Customer GetCustomer(int customerId);
    }
}

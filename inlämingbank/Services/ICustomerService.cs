using inlämingbank.BankAppData;
using inlämingbank.Pages;


namespace inlämningbank.Services
{
    public interface ICustomerService
    {
       int GetAllCustomersFromCountryCount(string countryCode);

       List<CustomersViewModel> GetAllCustomers(string sortColumn, string sortOrder, int page);

       Customer GetCustomer(int customerId);
    }
}

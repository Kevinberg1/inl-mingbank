using inlämingbank.Pages;


namespace inlämningbank.Services
{
    public interface ICustomerService
    {
       int GetAllCustomersFromCountryCount(string countryCode);

       List<CustomersModel.CustomersViewModel> GetAllCustomers(string sortColumn, string sortOrder, int page);
    }
}

using inlämingbank.BankAppData;
using inlämningbank.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace inlämingbank.Pages
{
    public partial class CustomersModel : PageModel
    {
        
        private readonly ICustomerService _customerService;

        public CustomersModel( ICustomerService customerService)
        {
            
            _customerService = customerService;

        }

        public List<CustomersViewModel> Customers { get; set; }

        public int Page { get; set; }

        public string SortColumn { get; set; }
        public string SortOrder { get; set; }

        public void OnGet(string sortColumn, string sortOrder, int page)
        {
            SortColumn = sortColumn;
            SortOrder = sortOrder;
            Page = page;

            Customers = _customerService.GetAllCustomers( sortColumn, sortOrder, page);
        }
    }
}

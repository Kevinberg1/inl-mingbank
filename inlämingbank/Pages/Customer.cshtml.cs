using inlämingbank.Pages;
using inlämningbank.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace inlämningbank.Pages
{
    public class CustomerModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public CustomerModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public CustomersViewModel Customer { get; set; }

        public void OnGet(int customerId)
        {
           Customer = _customerService.GetCustomer(customerId);


        }
    }
}

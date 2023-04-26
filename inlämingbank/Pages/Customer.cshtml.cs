using inlämingbank.Pages;
using inlämningbank.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AutoMapper;

namespace inlämningbank.Pages
{
    public class CustomerModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        public CustomerModel(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        public CustomersViewModel Customer { get; set; }

        public void OnGet(int customerId)
        {
           var customer = _customerService.GetCustomer(customerId);

           Customer = _mapper.Map<CustomersViewModel>(customer);


        }
    }
}

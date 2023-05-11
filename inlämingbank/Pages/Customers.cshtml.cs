using AutoMapper;
using inlämingbank.BankAppData;
using inlämningbank.Infrastructure.Paging;
using inlämningbank.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

namespace inlämingbank.Pages
{
    [Authorize(Roles = "Cashier")]
    public partial class CustomersModel : PageModel
    {
        
        private readonly ICustomerService _customerService;

        private readonly IMapper _mapper;

        public CustomersModel( ICustomerService customerService, IMapper mapper)
        {
            _mapper = mapper;
            _customerService = customerService;

        }

        public List<CustomersViewModel> Customers { get; set; }

        public int CurrentPage { get; set; }
        public string Q { get; set; }


        public string SortColumn { get; set; }
        public string SortOrder { get; set; }

        public void OnGet(string sortColumn, string sortOrder, int pageNo, string q)
        {

            if (pageNo == 0)
                pageNo = 1;
            CurrentPage = pageNo;
            Q = q;

            SortColumn = sortColumn;
            SortOrder = sortOrder;
            

            var customers = _customerService.GetAllCustomers( sortColumn, sortOrder, pageNo, Q);
            Customers = _mapper.Map<List<CustomersViewModel>>(customers.Results);
        }
    }
}

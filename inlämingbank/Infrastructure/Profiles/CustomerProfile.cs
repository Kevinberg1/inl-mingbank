using AutoMapper;
using inlämingbank.BankAppData;
using inlämingbank.Pages;

namespace inlämningbank.Infrastructure.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomersViewModel>().ReverseMap();
        }
    }
}

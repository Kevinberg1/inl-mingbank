using AutoMapper;
using inlämingbank.BankAppData;
using inlämingbank.Pages;
using inlämningbank.ViewModels;

namespace inlämningbank.Infrastructure.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomersViewModel>().ReverseMap();

            CreateMap<Account, AccountsViewModel>().ReverseMap();

            CreateMap<Disposition, AccountsViewModel>()
                .ForMember(dest=> dest.AccountId, opt => opt.MapFrom(src =>src.Account.AccountId))
                .ForMember(dest=> dest.Balance, opt => opt.MapFrom(src =>src.Account.Balance))
                .ForMember(dest=> dest.Created, opt => opt.MapFrom(src =>src.Account.Created))
                .ForMember(dest=> dest.Frequency, opt => opt.MapFrom(src =>src.Account.Frequency))
                .ReverseMap();

            CreateMap<Transaction, TransationViewModel>().ReverseMap();
        }
    }
}

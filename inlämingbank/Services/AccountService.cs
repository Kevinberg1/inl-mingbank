using inlämingbank.BankAppData;
using Microsoft.EntityFrameworkCore;

namespace inlämingbank.Services
{
    public class AccountService : IAccountService
    {
        private readonly BankAppDataContext _dbContext;
        public AccountService(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int GetAllAccountFromCountryCount(string countryCode)
        {
            var accounts = _dbContext.Accounts
                .Include(d => d.Dispositions)
                .ThenInclude(c => c.Customer)
                .SelectMany(a => a.Dispositions.Where(c => c.Customer.CountryCode == countryCode)).Count();
            return accounts;
        }
        public decimal GetAllAccountFromCountryCountTotal(string countryCode)
        {
            var accounts = _dbContext.Accounts
                .Include(d => d.Dispositions)
                .ThenInclude(c => c.Customer)
                .SelectMany(a => a.Dispositions
                    .Where(c => c.Customer.CountryCode == countryCode))
                .Select(c => c.Account.Balance)
                .Sum();
            return accounts;
        }
    }
}

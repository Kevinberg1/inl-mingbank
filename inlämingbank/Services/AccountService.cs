using inlämingbank.BankAppData;
using inlämningbank.Infrastructure.Paging;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

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

        public List<Account> GetAccounts(int customerId)
        {
            return _dbContext.Dispositions
                .Include(c => c.Customer)
                .Include(a => a.Account)
                .Where(c => c.CustomerId == customerId)
                .Select(a => a.Account).ToList();

        }

        public Account GetAccount(int accountId)
        {
            return _dbContext.Accounts.First(a => a.AccountId == accountId);
        }






        public void NewTransfer(int accountId,int ToAccountId, decimal amount)
        {
            var account = _dbContext.Accounts.First(a => a.AccountId == accountId);

            var toaccount = _dbContext.Accounts.First(t => t.AccountId == ToAccountId);
            
            if (amount > 100 || 10000 > amount)
            {
                toaccount.Balance += amount;

                _dbContext.Transactions.Add(new Transaction
                {
                    AccountId = accountId,
                    Date = DateTime.Now,
                    Type = "Debit",
                    Operation = "Debit in Cash",
                    Amount = -amount,
                    Balance = account.Balance,
                });

                _dbContext.Transactions.Add(new Transaction
                {
                    AccountId = toaccount.AccountId,
                    Date = DateTime.Now,
                    Type = "Credit",
                    Operation = "Credit in Cash",
                    Amount = amount,
                    Balance = account.Balance,


                });

                _dbContext.SaveChanges();
            }
        }




        public void NewWithdraw(int accountId, decimal amount)
        {
            var account = _dbContext.Accounts.First(a => a.AccountId == accountId);
            if (amount < account.Balance)
            {
                _dbContext.Transactions.Add(new Transaction
                {
                    AccountId = accountId,
                    Date = DateTime.Now,
                    Type = "Debit",
                    Operation = "Debit in Cash",
                    Amount = -amount,
                    Balance = account.Balance,
                    //glöm ej symbol
                });

                _dbContext.SaveChanges();
            }

        }

        public void NewDeposit(int accountId, decimal amount)
        {
            var account = _dbContext.Accounts.First(a => a.AccountId == accountId);
             
            if (amount < account.Balance)
            {
                _dbContext.Transactions.Add(new Transaction
                {
                    AccountId = accountId,
                    Date = DateTime.Now,
                    Type = "Credit",
                    Operation = "Credit in Cash",
                    Amount = amount,
                    Balance = account.Balance,
                });
            }
            _dbContext.SaveChanges();
        }

        public PagedResult<Transaction> GetTransactions(int accountId, int page)
        {
            var transactions = _dbContext.Accounts.Include(a => a.Transactions)
                .Where(a => a.AccountId == accountId)
                .SelectMany(t => t.Transactions).OrderByDescending(t => t.TransactionId).AsQueryable();

            return transactions.GetPaged(page, 20);
        }
        public void Update(Account account)
        {
            _dbContext.SaveChanges();
        }

    }
}

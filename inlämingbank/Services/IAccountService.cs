using inlämingbank.BankAppData;
using inlämningbank.Infrastructure.Paging;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace inlämingbank.Services
{
    public interface IAccountService
    {
        int GetAllAccountFromCountryCount(string countryCode);
        decimal GetAllAccountFromCountryCountTotal(string countryCode);
        List<Account> GetAccounts(int customerId);
        PagedResult<Transaction> GetTransactions(int accountId,int page);
        void NewDeposit(int accountId, decimal amount);
        void NewTransfer(int accountId, int toAccountId, decimal amount);
        void NewWithdraw(int accountId, decimal amount);
        public Account GetAccount(int accountId);
        void Update(Account account);

    }
}

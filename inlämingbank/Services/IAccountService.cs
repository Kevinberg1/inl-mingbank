namespace inlämingbank.Services
{
    public interface IAccountService
    {
        int GetAllAccountFromCountryCount(string countryCode);
        decimal GetAllAccountFromCountryCountTotal(string countryCode);
    }
}

using inlämingbank.BankAppData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace inlämingbank.Pages.Countris
{
    public class topCountrisModel : PageModel
    {
        private readonly BankAppDataContext _dbContext;

        public topCountrisModel(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }

      



        public class TopCustumersViewModel
        {
            public string Name { get; set; }

            public string? Email { get; set; }

            public int Balance { get; set; }

            public string Countrys { get; set; }

            public int Accounts { get; set; }


        }
        public List<TopCustumersViewModel> TopCustumer { get; set; }
            
        public string CountryCode { get; set; }


        
        //public List<TopCountrisViewModel> TopCountris { get; set; }
        //    = new List<TopCountrisViewModel>();


        public void OnGet(string countryCode)
        {
            CountryCode = countryCode;

            var country = _dbContext.Dispositions
                .Include(a => a.Account).Include(c => c.Customer).Where(c => c.Customer.CountryCode == countryCode)
                .Where(e => e.Type.ToLower() == "owner").OrderByDescending(c => c.Account.Balance).Take(10);

            //lägg ovan i ser´vice
            //Suppliers = _supplierService.GetSuppliers(sortColumn, sortOrder)
            //    .Select(s => new SupplierViewModel
            //    {
            //        Id = s.SupplierId,
            //        Name = s.CompanyName,
            //        City = s.City,
            //        Country = s.Country
            //    }).ToList();


            //TopCustumer = country.Select(s=> new TopCustumersViewModel(
            //{
            //    Name = s.Customer.Surname
            //}).ToList();
                
          
           






            //var countries = (from c in _dbContext.Customers
            //        join a in _dbContext.Accounts on c.CustomerId equals a.AccountId
            //        group a by c.Country into g
            //        select new
            //        {
            //            Country = g.Key,
            //            TotalAccounts = g.Count(),
            //            TotalBalance = g.Sum(a => a.Balance)
            //        })
            //    .OrderByDescending(c => c.TotalBalance)
            //    .ThenByDescending(c => c.TotalAccounts)
            //    .ToList();

            //foreach (var country in countries)
            //{
            //    TopCountris.Add(new topCountrisViewModel
            //    {
            //        Countrys = country.Country,
            //        Accounts = country.TotalAccounts,
            //        Balance = (int)country.TotalBalance
            //    });
            //}

        }
    }
}

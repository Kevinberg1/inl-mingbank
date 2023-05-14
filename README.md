Hello and welcome to my bank app, in this app you can manage transactions and transfers between several customers. you can log in as cashier or admin using ASP.NET Core Identity.

as an admin, you can create new users and choose the type of user it should be.

as a cashier, you can select a customer and see all the information about that particular person, you can also make transfers and withdrawals, deposits. when you are logged in as a cashier, you can also get a list where you get 50 customers at a time, there is also a search function that you can use. you can also sort the different rows depending on who you are looking for.

When you select a customer, you get a customer picture with information such as name, city, age and the most important thing in this app is their Account.
if you go further into the account, you will get to the transactions, where you can see the history of the account and also make new transactions.

I have used ASP.NET Core to create this web app, where I retrieve the list of information from a sql data base (data-first) throw EF core and then I have created a DBcontext that fits it.

web interface with ASP.NET Core Razor Pages, I have mostly done my own styling with CSS and html, have also used boostrap for some parts.

I have also fixed services that I use to bring things up in my view and I have made viewmodels so that I do not have any data entities in my view.

have also used Auto-mapper.

I have Seeded two users:
-------------------------------------
richard.chalk@systementor.se

pw: Hello123#

role: Admin
--------------------------------------
richard.erdos.chalk@gmail.se

pw: Hello123#

role: Cashier
-------------------------------------

I have deployed my project and database to Azure.


I have deployed my project and database to Azure: https://kevinbergbankapp.azurewebsites.net/
link to the sql database: https://aspcodeprod.blob.core.windows.net/school-dev/BankAppDatav2%20(1).bak

# MoneyMinder.Net

##### A financial web app that allows people to create multiple funds, unify data across those funds, categorize and track spending and savings, and set aside funds for specific purposes built with Visual Studio MVC, C#, and .NET.  05/17/18

## By Sara Hamilton

# Description
This is my capstone project for Epicodus.  Its purpose is to display understanding of Visual Studio MVC, ORM, Migrations, and Testing.  This is a replication of an app that I created in November of 2017.  It uses MVC architecture, user authentication and validation, object relational mapping in a database, one to many relationships, and functions involving dates and currency.  I chose to recreate it because it requires a thorough understanding of a language and framework in order to execute.  

## Functionality

### Models
  * User
	* UserId
    * Name
    * Password

  * Fund 
	* FundId
	* Name
	* Minimum
	* Goal
	* Total

  * Category
    * CategoryId
    * Name

  * Transaction 
	* TransactionId
	* Date
	* Description
	* Type (Deposit or Withdrawal)
	* Amount
	* Category
	* Fund
	* FromFund (for Transfers)
	* ToFund (for Transfers)

### Login
A user signs up to create a new account or logs in to an existing account.  Submitted info is verified for accuracy and checked against existing data in database.  Descriptive error messages appear on screen in case of errors.
Once all user info is verified, a user session is created. Navbar options expand allowing a user to view his/her funds, categories, and transactions.   

### Funds
A new user has a General fund and a Savings fund.  A user can create funds by selecting the Add Fund link.  The user can name each fund and has the option of setting a Minimum (a minimum amount to try to maintain in the fund) and/or a Goal (a desired amount to try to achieve).  A user can edit funds.  A fund name can be changed at any time.  A user may delete any fund that has not been transacted against.  When a user clicks on a fund name, the transaction history for that fund is displayed in a table.

### Categories
A new user has 15 default categories. New categories can be created.  Categories can be renamed at any time.  A user may delete any category that has not been transacted against.

### Transactions
A user must have at least one fund and at least one category before they can make a transaction.  All transactions must have a date, a fund, and a category. Including a description of the transaction is optional.  

### Transfer
A user may transfer any amount from any fund to another fund.  This is essentially a withdrawal from one fund and a transfer into another fund.  Assigning a date and category to the transaction is required.  Including a description of the transfer is optional.

### View Transactions by Account, Category, and Date Range
A user may view all transactions within a specified date range.  Sorting by fund and/or category is optional. A table will display all transactions within the specified parameters and show the total amount that was transacted.   

### Logout
User session is deleted and user is logged out.  The navbar options retract to only allow access to the main page and to login.  

## User Stories

### User Stories - basic behavior
* a user can
  * register for an account with a username and password
  * login to their account
  * create categories
  * create funds
  * edit categories and funds
  * delete categories and funds that have not been transacted against
  * make transactions tied to a specific fund and category
  * transfer money from one fund to another
  * filter transactions by date range 
  * logout

### Kim
Kim wants to save money.  The dividend that is payed out on her checking account through the local credit union is higher than the interest rates on any savings accounts or CDs she can find.  She decides to keep all of her money in her checking account and creates a fund in the MoneyMinder app called Savings to keep her savings and her spending money separate.

### Peter
Peter has three different checking accounts in three different banks.  He also has one savings account and two CDs that he wants to keep track of.  He creates a different fund in the MoneyMinder app for each of his accounts.  The app shows him the total amount of all of his separate accounts.

### John and Linda
John and Linda share a checking account.  They want to make sure that they have enough money to cover necessary expenses without overspending.  They create an fund called Home which they transfer $2500 to each month to cover all costs associated with their housing: mortgage, utilities, maintenance, etc.  They transfer $300 a month to an fund called Vacation.  When the Goal amount of $4000 is reached, they plan to go on a trip to Hawaii.  They each have their own fun money fund.  This is money that each of them can spend on whatever they want without having to check with each other.  $250 is transferred into each fun money fund each month.  All other money goes in a general fund to be spent on food, clothing, and life's other daily expenses.  

### Kevin
Kevin has paid off his mortgage and owns his own home.  He wants to make sure that he has enough money ready to pay his property tax and house insurance bills when they come around once a year.  He creates a fund called Escrow that he transfers $500 to each month.  This will prevent him from thinking that he has more money to spend than he actually does.

## Technologies Used
* HTML
* CSS
* Bootstrap
* Visual Studio
* C#
* .NET
* MySql
* MAMP

## Run the Application  

  * _Clone the github respository_
  ```
  $ git clone https://github.com/Sara-Hamilton/MoneyMinder.Net
  ```

  * _Install the .NET Framework and MAMP_

    .NET Core 1.1 SDK (Software Development Kit)

    .NET runtime.

    MAMP

    See https://www.learnhowtoprogram.com/c/getting-started-with-c/installing-c for instructions and links.

* _Start the Apache and MySql Servers in MAMP_

* _Move into the directory_
```
$ cd MoneyMinder.Net
```
*  _Restore the program_

 ```
 $ dotnet restore
 ```
* _Move one layer deeper into the directory_
```
$ cd MoneyMinder.Net
```
*  _Setup the database_

 ```
 $ dotnet ef database update --context MoneyDbContext
```
*  _Run the program_
```
$ dotnet run
```
## Testing

* _Move two layers into the directory_
```
$ cd MoneyMinder.Net/MoneyMinder.Net
```
*  _Setup the testing database_

 ```
 $ dotnet ef database update --context TestDbContext
```
* _Open project solution in Visual Studio_

*  _Run the tests_

### License

*MIT License*

Copyright (c) 2018 **_Sara Hamilton_**

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

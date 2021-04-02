# Stock Simulator
Stock buy and sell simulator

**Login Page** 
On the first page, we have the login screen, where the user can access the system. If he doesn't have an account, he can create a new one at the link below the button.
![Screenshot](https://github.com/marcelomcdev/stocksimulator/blob/master/readme-img/app-login.png)

**Register Page** 
On the page for creating a new account, the user must inform the data required to release the operation. Once done, a bank account with zero balance will be created for the user, where in the future he will be able to carry out operations within the application.
![Screenshot](https://github.com/marcelomcdev/stocksimulator/blob/master/readme-img/app-register.png)

**Shares** 
This screen shows the five most traded assets within the last 7 days. The user can click on the Negotiate button if he wants to see more details.
![Screenshot](https://github.com/marcelomcdev/stocksimulator/blob/master/readme-img/app-trade.png)

**Buying shares** 
This screen shows the details of the order to be carried out by the user, as well as performing its calculation. The process will check if there is enough balance in the account to make a share purchase or if the user will be able to sell any shares in the future. This screen is still under construction.
![Screenshot](https://github.com/marcelomcdev/stocksimulator/blob/master/readme-img/app-buy.png)

# Dependencies and tools installation
Before you start, using a command line software (like Windows Powershell as example), on the backend's folder, use the <code>dotnet restore</code> command to restore the existing dependencies and tools.

# How to run tests in this project
In the project folder, you can run in a cmd the command below:

<code>dotnet test</code>

For more information, access the url:

https://docs.microsoft.com/pt-br/dotnet/core/tools/dotnet-test

**Note** 

*You also run tests using Visual Studio, selecting the **Test** menu or open **Test Explorer** to run your tests.*




# TradeProfile
Trade Profile Application

Objective: Read in the trading profile in workding directory, and presenting the P/L based on the latest tickers' performance

The project comprises of 3 components:
##  Main Component 
Main Form (MainApp): Entry point of the application. The main app will hold the castle windsor IoC contaner, and perform register/resolve all the dependencies.

##  Data Model/Service Layer
Contains data model and services to be consumed by the reporting model.
Common: contains common data model to be used by service
StockTickerAlphaVantageService: ticker service
StockTransactionXmlService: read in user's transactions
ProfitLossService: calculate the P/L (to be consumed by ProfitLossPage)

##  Reporting Layer
Contains multiple Report page to be "injected" via IoC method.
SettingReport: Display the application setting
ProfitLossPage: Display the P/L data

##  Unit Test
Unit test project focussing on individual components


##  Steps to run:

1) Select File, Reload

![image](https://user-images.githubusercontent.com/977426/138586841-b7781117-dc4e-4622-8f91-a718e29e98f9.png)

2) Form will be loaded as follow
![image](https://user-images.githubusercontent.com/977426/138586877-080f3dd2-3a90-4b26-8187-6dbfac2a9dbe.png)



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



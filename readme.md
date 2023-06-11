# Point of sale
This exercise is part of the course "The World's Best Intro to TDD, Level 1" (https://www.jbrains.ca/training/course/worlds-best-intro-to-tdd/).

Developed with dotnet (c#), xUnit and Visual Studio.

## Practice objectives
- TDD
- Client-First Design
- Evolutionary Design
- DDD
- End to end development

## Brief explanation
JBrains ask us to develop a Point of Sale system for a store. The system should be able to:
- Display the price of a product

## How to run end to end manual testing
The solution includes a desktop app that contains a textarea that will display the output of the system: DesktopDisplayApp. This is the first app that you have to run, as it starts a NamedPipeServerStream that will wait for the console app to connect to.

Then you will have to run the console app PointOfSaleSystem, which takes the input from the Console.In and will eventually connect and call the NamedPipeServerStream served by DesktopDisplayApp.
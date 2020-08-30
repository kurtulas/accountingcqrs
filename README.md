
This is a sample application with Cqrs, microservices using Ocelot gateway and Blazor as ui.

Data is stored in memory for demonstration purposes and event sourcing part is not implemented yet.

Download the code and run in order

Accounting.Service
Transactions.Service
PublicGateway 
and 
BlazorUI app

For swagger definitions :

Public gateway : http://localhost:5000/swagger </br>
Accounting : http://localhost:5001/swagger </br>
Transactions : http://localhost:5002/swagger </br>

<h3>Architecture</h3>
<img src="img/architecture.png">



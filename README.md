
This is a sample application with Cqrs event sourcing, microservices using Ocelot gateway and Blazor as ui.

Data is stored in memory for demonstration purposes and event sourcing part is not implemented yet.


<h2> How to run? </h2>

Download the code and run in order

Accounting.Service </br>
Transactions.Service </br>
PublicGateway </br>
and </br>
BlazorUI app </br>
</br>
or 
</br>
docker-compose up docker-compose.yml</br>

<h2> For swagger definitions : </h2>

Public gateway : http://localhost:5000/swagger </br>
Accounting : http://localhost:5001/swagger </br>
Transactions : http://localhost:5002/swagger </br>

<h2>Architecture</h2>




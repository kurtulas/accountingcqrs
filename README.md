
This is a sample application with Cqrs event sourcing, microservices using Ocelot gateway and Blazor as ui.

Data is stored in memory for demonstration purposes and event store has not been implemented yet.


<h2> How to run? </h2>

Download the code and run in order

docker run --rm -it --hostname my-rabbit -p 15672:15672 -p 5672:5672 rabbitmq:3-management

dotnet run Accounting.Service </br>
dotnet run Transactions.Service </br>
dotnet run PublicGateway </br>
and </br>
dotnet run BlazorUI </br>
</br>
or 
</br>
docker-compose up docker-compose.yml</br>

<h2> For swagger definitions : </h2>

Public gateway : http://localhost:5000/swagger </br>
Accounting : http://localhost:5001/swagger </br>
Transactions : http://localhost:5002/swagger </br>

<h2>Architecture</h2>
<img src="img/architecture.png">



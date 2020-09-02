#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["microservices/transactions/Transaction.Service/Transaction.Service.csproj", "microservices/transactions/Transaction.Service/"]
COPY ["modules/accounting/src/Accounting.Domain/Accounting.Domain.csproj", "modules/accounting/src/Accounting.Domain/"]
COPY ["microservices/transactions/Transaction.ReadModel/Transaction.ReadModel.csproj", "microservices/transactions/Transaction.ReadModel/"]
RUN dotnet restore "microservices/transactions/Transaction.Service/Transaction.Service.csproj"
COPY . .
WORKDIR "/src/microservices/transactions/Transaction.Service"
RUN dotnet build "Transaction.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Transaction.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Transaction.Service.dll"]
#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["microservices/accounting/Accounting.Service/Accounting.Service.csproj", "microservices/accounting/Accounting.Service/"]
COPY ["modules/accounting/src/Accounting.Domain/Accounting.Domain.csproj", "modules/accounting/src/Accounting.Domain/"]
COPY ["microservices/accounting/Accounting.ReadModel/Accounting.ReadModel.csproj", "microservices/accounting/Accounting.ReadModel/"]
RUN dotnet restore "microservices/accounting/Accounting.Service/Accounting.Service.csproj"
COPY . .
WORKDIR "/src/microservices/accounting/Accounting.Service"
RUN dotnet build "Accounting.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Accounting.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Accounting.Service.dll"]
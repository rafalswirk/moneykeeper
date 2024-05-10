FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

ARG ASPNETCORE_ENVIRONMENT=development
ENV ASPNETCORE_ENVIRONMENT=$ASPNETCORE_ENVIRONMENT

WORKDIR /App

COPY . ./

WORKDIR /App/src/Server/Bootstrapper/MoneyKeeper.Web

RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App/src/Server/Bootstrapper/MoneyKeeper.Web
COPY --from=build-env /App/src/Server/Bootstrapper/MoneyKeeper.Web/out .
ENTRYPOINT ["dotnet", "MoneyKeeper.Web.dll", "--urls=http://127.0.0.1:5216"]
ARG ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
ARG ASPNETCORE_ENVIRONMENT
ENV ASPNETCORE_ENVIRONMENT=$ASPNETCORE_ENVIRONMENT
RUN echo ${ASPNETCORE_ENVIRONMENT}
WORKDIR /App

COPY . ./

WORKDIR /App/src/Server/Bootstrapper/MoneyKeeper.Web

RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
ARG ASPNETCORE_ENVIRONMENT
ENV ASPNETCORE_ENVIRONMENT=$ASPNETCORE_ENVIRONMENT
RUN echo ${ASPNETCORE_ENVIRONMENT}
WORKDIR /App/MoneyKeeper.Web
COPY --from=build-env /App/src/Server/Bootstrapper/MoneyKeeper.Web/out .
ENTRYPOINT ["dotnet", "MoneyKeeper.Web.dll"]
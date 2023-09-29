#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_ENVIRONMENT="Development"
ENV DbSettings__ConnectionString="mongodb://localhost:27017/"
ENV DbSettings__DatabaseName="HealthHubDb"
ENV DbSettings__DomainListDatabaseName="HealthHubDomainListsDb"
ENV JwtTokenSettings__Issuer="HealthHub"
ENV JwtTokenSettings__Audience="Users"
ENV JwtTokenSettings__SecretKey="KUHql1BwM52UUCPJXz9qbgABNpZicFSx"
ENV ENV__EMAIL_HOST="smtp.gmail.com"
ENV ENV__EMAIL_PORT="587"
ENV ENV__EMAIL_USERNAME="HealthHub"
ENV ENV__EMAIL_USERNAME_ADDRESS="miguelangels2468@gmail.com"
ENV ENV__EMAIL_PASSWORD="bnpdnueyztavmtcc"
ENV ENV__EXPIRATION_DATE_DAYS="1"
ENV ENV__PROJECT_NAME="HealthHub"

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["HealthHub.WebApi/HealthHub.WebApi.csproj", "HealthHub.WebApi/"]
COPY ["HealthHub.BusinessLogic/HealthHub.BusinessLogic.csproj", "HealthHub.BusinessLogic/"]
COPY ["HealthHub.Application/HealthHub.Application.csproj", "HealthHub.Application/"]
COPY ["HealthHub.Domain/HealthHub.Domain.csproj", "HealthHub.Domain/"]
COPY ["HealthHub.Persistence/HealthHub.Persistence.csproj", "HealthHub.Persistence/"]
RUN dotnet restore "HealthHub.WebApi/HealthHub.WebApi.csproj"
COPY . .
WORKDIR "/src/HealthHub.WebApi"
RUN dotnet build "HealthHub.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HealthHub.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HealthHub.WebApi.dll"]
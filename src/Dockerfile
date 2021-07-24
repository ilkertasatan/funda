FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
COPY /src .
WORKDIR /Funda.Assignment.Api
RUN dotnet restore Funda.Assignment.Api.csproj
RUN dotnet build Funda.Assignment.Api.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish Funda.Assignment.Api.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Funda.Assignment.Api.dll"]

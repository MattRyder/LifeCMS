FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
# COPY ./docker/waitforit.sh .

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

COPY LifeCMS.sln .

# Keep the project list equal to the solution file to maximize image cache use.
COPY src/Services/ContentCreation/ContentCreation.API/ContentCreation.API.csproj src/Services/ContentCreation/ContentCreation.API/
COPY src/Services/ContentCreation/ContentCreation.Domain/ContentCreation.Domain.csproj src/Services/ContentCreation/ContentCreation.Domain/
COPY src/Services/ContentCreation/ContentCreation.Infrastructure/ContentCreation.Infrastructure.csproj src/Services/ContentCreation/ContentCreation.Infrastructure/
COPY src/Services/ContentCreation/ContentCreation.IntegrationTests/ContentCreation.IntegrationTests.csproj src/Services/ContentCreation/ContentCreation.IntegrationTests/
COPY src/Services/ContentCreation/ContentCreation.UnitTests/ContentCreation.UnitTests.csproj src/Services/ContentCreation/ContentCreation.UnitTests/
COPY src/Services/Identity/Identity.API/Identity.API.csproj src/Services/Identity/Identity.API/
COPY src/Services/Identity/Identity.Infrastructure/Identity.Infrastructure.csproj src/Services/Identity/Identity.Infrastructure/

RUN dotnet restore LifeCMS.sln

COPY . .
WORKDIR /src/src/Services/ContentCreation/ContentCreation.API

RUN dotnet publish --no-restore -c Release -o /app

FROM build as publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT [ "dotnet", "ContentCreation.API.dll" ]

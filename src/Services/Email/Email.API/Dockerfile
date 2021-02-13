FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY LifeCMS.sln .

# Keep the project list equal to the solution file to maximize image cache use.
COPY src/EventBus/EventBus.Common/EventBus.Common.csproj src/EventBus/EventBus.Common/
COPY src/EventBus/EventBus.IntegrationEvents/EventBus.IntegrationEvents.csproj src/EventBus/EventBus.IntegrationEvents/
COPY src/EventBus/EventBus.RabbitMQ/EventBus.RabbitMQ.csproj src/EventBus/EventBus.RabbitMQ/
COPY src/Services/Email/Email.Infrastructure/Email.Infrastructure.csproj src/Services/Email/Email.Infrastructure/
COPY src/Services/Email/Email.API/Email.API.csproj src/Services/Email/Email.API/
COPY src/Services/Email/Email.Domain/Email.Domain.csproj src/Services/Email/Email.Domain/
COPY src/Services/Email/Email.UnitTests/Email.UnitTests.csproj src/Services/Email/Email.UnitTests/
COPY src/Services/ContentCreation/ContentCreation.API/ContentCreation.API.csproj src/Services/ContentCreation/ContentCreation.API/
COPY src/Services/ContentCreation/ContentCreation.Domain/ContentCreation.Domain.csproj src/Services/ContentCreation/ContentCreation.Domain/
COPY src/Services/ContentCreation/ContentCreation.Infrastructure/ContentCreation.Infrastructure.csproj src/Services/ContentCreation/ContentCreation.Infrastructure/
COPY src/Services/ContentCreation/ContentCreation.IntegrationTests/ContentCreation.IntegrationTests.csproj src/Services/ContentCreation/ContentCreation.IntegrationTests/
COPY src/Services/ContentCreation/ContentCreation.UnitTests/ContentCreation.UnitTests.csproj src/Services/ContentCreation/ContentCreation.UnitTests/
COPY src/Services/Identity/Identity.API/Identity.API.csproj src/Services/Identity/Identity.API/
COPY src/Services/Identity/Identity.Infrastructure/Identity.Infrastructure.csproj src/Services/Identity/Identity.Infrastructure/

RUN dotnet restore LifeCMS.sln

COPY . .
WORKDIR /src/src/Services/Email/Email.API

RUN dotnet publish --no-restore -c Release -o /app

FROM build as publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT [ "dotnet", "Email.API.dll" ]

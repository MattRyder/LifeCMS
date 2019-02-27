FROM microsoft/dotnet:2.1-sdk AS base
WORKDIR /app
COPY . .

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 8080

WORKDIR /app/Socialite.Domain
RUN dotnet restore

WORKDIR /app/Socialite.Infrastructure
RUN dotnet restore

WORKDIR /app/Socialite.WebAPI
RUN dotnet restore

FROM base AS publish
RUN dotnet publish --no-restore -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
RUN chmod +x ./entrypoint.sh
RUN chmod +x ./.docker/waitforit.sh
RUN env
CMD /bin/bash ./.docker/waitforit.sh $SQL_DB_HOST:3306 -- ./entrypoint.sh
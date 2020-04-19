FROM faithlife/aspnetcore-build-yarn:2019.12 AS base
WORKDIR /build
COPY ./docker/waitforit.sh .

WORKDIR /source
COPY . .

WORKDIR /source/Socialite.Domain
RUN dotnet restore

WORKDIR /source/Socialite.Infrastructure
RUN dotnet restore

WORKDIR /source/Socialite.Authentication
RUN dotnet restore

FROM base AS publish
RUN dotnet publish --no-restore -c Release -o /build/Socialite
RUN cp /source/Socialite.Domain/Socialite.Domain.csproj /build/Socialite
RUN cp /source/Socialite.Infrastructure/Socialite.Infrastructure.csproj /build/Socialite
RUN cp /source/Socialite.Authentication/Socialite.Authentication.csproj /build/Socialite

FROM base AS final
WORKDIR /app
COPY --from=publish /build .
EXPOSE 80/tcp
ENV ASPNETCORE_URLS http://0.0.0.0:80

ENV PATH="${PATH}:/root/.dotnet/tools"

RUN dotnet tool install --global dotnet-ef

RUN chmod +x ./waitforit.sh

WORKDIR /app/Socialite

ENTRYPOINT [ "/bin/bash", "../waitforit.sh", "$SQL_DB_HOST:3306", "--", "dotnet", "Socialite.Authentication.dll" ]
Socialite
&middot;
[![CircleCI](https://circleci.com/gh/MattRyder/Socialite/tree/master.svg?style=svg)](https://circleci.com/gh/MattRyder/Socialite/tree/master)
===

A self-hosted social content management system, built on ASP Core.

## Installing Socialite via `docker-compose`

Requires [docker](https://www.docker.com/) and [docker-compose](https://docs.docker.com/compose/) installed.

```bash
docker-compose build

docker-compose up
```

## Building Socialite

```bash
dotnet run -p Socialite.WebAPI
```

## Testing Socialite

```bash
dotnet test
```
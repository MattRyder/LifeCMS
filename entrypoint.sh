#!/bin/bash

set -e
dotnet_run_cmd="dotnet watch --project Socialite.WebAPI run"

set -e
dotnet_migrate_db_cmd="dotnet ef database update --startup-project Socialite.WebAPI --project Socialite.Infrastructure"

until $dotnet_migrate_db_cmd; do
    >&2 echo "Migrating Database..."
    sleep 1
done

>&2 echo "Database migrated. Booting Socialite..."

exec $dotnet_run_cmd
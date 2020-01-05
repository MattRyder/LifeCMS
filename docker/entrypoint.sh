set -e
dotnet_run_cmd="dotnet run --project Socialite/Socialite.WebAPI"

# set -e
# dotnet_migrate_db_cmd="dotnet ef database update --startup-project Socialite/Socialite.WebAPI --project Socialite/Socialite.Infrastructure"

# until $dotnet_migrate_db_cmd; do
#     >&2 echo "Migrating Database..."
#     sleep 1
# done

>&2 echo "Booting Socialite..."

exec $dotnet_run_cmd
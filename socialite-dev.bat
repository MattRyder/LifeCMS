start "Socialite.WebAPI" /MIN dotnet watch --project Socialite.WebAPI\ run &
start "Socialite.ReactClient" /MIN yarn --cwd Socialite.ReactClient\ start &
start "Socialite.Authentication" /MIN dotnet watch --project Socialite.Authentication\ run
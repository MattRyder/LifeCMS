start "LifeCMS.WebAPI" /MIN dotnet watch --project LifeCMS.WebAPI\ run &
start "LifeCMS.ReactClient" /MIN yarn --cwd LifeCMS.ReactClient\ start &
start "LifeCMS.Services.Identity.API" /MIN dotnet watch --project LifeCMS.Services.Identity.API\ run
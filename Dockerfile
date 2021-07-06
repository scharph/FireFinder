FROM mcr.microsoft.com/dotnet/aspnet:3.1
COPY FireFinder.Api/bin/Debug/netcoreapp3.1/ App/
WORKDIR /App
ENTRYPOINT ["dotnet", "FireFinder.API.dll"]
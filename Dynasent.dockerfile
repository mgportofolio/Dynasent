FROM mcr.microsoft.com/dotnet/core/sdk:2.2 as build
COPY . / src
WORKDIR /src/Dynasent
RUN dotnet restore
RUN dotnet publish -c Release

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-alpine as runtime
COPY --from=build  /src/Dynasent/bin/Release/netcore2.2/publish/app

WORKDIR /app
ENTRYPOINT ["dotnet", "Dynasent.dll"]



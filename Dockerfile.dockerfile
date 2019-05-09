FROM mcr.microsoft.com/dotnet/core/sdk:2.2 as BUILD
COPY . / src
WORKDIR /src/Dynasent
RUN dotnet restore
RUN dotnet publish -c Release

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 as runtime
COPY --from=build  /src/Dynasent/bin/Release/netcore2.2/publish/app

WORKDIR /app
ENTRYPOINT ["dotner", "Dynasent.dll"]

docker build . -t dynasent.0.0.2
docker image ls
docker run -p 33240:80 dynasent.0.0.2

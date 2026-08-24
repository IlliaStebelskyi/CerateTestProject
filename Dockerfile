FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /EfCorePart2
COPY . ./
RUN dotnet restore
RUN dotnet publish -o out

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /EfCorePart2
RUN apt-get update && apt-get install -y libkrb5-3 && rm -rf /var/lib/apt/lists/*
COPY --from=build /EfCorePart2/out .
ENTRYPOINT ["dotnet", "EfCorePart2.dll"]
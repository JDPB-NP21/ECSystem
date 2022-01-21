# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /repo/app

# Copy csproj and restore as distinct layers
COPY ./ECSystem.Server.Main/*.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY ./ECSystem.Server.Main ./
COPY ./Protos ../Protos
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /repo/app/out .
ENTRYPOINT ["dotnet", "ECSystem.Server.Main.dll"]

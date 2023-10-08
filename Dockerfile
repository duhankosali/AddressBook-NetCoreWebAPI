# See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Base image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# SDK image to build and publish our app
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy csproj files and restore to cache the layers for faster builds
COPY ["AddressBook.API/AddressBook.API.csproj", "AddressBook.API/"]
COPY ["AddressBook.Service/AddressBook.Service.csproj", "AddressBook.Service/"]
COPY ["AddressBook.Repository/AddressBook.Repository.csproj", "AddressBook.Repository/"]
COPY ["AddressBook.Core/AddressBook.Core.csproj", "AddressBook.Core/"]

RUN dotnet restore "AddressBook.API/AddressBook.API.csproj"
RUN dotnet restore "AddressBook.Service/AddressBook.Service.csproj"
RUN dotnet restore "AddressBook.Repository/AddressBook.Repository.csproj"
RUN dotnet restore "AddressBook.Core/AddressBook.Core.csproj"

# Copy all the source code and build
COPY . .
WORKDIR "/src/AddressBook.API"
RUN dotnet build "AddressBook.API.csproj" -c Release -o /app/build

# Publish the API project
FROM build AS publish
RUN dotnet publish "AddressBook.API.csproj" -c Release -o /app/publish

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV DOCKER_ENVIRONMENT=Docker

ENTRYPOINT ["dotnet", "AddressBook.API.dll"]
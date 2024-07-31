
# Set the base image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /app

# Copy the source code
COPY . .

WORKDIR /app/pest.logging
RUN dotnet restore pest.logging.csproj

WORKDIR /app/pest.puid
RUN dotnet restore pest.puid.csproj

# Build the application
RUN dotnet publish -c Release -o out

# Set the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory
WORKDIR /app/pest.puid
EXPOSE 8080

# Copy the published output from the build stage
COPY --from=build /app/pest.puid/out .

# Set the entry point
ENTRYPOINT ["dotnet", "pest.puid.dll"]
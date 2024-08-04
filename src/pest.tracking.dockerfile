
# Set the base image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /app

# Copy the source code
COPY . .

WORKDIR /app/pest.tracking
RUN dotnet restore pest.tracking.csproj


# Build the application
RUN dotnet publish -c Release -o out

# Set the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory
WORKDIR /app/pest.tracking
EXPOSE 8080

# Copy the published output from the build stage
COPY --from=build /app/pest.tracking/out .

# Set the entry point
ENTRYPOINT ["dotnet", "pest.tracking.dll"]
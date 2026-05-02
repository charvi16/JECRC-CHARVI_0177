# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and publish
COPY . ./
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# 👇 Force app to run on port 80
ENV ASPNETCORE_URLS=http://+:80

# Copy published output
COPY --from=build /app/publish .

# 👇 Expose port 80 (for clarity, not mandatory but recommended)
EXPOSE 80

ENTRYPOINT ["dotnet", "EmpSinMemoryApi.dll"]
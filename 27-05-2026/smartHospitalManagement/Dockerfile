FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY SmartHospital.Api/SmartHospital.Api.csproj SmartHospital.Api/
RUN dotnet restore SmartHospital.Api/SmartHospital.Api.csproj
COPY SmartHospital.Api SmartHospital.Api
RUN dotnet publish SmartHospital.Api/SmartHospital.Api.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "SmartHospital.Api.dll"]

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/OfficesService/EasyClinic.OfficesService.Api/EasyClinic.OfficesService.Api.csproj", "src/Services/OfficesService/EasyClinic.OfficesService.Api/"]
RUN dotnet restore "./src/Services/OfficesService/EasyClinic.OfficesService.Api/EasyClinic.OfficesService.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/OfficesService/EasyClinic.OfficesService.Api"
RUN dotnet build "./EasyClinic.OfficesService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EasyClinic.OfficesService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EasyClinic.OfficesService.Api.dll"]
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Services/AuthService/EasyClinic.AuthService.Api/EasyClinic.AuthService.Api.csproj", "src/Services/AuthService/EasyClinic.AuthService.Api/"]
RUN dotnet restore "src/Services/AuthService/EasyClinic.AuthService.Api/EasyClinic.AuthService.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/AuthService/EasyClinic.AuthService.Api"
RUN dotnet build "EasyClinic.AuthService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EasyClinic.AuthService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EasyClinic.AuthService.Api.dll"]
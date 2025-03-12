FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/CRM-KSK.Api/CRM-KSK.Api.csproj", "src/CRM-KSK.Api/"]
COPY ["src/CRM-KSK.Application/CRM-KSK.Application.csproj", "src/CRM-KSK.Application/"]
COPY ["src/CRM-KSK.Core/CRM-KSK.Core.csproj", "src/CRM-KSK.Core/"]
COPY ["src/CRM-KSK.Dal.PostgreSQL/CRM-KSK.Dal.PostgreSQL.csproj", "src/CRM-KSK.Dal.PostgreSQL/"]
COPY ["src/CRM-KSK.Infrastructure/CRM-KSK.Infrastructure.csproj", "src/CRM-KSK.Infrastructure/"]
RUN dotnet restore "./src/CRM-KSK.Api/CRM-KSK.Api.csproj"
COPY . .
WORKDIR "/src/src/CRM-KSK.Api"
RUN dotnet build "./CRM-KSK.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CRM-KSK.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CRM-KSK.Api.dll"]
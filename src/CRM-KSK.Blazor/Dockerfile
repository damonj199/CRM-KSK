FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/CRM-KSK.Blazor/CRM-KSK.Blazor.csproj", "."]
RUN dotnet restore 
COPY . .

RUN dotnet build "src/CRM-KSK.Blazor/CRM-KSK.Blazor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/CRM-KSK.Blazor/CRM-KSK.Blazor.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY src/CRM-KSK.Blazor/nginx.conf /etc/nginx/nginx.conf
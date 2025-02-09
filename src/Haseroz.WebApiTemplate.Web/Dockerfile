FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/Haseroz.WebApiTemplate.Web/Haseroz.WebApiTemplate.Web.csproj", "src/Haseroz.WebApiTemplate.Web/"]
RUN dotnet restore "./src/Haseroz.WebApiTemplate.Web/Haseroz.WebApiTemplate.Web.csproj"

COPY . .
WORKDIR "/src/src/Haseroz.WebApiTemplate.Web"

RUN dotnet build "./Haseroz.WebApiTemplate.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "./Haseroz.WebApiTemplate.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Haseroz.WebApiTemplate.Web.dll"]

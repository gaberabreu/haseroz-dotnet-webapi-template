FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/Net.WebApi.Skeleton.Web/Net.WebApi.Skeleton.Web.csproj", "src/Net.WebApi.Skeleton.Web/"]
RUN dotnet restore "./src/Net.WebApi.Skeleton.Web/Net.WebApi.Skeleton.Web.csproj"

COPY ["src/Net.WebApi.Skeleton.Web/", "src/Net.WebApi.Skeleton.Web/"]
WORKDIR "/src/src/Net.WebApi.Skeleton.Web"

RUN dotnet build "./Net.WebApi.Skeleton.Web.csproj" -c "$BUILD_CONFIGURATION" -o /app/build

FROM build AS publish
RUN dotnet publish "./Net.WebApi.Skeleton.Web.csproj" -c "$BUILD_CONFIGURATION" -o /app/publish /p:UseAppHost=false

FROM base AS final
RUN useradd -m appuser
USER appuser

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Net.WebApi.Skeleton.Web.dll"]

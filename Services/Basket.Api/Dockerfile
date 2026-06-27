FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8000

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Basket.Api/Basket.Api.csproj", "Services/Basket.Api/"]
COPY ["Services/Basket.Core/Basket.Core.csproj", "Services/Basket.Core/"]
COPY ["Services/Basket.Application/Basket.Application.csproj", "Services/Basket.Application/"]
COPY ["Services/Basket.Infrastructure/Basket.Infrastructure.csproj", "Services/Basket.Infrastructure/"]
RUN dotnet restore "Services/Basket.Api/Basket.Api.csproj"
COPY . .
WORKDIR "/src/Services/Basket.Api"
RUN dotnet build "./Basket.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Basket.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.Api.dll"]

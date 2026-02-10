FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/PlantControlPanel.Web/PlantControlPanel.Web.csproj", "src/PlantControlPanel.Web/"]
COPY ["src/PlantControlPanel.Presentation/PlantControlPanel.Presentation.csproj", "src/PlantControlPanel.Presentation/"]
COPY ["src/PlantControlPanel.Application.Contracts/PlantControlPanel.Application.Contracts.csproj", "src/PlantControlPanel.Application.Contracts/"]
COPY ["src/PlantControlPanel.Infrastructure/PlantControlPanel.Infrastructure.csproj", "src/PlantControlPanel.Infrastructure/"]
COPY ["src/PlantControlPanel.Application.Abstractions/PlantControlPanel.Application.Abstractions.csproj", "src/PlantControlPanel.Application.Abstractions/"]
COPY ["src/PlantControlPanel.Domain/PlantControlPanel.Domain.csproj", "src/PlantControlPanel.Domain/"]
COPY ["src/PlantControlPanel.Application/PlantControlPanel.Application.csproj", "src/PlantControlPanel.Application/"]
RUN dotnet restore "src/PlantControlPanel.Web/PlantControlPanel.Web.csproj"
COPY . .
WORKDIR "/src/src/PlantControlPanel.Web"
RUN dotnet build "./PlantControlPanel.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PlantControlPanel.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["sh", "-c", "dotnet PlantControlPanel.Web.dll"]

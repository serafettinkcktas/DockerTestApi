FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DockerTestApi/DockerTestApi.csproj", "DockerTestApi/"]
RUN dotnet restore "DockerTestApi/DockerTestApi.csproj"
COPY . .
WORKDIR "/src/DockerTestApi"
RUN dotnet build "./DockerTestApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DockerTestApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIROINMENT=Development
ENTRYPOINT ["dotnet", "DockerTestApi.dll"]

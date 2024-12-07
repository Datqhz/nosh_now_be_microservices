﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5237
ENV ASPNETCORE_URLS=http://+:5237

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AuthServer/AuthServer.csproj", "AuthServer/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "AuthServer/AuthServer.csproj"
COPY . .
WORKDIR "/src/AuthServer"
RUN dotnet build "AuthServer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AuthServer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthServer.dll"]
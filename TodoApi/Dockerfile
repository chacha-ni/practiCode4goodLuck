# שלב בסיסי להרצת האפליקציה
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
ENV ASPNETCORE_URLS=http://+:8080

# שלב הבנייה
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TodoApi.csproj", "."]
RUN dotnet restore "./TodoApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./TodoApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# שלב הפרסום
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TodoApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# שלב הרצה סופי
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TodoApi.dll", "--urls", "http://0.0.0.0:${PORT:-8080}"]

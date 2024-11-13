FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 7156 5002

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем файлы проектов
COPY ["StudyPlatform/StudyPlatform.csproj", "StudyPlatform/"]
COPY ["BL/BL.csproj", "BL/"]
COPY ["DAL/DAL.csproj", "DAL/"]

# Восстанавливаем зависимости
RUN dotnet restore "StudyPlatform/StudyPlatform.csproj"

# Копируем все файлы и сборка
COPY . .
WORKDIR "/src/StudyPlatform"
RUN dotnet build "StudyPlatform.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StudyPlatform.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StudyPlatform.dll"]

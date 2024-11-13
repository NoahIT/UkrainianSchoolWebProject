FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 7079 5210

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ChatServer.csproj", "./"]
RUN dotnet restore "./ChatServer.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "ChatServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChatServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatServer.dll"]

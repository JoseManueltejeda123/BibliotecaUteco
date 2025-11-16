FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

# Cambiar puerto a 5000
EXPOSE 5000

# Hacer que ASP.NET Core escuche en el puerto 5000
ENV ASPNETCORE_URLS=http://0.0.0.0:5000

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["BibliotecaUteco/BibliotecaUteco/BibliotecaUteco.csproj", "BibliotecaUteco/BibliotecaUteco/"]
COPY ["BibliotecaUteco/BibliotecaUteco.Client/BibliotecaUteco.Client.csproj", "BibliotecaUteco/BibliotecaUteco.Client/"]

RUN dotnet restore "BibliotecaUteco/BibliotecaUteco/BibliotecaUteco.csproj"

COPY . .
WORKDIR "/src/BibliotecaUteco/BibliotecaUteco"

RUN dotnet build "./BibliotecaUteco.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BibliotecaUteco.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BibliotecaUteco.dll"]
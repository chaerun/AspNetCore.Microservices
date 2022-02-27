#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/UnitAPI/UnitAPI.csproj", "Services/UnitAPI/"]
RUN dotnet restore "Services/UnitAPI/UnitAPI.csproj"
COPY . .
WORKDIR "/src/Services/UnitAPI"
RUN dotnet build "UnitAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UnitAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UnitAPI.dll"]
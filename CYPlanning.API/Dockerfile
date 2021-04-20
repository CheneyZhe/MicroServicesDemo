#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["CYPlanning.API/CYPlanning.API.csproj", "CYPlanning.API/"]
RUN dotnet restore "CYPlanning.API/CYPlanning.API.csproj"
COPY . .
WORKDIR "/src/CYPlanning.API"
RUN dotnet build "CYPlanning.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CYPlanning.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CYPlanning.API.dll"]
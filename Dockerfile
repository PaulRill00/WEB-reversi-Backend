#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM paulrill/reversi:frontend AS frontend

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ReversiRestAPI/ReversiRestAPI.csproj", "ReversiRestAPI/"]
RUN dotnet restore "ReversiRestAPI/ReversiRestAPI.csproj"
COPY . .
WORKDIR "/src/ReversiRestAPI"
RUN dotnet build "ReversiRestAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReversiRestAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=frontend /wwwroot /app/wwwroot
ENTRYPOINT ["dotnet", "ReversiRestAPI.dll"]
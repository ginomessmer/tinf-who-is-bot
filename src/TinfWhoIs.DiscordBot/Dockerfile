#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TinfWhoIs.DiscordBot/TinfWhoIs.DiscordBot.csproj", "TinfWhoIs.DiscordBot/"]
RUN dotnet restore "TinfWhoIs.DiscordBot/TinfWhoIs.DiscordBot.csproj"
COPY . .
WORKDIR "/src/TinfWhoIs.DiscordBot"
RUN dotnet build "TinfWhoIs.DiscordBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TinfWhoIs.DiscordBot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TinfWhoIs.DiscordBot.dll"]
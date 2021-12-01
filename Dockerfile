#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WhistleblowerSystem.Database/WhistleblowerSystem.Database.csproj", "WhistleblowerSystem.Database/"]
COPY ["WhistleblowerSystem/Server/WhistleblowerSystem.Server.csproj", "WhistleblowerSystem/Server/"]
COPY ["WhistleblowerSystem/Shared/WhistleblowerSystem.Shared.csproj", "WhistleblowerSystem.Shared/"]
COPY ["WhistleblowerSystem.Business/WhistleblowerSystem.Business.csproj", "WhistleblowerSystem.Business/"]
COPY ["Initialization/WhistleblowerSystem.Initialization.csproj", "Initialization/"]
RUN dotnet restore "WhistleblowerSystem/Server/WhistleblowerSystem.Server.csproj"
COPY . .
WORKDIR "/src/WhistleblowerSystem/Server"
RUN dotnet build "WhistleblowerSystem.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WhistleblowerSystem.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "WhistleblowerSystem.Server.dll"]
#Heroku
CMD ASPNETCORE_URLS=http://*:$PORT dotnet WhistleblowerSystem.Server.dll
#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/OchoaLopes.SlingShot.APIService/OchoaLopes.SlingShot.APIService.csproj", "src/OchoaLopes.SlingShot.APIService/"]
RUN dotnet restore "src/OchoaLopes.SlingShot.APIService/OchoaLopes.SlingShot.APIService.csproj"
COPY . .
WORKDIR "/src/src/OchoaLopes.SlingShot.APIService"
RUN dotnet build "OchoaLopes.SlingShot.APIService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OchoaLopes.SlingShot.APIService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OchoaLopes.SlingShot.APIService.dll"]
#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["User/User.csproj", "User/"]
COPY ["Framework.Service/Framework.Service.csproj", "Framework.Service/"]
COPY ["Framework.Configuration/Framework.Configuration.csproj", "Framework.Configuration/"]
COPY ["Framework.Constant/Framework.Constant.csproj", "Framework.Constant/"]
RUN dotnet restore "./User/./User.csproj"
COPY . .
WORKDIR "/src/User"
RUN dotnet build "./User.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./User.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.dll"]
#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/UMBIT.ToDo.API/UMBIT.ToDo.API.csproj", "src/UMBIT.ToDo.API/"]
COPY ["src/UMBIT.ToDo.Core/UMBIT.ToDo.Core.csproj", "src/UMBIT.ToDo.Core/"]
COPY ["src/UMBIT.ToDo.Dominio/UMBIT.ToDo.Dominio.csproj", "src/UMBIT.ToDo.Dominio/"]
COPY ["src/UMBIT.ToDo.Infraestrutura/UMBIT.ToDo.Infraestrutura.csproj", "src/UMBIT.ToDo.Infraestrutura/"]
RUN dotnet restore "./src/UMBIT.ToDo.API/./UMBIT.ToDo.API.csproj"
COPY . .
WORKDIR "/src/src/UMBIT.ToDo.API"
RUN dotnet build "./UMBIT.ToDo.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UMBIT.ToDo.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UMBIT.ToDo.API.dll"]
#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Source/WebAPI/WebAPI.csproj", "Source/WebAPI/"]
COPY ["Source/Application/Application.csproj", "Source/Application/"]
COPY ["Source/Domain/Domain.csproj", "Source/Domain/"]
COPY ["Source/Infrastructure/Infrastructure.csproj", "Source/Infrastructure/"]
RUN dotnet restore "Source/WebAPI/WebAPI.csproj"
COPY . .
WORKDIR "/src/Source/WebAPI"
RUN dotnet build "WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EOrchestralBriefcase.WebAPI.dll"]
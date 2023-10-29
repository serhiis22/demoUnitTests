#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Subscription.Service/Presentation/WebApi/Subscription.WebApi.csproj", "Subscription.Service/Presentation/WebApi/"]
COPY ["Subscription.Service/Core/Application/Subscription.Application.csproj", "Subscription.Service/Core/Application/"]
COPY ["Subscription.Service/Core/Domain/Subscription.Domain.csproj", "Subscription.Service/Core/Domain/"]
COPY ["Subscription.Service/Infrastructure/Persistence/Subscription.Persistence.csproj", "Subscription.Service/Infrastructure/Persistence/"]
RUN dotnet restore "Subscription.Service/Presentation/WebApi/Subscription.WebApi.csproj"
COPY . .
WORKDIR "/src/Subscription.Service/Presentation/WebApi"
RUN dotnet build "Subscription.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Subscription.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Subscription.WebApi.dll"]
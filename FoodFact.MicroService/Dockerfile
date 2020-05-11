#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["FoodFact.MicroService/FoodFact.MicroService.csproj", "FoodFact.MicroService/"]
COPY ["FoodFact.DTO/FoodFact.DTO.csproj", "FoodFact.DTO/"]
COPY ["FoodFact.BusinessContract/FoodFact.BusinessContract.csproj", "FoodFact.BusinessContract/"]
COPY ["FoodFact.Business/FoodFact.Business.csproj", "FoodFact.Business/"]
RUN dotnet restore "FoodFact.MicroService/FoodFact.MicroService.csproj"
COPY . .
WORKDIR "/src/FoodFact.MicroService"
RUN dotnet build "FoodFact.MicroService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FoodFact.MicroService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FoodFact.MicroService.dll"]
#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Discount/API/Discount.Api/Discount.Api.csproj", "Services/Discount/API/Discount.Api/"]
COPY ["Services/Discount/API/Discount.Infrastructure/Discount.Infrastructure.csproj", "Services/Discount/API/Discount.Infrastructure/"]
COPY ["Services/Discount/API/Discount.Core/Discount.Core.csproj", "Services/Discount/API/Discount.Core/"]
RUN dotnet restore "Services/Discount/API/Discount.Api/Discount.Api.csproj"
COPY . .
WORKDIR "/src/Services/Discount/API/Discount.Api"
RUN dotnet build "Discount.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Discount.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Discount.Api.dll"]

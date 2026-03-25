FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["Med.Api/Med.Api.csproj", "Med.Api/"]
COPY ["Med.Core/Med.Core.csproj", "Med.Core/"]
COPY ["Med.Web/Med.Web.csproj", "Med.Web/"]

RUN dotnet restore "Med.Api/Med.Api.csproj"

COPY . .

RUN dotnet publish "Med.Api/Med.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Med.Api.dll"]
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["./API/DocsNetAPI.csproj", "API/"]
COPY ["./Application/Application.csproj", "Application/"]
COPY ["./Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["./Domain/Domain.csproj", "Domain/"]

RUN dotnet restore "./API/"

WORKDIR /src
COPY . .

WORKDIR /src/API
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./  

ENTRYPOINT ["dotnet", "DocsNetAPI.dll"]

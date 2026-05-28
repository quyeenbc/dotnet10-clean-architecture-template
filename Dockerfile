FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["CleanArchitecture.API/CleanArchitecture.API.csproj", "CleanArchitecture.API/"]
COPY ["CleanArchitecture.Application/CleanArchitecture.Application.csproj", "CleanArchitecture.Application/"]
COPY ["CleanArchitecture.Domain/CleanArchitecture.Domain.csproj", "CleanArchitecture.Domain/"]
COPY ["CleanArchitecture.Infrastructure/CleanArchitecture.Infrastructure.csproj", "CleanArchitecture.Infrastructure/"]

RUN dotnet restore "CleanArchitecture.API/CleanArchitecture.API.csproj"

COPY . .
RUN dotnet build "CleanArchitecture.API/CleanArchitecture.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CleanArchitecture.API/CleanArchitecture.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleanArchitecture.API.dll"]
FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TestUnit/TestUnit.csproj", "TestUnit/"]
RUN dotnet restore "TestUnit/TestUnit.csproj"
COPY . .
WORKDIR "/src/TestUnit"
RUN dotnet build "TestUnit.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestUnit.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestUnit.dll"]

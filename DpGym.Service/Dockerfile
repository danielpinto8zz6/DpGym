FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DpGym/DpGym.csproj", "DpGym/"]
RUN dotnet restore "DpGym/DpGym.csproj"
COPY . .
WORKDIR "/src/DpGym"
RUN dotnet build "DpGym.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DpGym.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DpGym.dll"]

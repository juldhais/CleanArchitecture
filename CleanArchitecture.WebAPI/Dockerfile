FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CleanArchitecture.WebAPI/CleanArchitecture.WebAPI.csproj", "CleanArchitecture.WebAPI/"]
RUN dotnet restore "CleanArchitecture.WebAPI/CleanArchitecture.WebAPI.csproj"
COPY . .
WORKDIR "/src/CleanArchitecture.WebAPI"
RUN dotnet build "CleanArchitecture.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CleanArchitecture.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleanArchitecture.WebAPI.dll"]

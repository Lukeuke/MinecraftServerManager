FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Web/Web.Presentation/Web.Presentation.csproj", "Web.Presentation/"]
RUN dotnet restore "src/Web/Web.Presentation/Web.Presentation.csproj"
COPY . .
WORKDIR "/src/Web.Presentation"
RUN dotnet build "Web.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.Presentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.Presentation.dll"]

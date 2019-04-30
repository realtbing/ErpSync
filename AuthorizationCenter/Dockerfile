FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["AuthorizationCenter/AuthorizationCenter.csproj", "AuthorizationCenter/"]
RUN dotnet restore "AuthorizationCenter/AuthorizationCenter.csproj"
COPY . .
WORKDIR "/src/AuthorizationCenter"
RUN dotnet build "AuthorizationCenter.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AuthorizationCenter.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AuthorizationCenter.dll"]
#FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
#WORKDIR /app
#EXPOSE 10001
#EXPOSE 44330
#
#FROM microsoft/dotnet:2.1-sdk AS build
#WORKDIR /src
#COPY ["/Api/Api.csproj", "/Api"]
#RUN dotnet restore "Api.csproj"
#COPY . .
#WORKDIR "/src"
#RUN dotnet build "Api.csproj" -c Release -o /app
#
#FROM build AS publish
#RUN dotnet publish "Api.csproj" -c Release -o /app
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app .
#ENTRYPOINT ["dotnet", "Api.dll"]
FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base

COPY . /publish

WORKDIR /publish

EXPOSE 10001

CMD ["dotnet", "Api.dll", "--server.urls", "http://*:10001"]
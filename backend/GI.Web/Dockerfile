FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy the entire backend folder
COPY . .

# Restore the web project (project references will resolve)
RUN dotnet restore "GI.Web/GI.Web.csproj"

# Publish
RUN dotnet publish "GI.Web/GI.Web.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "GI.Web.dll"]
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .

# Set the ASP.NET Core port to 80
ENV ASPNETCORE_HTTP_PORTS=80
EXPOSE 80

ENTRYPOINT ["dotnet", "AllYourGoods.Api.dll"]
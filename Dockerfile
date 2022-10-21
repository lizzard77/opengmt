FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

RUN apt-get update 
RUN apt-get install -y libpng-dev libjpeg-dev curl libxi6 build-essential libgl1-mesa-glx
RUN curl -sL https://deb.nodesource.com/setup_lts.x | bash -
RUN apt-get install -y nodejs

WORKDIR /App
# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
RUN npm ci && npm run build
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /App
COPY --from=build-env /App/out .
EXPOSE 80
VOLUME [ "/data" ]
ENTRYPOINT ["dotnet", "opengmt.dll"]
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /backend

#NEEDED FOR SWAGGER TO WORK
ENV ASPNETCORE_ENVIRONMENT=Development

COPY . .
COPY Directory.Build.props .

RUN apt-get update && apt-get -y install sudo

RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

FROM base AS build
WORKDIR /src
COPY ["backend.csproj", "./"]
RUN dotnet restore
COPY . .
WORKDIR "/src/."
RUN dotnet build "backend.csproj" -c Release -o /backend/build

FROM build AS publish
RUN dotnet publish -c Release -o /backend/publish /p:UseAppHost=false


FROM base AS final
ENV ASPNETCORE_URLS=http://+:5252
WORKDIR /backend
COPY --from=publish /backend/publish .
ENTRYPOINT dotnet watch run


#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /qwe
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /scr
COPY ["GrpcClient/GrpcClient.csproj", "GrpcClient/"]
COPY ["GrpcSample/GrpcSample.csproj", "GrpcSample/"]
RUN dotnet restore "GrpcClient/GrpcClient.csproj"
COPY . .
WORKDIR "/scr/GrpcClient"
RUN dotnet build "GrpcClient.csproj" -c Release -o /qwe/build

FROM build AS publish
RUN dotnet publish "GrpcClient.csproj" -c Release -o /qwe/publish

FROM base AS final
WORKDIR /qwe
COPY --from=publish /qwe/publish .
ENTRYPOINT ["dotnet", "GrpcClient.dll"]
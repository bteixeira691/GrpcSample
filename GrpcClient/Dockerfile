#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /app
COPY . ./
dotnet restore "GrpcClient/GrpcClient.csproj"
RUN dotnet publish "GrpcClient/GrpcClient.csproj" -c Release -o out
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "GrpcClient.dll"]



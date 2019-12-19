FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /app
COPY . ./
RUN dotnet restore "GrpcSample/GrpcSample.csproj"
RUN dotnet publish "GrpcSample/GrpcSample.csproj" -c Release -o out
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "GrpcSample.dll"]
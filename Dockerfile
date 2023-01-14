FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 as build
WORKDIR /src
COPY ["src/Better.Api/Better.Api.csproj", "src/Better.Api/"]
COPY ["src/Better.Application/Better.Application.csproj", "src/Better.Application/"]
COPY ["src/Better.Core/Better.Core.csproj", "src/Better.Core/"]
COPY ["src/Better.Infrastructure/Better.Infrastructure.csproj", "src/Better.Infrastructure/"]
RUN dotnet restore "src/Better.Api/Better.Api.csproj"
COPY . .
WORKDIR "/src/src/Better.Api"
RUN dotnet build "Better.Api.csproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish "Better.Api.csproj" -c Release -o /app/publish

FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ConnectionStrings__SqlConnection="User ID=challengeuser;Password=challengepass;Server=104.197.55.31;Port=5432;Database=challenge;Integrated Security=true;Pooling=true;"

ENTRYPOINT ["dotnet", "Better.Api.dll"]
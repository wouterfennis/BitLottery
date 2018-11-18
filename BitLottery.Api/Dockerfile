FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["BitLottery.Api/BitLottery.Api.csproj", "BitLottery.Api/"]
COPY ["BitLottery.Business/BitLottery.Business.csproj", "BitLottery.Business/"]
COPY ["BitLottery.Entities/BitLottery.Entities.csproj", "BitLottery.Entities/"]
COPY ["BitLottery.Utilities/BitLottery.Utilities.csproj", "BitLottery.Utilities/"]
COPY ["BitLottery.Random/BitLottery.RandomService.csproj", "BitLottery.Random/"]
COPY ["BitLottery.Database/BitLottery.Database.csproj", "BitLottery.Database/"]
RUN dotnet restore "BitLottery.Api/BitLottery.Api.csproj"
COPY . .
WORKDIR "/src/BitLottery.Api"
RUN dotnet build "BitLottery.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BitLottery.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BitLottery.Api.dll"]
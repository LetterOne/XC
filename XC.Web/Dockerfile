#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-sac2016 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk-nanoserver-sac2016 AS build
WORKDIR /src
COPY ["XC.Web/XC.Web.csproj", "XC.Web/"]
RUN dotnet restore "XC.Web/XC.Web.csproj"
COPY . .
WORKDIR "/src/XC.Web"
RUN dotnet build "XC.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "XC.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "XC.Web.dll"]
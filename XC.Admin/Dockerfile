#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 51077
EXPOSE 44346

FROM microsoft/dotnet:2.1-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY ["XC.Admin/XC.Admin.csproj", "XC.Admin/"]
RUN dotnet restore "XC.Admin/XC.Admin.csproj"
COPY . .
WORKDIR "/src/XC.Admin"
RUN dotnet build "XC.Admin.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "XC.Admin.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "XC.Admin.dll"]
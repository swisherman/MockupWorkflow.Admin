FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

ARG BUILD_CONFIGURATION=Debug


COPY ["MockupWorkflow.Admin/src/MockupWorkflow.Admin.Web/MockupWorkflow.Admin.Web.csproj", "MockupWorkflow.Admin/src/MockupWorkflow.Admin.Web/"]
COPY ["MockupWorkflow.Shared/MockupWorkflow.Shared/MockupWorkflow.Shared.csproj", "MockupWorkflow.Shared/MockupWorkflow.Shared/"]


RUN dotnet restore "MockupWorkflow.Admin/src/MockupWorkflow.Admin.Web/MockupWorkflow.Admin.Web.csproj"

COPY . .

WORKDIR "/src/MockupWorkflow.Admin/src/MockupWorkflow.Admin.Web"

RUN dotnet publish "MockupWorkflow.Admin.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "MockupWorkflow.Admin.Web.dll"]
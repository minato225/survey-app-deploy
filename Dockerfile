FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release

COPY ["Directory.Build.props", "./"]
COPY ["Directory.Packages.props", "./"]
COPY ["src/SurveyApp.Presentation/SurveyApp.Presentation.csproj", "src/SurveyApp.Presentation/"]
COPY ["src/SurveyApp.Core/SurveyApp.Application.csproj", "src/SurveyApp.Core/"]
COPY ["src/SurveyApp.ExternalServices/SurveyApp.ExternalServices.csproj", "src/SurveyApp.ExternalServices/"]
COPY ["src/SurveyApp.Domain/SurveyApp.Domain.csproj", "src/SurveyApp.Domain/"]
COPY ["src/SurveyApp.Infrastructure/SurveyApp.Infrastructure.csproj", "src/SurveyApp.Infrastructure/"]

RUN dotnet restore "src/SurveyApp.Presentation/SurveyApp.Presentation.csproj" \
    --verbosity n \
    --runtime linux-x64

COPY . .
WORKDIR "/src/SurveyApp.Presentation"
RUN dotnet build "SurveyApp.Presentation.csproj" \
    -c $BUILD_CONFIGURATION \
    --no-restore \
    -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SurveyApp.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SurveyApp.Presentation.dll"]

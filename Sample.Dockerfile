# /Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env

ARG SAM_BUILD_MODE="run"
ARG GPR_TOKEN
ARG GPR_USER

WORKDIR /app

RUN if [ "$SAM_BUILD_MODE" == "debug" ]; \
    then export configuration=Debug; \
    else export configuration=Release; \
    fi

RUN dotnet nuget add source "https://nuget.pkg.github.com/EpitomeGlobal/index.json" -p $GPR_TOKEN -u $GPR_USER --store-password-in-clear-text
WORKDIR /app/backend

# Copy csproj and restore as distinct layers
COPY  *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish \
        --configuration Release \
        --runtime linux-x64 \
        --self-contained false \
        --output out \
         -p:PublishReadyToRun=true


FROM public.ecr.aws/lambda/dotnet:5.0
WORKDIR /var/task
COPY --from=build-env /app/backend/out .

# Build runtime image
# FROM mcr.microsoft.com/dotnet/aspnet:5.0
# WORKDIR /app
# COPY --from=build-env /app/backend/out .

# ENTRYPOINT ["dotnet", "Coursepad.Api.dll"]

ARG appsettings
# Stage 1 - Generar la app y descargar dependencias
#Cambiar version de netcore
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

ARG appsettings

ENV HTTPS_PROXY http://proxycentral:8080
ENV HTTP_PROXY http://proxycentral:8080

WORKDIR /app

COPY *.csproj ./
COPY . ./

RUN dotnet restore

# Stage 2 - Generar publish  
FROM build-env AS publish 
 
RUN dotnet publish -c Release -o /app/publish


# Stage 3 - Publicar app y general imagen final
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime


EXPOSE 80
EXPOSE 443

ENV TZ=America/Buenos_Aires
#Cambiar por puerto propio
ENV ASPNETCORE_URLS http://+:5035
RUN echo $appsettings
ENV ASPNETCORE_ENVIRONMENT $appsettings

WORKDIR /app
COPY app.version ./ 
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "track3-api-reportes-core.dll"]
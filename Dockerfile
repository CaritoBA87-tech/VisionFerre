# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar archivos y restaurar
COPY . .
RUN dotnet restore "./AulaDiser.Proyecto.API/AulaDiser.Proyecto.API.csproj"

# Publicar la aplicación
RUN dotnet publish "./AulaDiser.Proyecto.API/AulaDiser.Proyecto.API.csproj" -c Release -o /out

# Etapa final (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .

# Configurar el puerto para Railway
ENV ASPNETCORE_URLS=http://+:3000
ENV ASPNETCORE_ENVIRONMENT=Development

EXPOSE 3000
ENTRYPOINT ["dotnet", "AulaDiser.Proyecto.API.dll"]
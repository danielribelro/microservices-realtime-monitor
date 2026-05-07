# Estágio de Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia tudo para o container
COPY . .

# Restaura e publica o projeto ESPECIFICANDO o arquivo .csproj
# (Ajustei para o nome que aparece na sua pasta)
RUN dotnet restore "MicroservicesSimulator.csproj"
RUN dotnet publish "MicroservicesSimulator.csproj" -c Release -o /app/publish

# Estágio de Execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Variáveis de ambiente para o Render
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "MicroservicesSimulator.dll"]
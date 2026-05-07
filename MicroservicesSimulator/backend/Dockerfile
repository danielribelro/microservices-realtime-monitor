# Estágio de Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia tudo para o container
COPY . .

# Restaura e publica o projeto apontando para o arquivo de projeto atual
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Estágio de Execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Variáveis de ambiente para o Render
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

# O comando final tenta rodar o executável principal
# Se o seu projeto mudar de nome, o "*" ajuda a encontrar a DLL correta
ENTRYPOINT ["sh", "-c", "dotnet $(ls *.dll | head -n 1)"]
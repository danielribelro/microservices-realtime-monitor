# Estágio de Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia TUDO da pasta atual para dentro do container
COPY . ./

# Restaura as dependências focando no arquivo de projeto
RUN dotnet restore "MicroservicesSimulator.csproj"

# Compila o projeto
RUN dotnet publish "MicroservicesSimulator.csproj" -c Release -o out

# Estágio de Execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copia apenas o resultado da compilação do estágio anterior
COPY --from=build /app/out .

# Configurações de porta para o Render
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "MicroservicesSimulator.dll"]
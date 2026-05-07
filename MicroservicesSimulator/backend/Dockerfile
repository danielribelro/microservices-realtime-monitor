# ESTÁGIO 1: Build (Onde a mágica da compilação acontece)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# 1. Copia os arquivos da sua pasta backend para dentro do container
COPY . ./

# 2. Restaura as dependências (Aqui ele baixa os pacotes NuGet)
RUN dotnet restore "MicroservicesSimulator.csproj"

# 3. Publica o projeto (Cria os arquivos prontos para rodar)
RUN dotnet publish "MicroservicesSimulator.csproj" -c Release -o out

# ESTÁGIO 2: Execução (Onde o servidor realmente roda)
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# 4. Pega apenas o que foi compilado no estágio anterior (fica bem leve)
COPY --from=build /app/out .

# 5. Configurações essenciais para o Render
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

# 6. Inicia a DLL do seu projeto
ENTRYPOINT ["dotnet", "MicroservicesSimulator.dll"]
# PokemonKotas

PokemonKotas é um projeto que simula uma Pokédex, permitindo a captura, listagem e gerenciamento de Pokémons e seus mestres.

## Tecnologias Utilizadas

- **Linguagem**: C#
- **Framework**: .NET 9 (preview)
- **Banco de Dados**: SQLite
- **Front-end**: Blazor WebAssembly
- **Back-end**: ASP.NET Core API
- **Testes**: xUnit, Moq, bUnit
- **Outras Bibliotecas**: 
1. Entity Framework Core
	- Code first
2. Blazored SessionStorage
	- Para armazenamento do Id do mestre pokémon no Session Storage
3. StrawberryShake
	- Para consulta GraphQL do pokeapi.co < decidi utilizar essa tecnologia para evitar fazer vários requests para conseguir as informações
4. Microsoft.Extensions.Caching.Memory
	- Adicionei cache in-memory para evitar consumir muitos recursos durante a consulta, então faço um fetch de todos os 151 pokémons e consulto somente em cache após a primeira requisição.
5. Scalar
	- Provavelmente será o novo substituto do swagger..

Utilizei DDD e compartilhei as bibliotecas entre o projeto do front-end e o projeto do back-end.

## Instalação e Uso

### Pré-requisitos

- .NET 9 SDK
- Visual Studio 2022 ou superior

### Passos para Instalação

1. **Clone o repositório**:

```
git clone https://github.com/seu-usuario/pokemonkotas.git
cd pokemonkotas
```

2. **Restaure as dependências**:
```
dotnet restore
```

3. **Configure o banco de dados** (opcional):
    - Navegue até o projeto `PokemonKotas.Data` e aplique as migrações:
```
cd PokemonKotas.Data
dotnet ef database update
```

### Executando o Projeto

1. **Inicie a API**:
```
cd ../PokemonKotas.Api
dotnet run
```
2. **Inicie o Front-end**:
```
cd ../PokemonKotas.Web
dotnet run
```
3. **Acesse a aplicação**:
    - Abra o navegador e acesse `http://localhost:4040` ou `https://localhost:4041`.

### Executando os Testes

1. **Navegue até o projeto de testes**:
```
cd ../PokemonKotas.Tests
```
2. **Execute os testes**:
```
dotnet test
```
### Aplicação em execução
Fiz deploy de demonstração, vou deixar ativos por alguns dias.
https://front-pokemon.deiro.dev.br/
https://backend-pokemon.deiro.dev.br/scalar/v1

That's all folks!

>This is a challenge by [Coodesh](https://coodesh.com/)
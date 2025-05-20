# Lar.Connections

Sistema para gerenciamento de contatos pessoais e familiares desenvolvido com .NET 8, seguindo princípios de Clean Architecture, Domain-Driven Design e CQRS.

## 📋 Sobre o Projeto

Lar.Connections é uma API RESTful robusta para gerenciar contatos pessoais e familiares, oferecendo funcionalidades completas de CRUD para pessoas e seus telefones. O projeto implementa padrões modernos de desenvolvimento como CQRS (Command Query Responsibility Segregation), validações com FluentValidation e uma arquitetura bem estruturada em camadas.

## 🏗️ Arquitetura

O projeto segue uma arquitetura limpa e bem organizada em 4 camadas:

```
src/
├── Lar.Connections.Application/     # Camada de aplicação (CQRS, Handlers, Validators)
├── Lar.Connections.Domain/          # Camada de domínio (Entidades, Enums, Rules)
├── Lar.Connections.Infrastructure/  # Camada de infraestrutura (Dados, Repositórios)
└── Lar.Connections.WebApi/          # Camada de apresentação (Controllers, API)
```

### 🎯 Funcionalidades Implementadas

#### 👤 Gestão de Pessoas
- ✅ Criar nova pessoa com telefones
- ✅ Buscar pessoas com paginação e filtros
- ✅ Buscar pessoa por ID ou documento
- ✅ Atualizar informações pessoais
- ✅ Excluir pessoas do sistema
- ✅ Pesquisa por termo (nome/documento)
- ✅ Ordenação personalizável
- ✅ Inclusão/exclusão de pessoas e contatos

#### 📱 Gestão de Telefones
- ✅ Adicionar até 5 telefones por pessoa
- ✅ Tipos: Residencial, Móvel, Comercial
- ✅ Validação de formato de telefone
- ✅ Consultar telefones por pessoa

#### 🔍 Funcionalidades Avançadas
- ✅ Paginação eficiente com contagem total
- ✅ Filtros por status (ativo/inativo)
- ✅ Busca por documento
- ✅ Validações de domínio rigorosas

## 🛠️ Tecnologias Utilizadas

### Backend & Framework
- **.NET 8.0** - Framework principal
- **C#** - Linguagem de programação
- **ASP.NET Core** - API Web framework

### Banco de Dados
- **SQL Server** - Banco de dados relacional
- **Dapper** - Micro ORM para performance otimizada
- **Scripts SQL** - Criação automática de tabelas

### Padrões e Bibliotecas
- **MediatR** - Implementação do padrão Mediator para CQRS
- **FluentValidation** - Validações fluentes e expressivas
- **Serilog** - Logging estruturado e flexível

### DevOps & Containerização
- **Docker** - Containerização da aplicação
- **Swagger/OpenAPI** - Documentação automática da API

### Padrões Arquiteturais
- **Clean Architecture** - Separação clara de responsabilidades
- **CQRS** - Command Query Responsibility Segregation
- **Domain-Driven Design (DDD)** - Modelagem orientada ao domínio
- **Repository Pattern** - Abstração da camada de dados
- **Dependency Injection** - Inversão de controle
- **Result Pattern** - Tratamento consistente de retornos

## 📋 Pré-requisitos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download) ou superior
- [SQL Server](https://www.microsoft.com/sql-server) (LocalDB ou instância completa)
- [Docker](https://www.docker.com/) (opcional, para containerização)
- [Git](https://git-scm.com/)

## 🔧 Instalação e Configuração

### 1. Clone o repositório
```bash
git clone https://github.com/diegolobo/Lar.Connections.git
cd Lar.Connections
```

### 2. Configure a connection string
Edite o arquivo `src/Lar.Connections.WebApi/appsettings.Development.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=LarConnectionsDB;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

### 3. Execute a aplicação
```bash
cd src/Lar.Connections.WebApi
dotnet restore
dotnet run
```

### 4. Acesse a aplicação
- **API**: `https://localhost:7063` ou `http://localhost:5273`
- **Swagger UI**: `https://localhost:7063/swagger`

> **Nota**: As tabelas do banco de dados são criadas automaticamente na primeira execução.

## 🐳 Executando com Docker

### Build da imagem:
```bash
docker build -t lar-connections .
```

### Executar container:
```bash
docker run -p 8080:8080 -p 8081:8081 lar-connections
```

Acesse: `http://localhost:8080/swagger`

## 📁 Estrutura Detalhada do Projeto

```
Lar.Connections/
├── src/
│   ├── Lar.Connections.Application/
│   │   ├── Behaviors/                    # Pipeline behaviors (validação)
│   │   ├── UseCases/
│   │   │   └── People/                   # Casos de uso para pessoas
│   │   │       ├── ExcludePerson/        # Excluir pessoa
│   │   │       ├── GetPeople/            # Buscar pessoas
│   │   │       ├── NewPerson/            # Criar pessoa
│   │   │       └── UpdatePerson/         # Atualizar pessoa
│   │   └── DependencyInjection.cs
│   │
│   ├── Lar.Connections.Domain/
│   │   ├── Common/                       # Classes base e padrões
│   │   │   ├── Enums/                    # Status de resultados
│   │   │   ├── Exceptions/               # Exceções customizadas
│   │   │   ├── Interfaces/               # Contratos base
│   │   │   └── Records/                  # Records para validações/erros
│   │   ├── Entities/                     # Entidades do domínio
│   │   │   ├── Person.cs                 # Entidade pessoa
│   │   │   └── Phone.cs                  # Entidade telefone
│   │   └── Enums/
│   │       └── PhoneType.cs              # Tipos de telefone
│   │
│   ├── Lar.Connections.Infrastructure/
│   │   ├── Constants/                    # Constantes de infraestrutura
│   │   ├── Queries/                      # Queries SQL otimizadas
│   │   ├── Repositories/                 # Implementações de repositórios
│   │   ├── Scripts/                      # Scripts de criação de tabelas
│   │   ├── Seeds/                        # Inicialização do banco
│   │   └── Utilities/                    # Utilitários (carregamento de scripts)
│   │
│   └── Lar.Connections.WebApi/
│       ├── Constants/                    # Constantes da API
│       ├── Controllers/
│       │   ├── Base/                     # Controller base com helpers
│       │   └── PersonController.cs       # Controller de pessoas
│       ├── Properties/                   # Configurações de launch
│       ├── Dockerfile                    # Configuração Docker
│       ├── Program.cs                    # Configuração da aplicação
│       └── appsettings.json             # Configurações da aplicação
│
└── tests/
    └── Lar.Connections.Tests/           # Projeto de testes (xUnit)
```

## 🧪 Testes

Para executar os testes:

```bash
cd tests/Lar.Connections.Tests
dotnet test
```

## 📚 API Endpoints

### 👤 Gestão de Pessoas

| Método | Endpoint | Descrição | Parâmetros |
|--------|----------|-----------|------------|
| `GET` | `/api/Person` | Lista pessoas com paginação | `page`, `pageSize`, `searchTerm`, `includeInactive`, `includePhones`, `sortBy`, `sortDescending` |
| `GET` | `/api/Person/{id}` | Busca pessoa por ID | `includePhones` |
| `GET` | `/api/Person/by-document/{document}` | Busca pessoa por documento | `includePhones` |
| `GET` | `/api/Person/search/{term}` | Pesquisa pessoas por termo | `page`, `pageSize`, `includeInactive`, `includePhones` |
| `POST` | `/api/Person` | Cria nova pessoa | Body: `NewPersonCommand` |
| `PUT` | `/api/Person/{id}` | Atualiza pessoa existente | Body: `UpdatePersonCommand` |
| `DELETE` | `/api/Person/{id}` | Remove pessoa | - |

### 📋 Exemplos de Uso

#### Criar pessoa com telefones:
```json
POST /api/Person
{
  "name": "João Silva",
  "document": "12345678901",
  "birthDate": "1990-05-15T00:00:00",
  "phones": [
    {
      "type": 2,
      "number": "(11) 99999-9999"
    },
    {
      "type": 1,
      "number": "(11) 3333-3333"
    }
  ]
}
```

#### Buscar pessoas com paginação:
```
GET /api/Person?page=1&pageSize=10&searchTerm=silva&includePhones=true&sortBy=Name&sortDescending=false
```

## 🔧 Configurações Avançadas

### Logging (Serilog)
O projeto utiliza Serilog para logging estruturado:
- Console output em desenvolvimento
- Enriquecimento com informações de máquina, thread e ambiente
- Configuração via `appsettings.json`

### Validações
- **FluentValidation** para validações de entrada
- **ValidationBehavior** no pipeline do MediatR
- Validações de domínio nas entidades
- Tratamento consistente de erros com **Result Pattern**

### Performance
- **Dapper** para consultas SQL otimizadas
- Queries específicas para diferentes cenários (com/sem telefones)
- Paginação eficiente com CTEs
- Índices otimizados no banco de dados


## 👨‍💻 Autor

**Diego Lobo**
- GitHub: [@diegolobo](https://github.com/diegolobo)
- LinkedIn: [Diego Lobo](https://www.linkedin.com/in/lobodiego)

## 🔄 Próximas Funcionalidades

- [ ] Autenticação e autorização (JWT)
- [ ] Relacionamentos entre pessoas
- [ ] Notificações por email
- [ ] API de importação/exportação
- [ ] Cache com Redis
- [ ] Testes de integração

---

## 🏆 Características Técnicas Destacadas

### ✨ Clean Architecture
- **Separação clara** entre camadas
- **Inversão de dependências** bem implementada
- **Testabilidade** aprimorada

### 🎯 CQRS Pattern
- **Commands** para operações de escrita
- **Queries** para operações de leitura
- **Handlers** específicos para cada operação

### 🛡️ Robustez
- **Result Pattern** para tratamento de erros
- **Validações rigorosas** em múltiplas camadas
- **Logging estruturado** com Serilog

### ⚡ Performance
- **Dapper** para consultas SQL otimizadas
- **Paginação eficiente** com CTEs
- **Lazy loading** de relacionamentos

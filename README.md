# 
# To-do-List

Este projeto é uma aplicação **ASP.NET Core Web API** que permite gerenciar usuários e tarefas. Ele utiliza o **Entity Framework** com **PostgreSQL** para persistência de dados e oferece endpoints para operações CRUD e autenticação de usuários.

## Requisitos Atendidos
 **Modelos de Dados:**
   - **Usuário:**
     - `Id` (UUID, chave primária)
     - `Username` (string, obrigatório)
     - `PasswordHash` (string, obrigatório)
     - `CreatedAt` (DateTime, automático)
   - **Tarefas:**
     - `Id` (UUID, chave primária)
     - `Title` (string, obrigatório)
     - `Description` (string, opcional)
     - `IsCompleted` (bool, padrão: false)
     - `Category` (string, obrigatório)
     - `CreatedAt` (DateTime, automático)
     - `UpdatedAt` (DateTime, automático)
     - `UserId` (UUID, chave estrangeira para Usuários)
 **Endpoints CRUD para Tarefas:**
   - Criação, leitura, atualização e exclusão de tarefas.
   - Filtros adicionais:
     - Filtrar por **categoria**.
     - Filtrar por **usuário**.
 **Endpoints para Autenticação:**
   - Registro de novos usuários.
   - Login para autenticação com validação de credenciais.
 **Validações:**
   - Validações aplicadas aos modelos de dados e endpoints para garantir consistência.

## Tecnologias Utilizadas

- **ASP.NET Core 8**
- **Entity Framework Core**
- **PostgreSQL**
- **JWT Authentication**
- **Xunit**

### Endpoints Disponíveis

#### Usuários

- **POST /users/register**: Registrar um novo usuário.
- **POST /users/login**: Fazer login de usuário.

#### Tarefas

- **GET /tasks**: Listar todas as tarefas.
  - Parâmetros de filtro opcionais: `category`, `userId`.
- **GET /tasks/{id}**: Obter uma tarefa específica.
- **POST /tasks**: Criar uma nova tarefa.
- **PUT /tasks/{id}**: Atualizar uma tarefa existente.
- **DELETE /tasks/{id}**: Excluir uma tarefa.

### Estrutura de Diretórios
```
📁 src
├── 📁 TaskManager.API
├── 📁 TaskManager.Domain
├── 📁 TaskManager.Infra
├── 📁 TaskManager.Application
📁 tests
├── 📁 TaskManager.e2eTest
├── 📁 TaskManager.integrationTest
├── 📁 TaskManager.unitTest

```

### Estrutura de Camadas
```
📁 TaskManager.API
├── 📁 Controllers 
├── 📁 FilterException //Filtragem e validação de todas exceções do projeto

📁 TaskManager.Domain
├── 📁 TaskManager.e2eTest

📁 TaskManager.Infra
├── 📁 TaskManager.e2eTest

📁 TaskManager.Application
├── 📁 TaskManager.e2eTest



```


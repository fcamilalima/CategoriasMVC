# CategoriasMVC

## Descrição

O **CAtegoriasMVC** é uma API desenvolvida em ASP.NET Core (.NET 8) com Razor Pages para gerenciamento de produtos e categorias, apesar do nome. O projeto implementa boas práticas como autenticação JWT, MVC, EntityFrameworkCore.

---

## Funcionalidades

- CRUD de produtos e categorias
- Autenticação e autorização com JWT

---

## Tecnologias Utilizadas

- .NET 8
- ASP.NET Core Razor Pages
- Entity Framework Core (MySQL)
- JWT (Json Web Token)
- Swagger/OpenAPI

---

## Configuração

### 1. Clonar o repositório
```
git@github.com:fcamilalima/CategoriasMVC.git
```

### 2. Configurar o banco de dados

Edite a string de conexão em `appsettings.json`:

### 3. Aplicar as migrações

```
dotnet ef database update
```

### 4. Executar o projeto

```
 dotnet run
```

## Autenticação

A API utiliza autenticação JWT. Para obter um token, utilize o endpoint:

```
POST /api/v1/Auth/login
```

---

## Contribuição

1. Faça um fork do projeto          
2. Crie uma branch: `git checkout -b minha-feature`
3. Commit suas alterações: `git commit -m 'Minha nova feature'`
4. Push para o branch: `git push origin minha-feature`
5. Abra um Pull Request

---

## Contato
- **Autor**: Camila Lima
- **Email**: fcamilalima@gmail.com

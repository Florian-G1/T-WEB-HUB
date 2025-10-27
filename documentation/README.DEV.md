# 🧰 Developer Guide – .NET 9 REST API & Vue 3 Client (Vite + pnpm)

## 📦 Project Structure

```
/src
  /Api                → ASP.NET Core (.NET 9) entry point
  /Application        → Business logic, use cases
  /Domain             → Entities and domain interfaces
  /Infrastructure     → Data access, external services
/client
  /src
    /components       → Vue 3 components
    /views            → Pages
    /store            → Pinia stores
    /api              → API calls to backend
```

---

## 🧱 Naming Conventions

### C# / .NET 9

| Element                  | Convention                           | Example                                      |
| ------------------------ | ------------------------------------ | -------------------------------------------- |
| **Classes / Interfaces** | PascalCase                           | `UserService`, `IUserRepository`             |
| **Methods**              | PascalCase                           | `GetUserById()`                              |
| **Properties**           | PascalCase                           | `FirstName`, `CreatedAt`                     |
| **Local variables**      | camelCase                            | `userList`, `totalCount`                     |
| **Constants**            | PascalCase                           | `DefaultPageSize = 10;`                      |
| **Namespaces**           | PascalCase                           | `MyApp.Infrastructure.Persistence`           |
| **Files**                | Same as main class                   | `UserService.cs`                             |
| **DTOs / Records**       | PascalCase + suffix `Dto` or `Model` | `UserDto`, `LoginModel`                      |
| **Unit tests**           | `ClassName_Tests.cs`                 | `UserService_Tests.cs`                       |
| **Git branches**         | kebab-case                           | `feature/add-user-endpoint`, `fix/login-bug` |

### Vue 3 / TypeScript / Vite

| Element                   | Convention       | Example                             |
| ------------------------- | ---------------- | ----------------------------------- |
| **Components**            | PascalCase       | `UserCard.vue`                      |
| **Folders**               | kebab-case       | `user-profile/`, `auth/`            |
| **Files (JS/TS)**         | kebab-case       | `use-user-store.ts`, `auth-api.ts`  |
| **Variables / functions** | camelCase        | `getUserById()`, `userList`         |
| **Constants**             | UPPER_SNAKE_CASE | `API_BASE_URL`                      |
| **Props / Emits**         | camelCase        | `userId`, `onUserSelect`            |
| **Stores (Pinia)**        | `useXxxStore`    | `useUserStore()`                    |
| **Routes**                | kebab-case       | `/user-profile`, `/admin-dashboard` |
| **CSS classes**           | kebab-case       | `.user-card`, `.main-header`        |

---

## ⚙️ Useful Commands

### Backend (.NET 9)

#### 📦 Initialization

```bash
dotnet restore
```

#### ▶️ Run API

```bash
dotnet run --project src/Api
```

#### 🧪 Run tests

```bash
dotnet test
```

#### 🧰 Development tools

```bash
dotnet watch run              # Hot reload
dotnet ef migrations add <Name> --project src/Infrastructure
dotnet ef database update     # Apply migrations
```

#### 🧹 Clean & rebuild

```bash
dotnet clean
dotnet build
```

---

### Frontend (Vite + Vue 3 + pnpm)

#### 📦 Install dependencies

```bash
pnpm install
```

#### ▶️ Start dev server

```bash
pnpm dev
```

#### 🧱 Production build

```bash
pnpm build
```

#### 🔍 Lint & format

```bash
pnpm lint
pnpm format
```

#### 🧪 Run unit tests

```bash
pnpm test
```

---

## ⚙️ Environment Setup

### Backend

Create a `.env` or `appsettings.Development.json` file with the following keys:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=AppDb;Username=app;Password=secret"
  },
  "Jwt": {
    "Key": "your_secret_key_here",
    "Issuer": "MyApp"
  }
}
```

### Frontend

Create a `.env` file in `/client`:

```
VITE_API_BASE_URL=http://localhost:5000/api
```

---

## 🧩 Best Practices

### Backend

- Follow **Clean Architecture** principles (Domain → Application → Infrastructure → Api).
- Use **CQRS + Mediator (MediatR)** for commands and queries.
- Validate input with **FluentValidation**.
- Document endpoints using **Swagger / Swashbuckle**.
- Use DTOs for all API inputs and outputs.

### Frontend

- Use **Composition API** (`<script setup>`).
- Centralize API calls in `/src/api`.
- Manage state globally with **Pinia**.
- Use TypeScript strict mode (`"strict": true`).
- Apply consistent formatting (ESLint + Prettier).

---

## 🧭 Git Guidelines

| Branch Type | Format          | Example             |
| ----------- | --------------- | ------------------- |
| Feature     | `feat/<name>`   | `feat/add-auth`     |
| Fix         | `fix/<name>`    | `fix/user-login`    |
| Hotfix      | `hotfix/<name>` | `hotfix/prod-error` |

### Types et Scopes

Complite ref : [Conventional Commits Guide](https://gist.github.com/qoomon/5dfcdf8eec66a051ecd85625518cfd13)

#### Types principaux

- `feat` - New feature
- `fix` - Fix a bug
- `docs` - Documentation
- `style` - Formate, missing ";", etc.
- `refactor` - Change logic without breacking test
- `test` - Add or change tests
- `chore` - Maintenace tasks

#### Exemples

```
feat(auth): add login form component
fix(user): resolve null pointer exception in getUserById
docs(readme): update installation instructions
style(button): improve hover state styling
refactor(service): extract common logic to base class
```

### Rules

- Direct commit or push on main and dev are prohibit
- Push on dev can be done with Pull Request from any branch
- Push on main can be done with Pull Request from dev or hotfix/\* branch
- Pull Request must pass the ci before merging
- Pull Request must be approuved by at least 1 other team member if it's possible

---

## 🧾 Resources

- [.NET 9 Documentation](https://learn.microsoft.com/dotnet/)
- [Vue 3 Docs](https://vuejs.org/guide/introduction.html)
- [Vite Documentation](https://vitejs.dev/)
- [Pinia Docs](https://pinia.vuejs.org/)
- [Conventional Commits](https://www.conventionalcommits.org/)
- [Clean Architecture – .NET](https://jasontaylor.dev/clean-architecture-getting-started/)

---

📘 **Internal Developer Documentation**  
For installation or user setup, see `README.md`.

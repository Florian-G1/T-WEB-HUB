# 🛍️ Gunpla_600

**Gunpla_600** est une application e-commerce de petite envergure développée avec une architecture propre et modulaire.  
Le projet est composé d’un **back-end en C# (ASP.NET Core)** suivant le principe de la **Clean Architecture**, et d’un **front-end en Vue.js** pour l’interface utilisateur.

---

## 🧱 Structure du projet

```
Gunpla_600/
│
├── server/           # Back-end ASP.NET Core (API REST)
│   ├── Gunpla.Api/              # Projet principal (controllers, endpoints)
│   ├── Gunpla.Application/      # Logique métier, services, DTOs
│   ├── Gunpla.Domain/           # Entités et modèles du domaine
│   ├── Gunpla.Infrastructure/   # Accès aux données, implémentations externes
│   └── Gunpla.Tests/            # Tests unitaires et d’intégration
│
└── client/           # Front-end Vue.js (interface utilisateur)
    ├── src/
    │   ├── assets/               # Images, icônes, styles
    │   ├── components/           # Composants réutilisables Vue
    │   ├── pages/                # Pages principales (Home, Shop, Cart, etc.)
    │   ├── router/               # Routes Vue Router
    │   ├── store/                # Gestion d’état (Pinia ou Vuex)
    │   ├── services/             # Appels API (vers le backend)
    │   └── App.vue / main.js     # Point d’entrée de l’app
    ├── public/
    ├── package.json
    └── vite.config.js            # Config du build Vue (Vite)
```

---

## 🚀 Démarrage rapide

### 1️⃣ Cloner le dépôt

```bash
git clone https://github.com/ton-utilisateur/Gunpla_600.git
cd Gunpla_600
```

---

### **Avec docker**

### 2️⃣ Lancer le projet

#### 📦 Prérequis

- [Docker]
- [Docker compose]
- [Taskfile] (optionnel)

#### Lancer le projet

**Avec Taskfile**

```bash
task dev-build
```

**Sans Taskfile**

```bash
docker compose -f docker-compose.dev.yml up -d
```

### **Sans docker**

### 2️⃣ Lancer le **back-end**

#### 📦 Prérequis

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server (ou SQLite pour le dev)

#### ⚙️ Lancer l’API

```bash
cd server/Gunpla_600.Api
dotnet restore
dotnet run
```

> L’API sera disponible sur `https://localhost:5001` ou `http://localhost:5000`.

---

### 3️⃣ Lancer le **front-end**

#### 📦 Prérequis

- [Node.js](https://nodejs.org/) (v18+ recommandé)
- [npm](https://www.npmjs.com/) ou [pnpm](https://pnpm.io/)

#### ⚙️ Démarrer le client

```bash
cd client
pnpm install
pnpm run dev
```

> L’application sera disponible sur `http://localhost:5173`.

---

## 🧩 Stack technique

### Back-end

- **C# / ASP.NET Core 8**
- **Entity Framework Core** (accès aux données)
- **Clean Architecture** (Domain, Application, Infrastructure, API)
- **JWT Authentication**
- **Swagger** (documentation de l’API)

### Front-end

- **Vue 3** (Composition API)
- **Vite** (bundler ultra rapide)
- **Pinia** (store management)
- **Vue Router**
- **Axios** (consommation de l’API)
- **TailwindCSS** (design et responsive)

---

## 🧠 Objectif du projet

Créer une application e-commerce simple mais modulaire, permettant :

- La consultation de produits.
- L’ajout d’articles au panier.
- La gestion d’un compte utilisateur.
- Le paiement (Stripe ou autre).
- Un panneau d’administration basique.

---

## 🧑‍💻 Auteur

**Gunpla_600**  
Développé par [Florian GEHIN]

---

## ⚖️ Licence

## Ce projet n'est pour l'instant pas sous licence

### ⭐ Astuce

> Pour une meilleure intégration, configure le **CORS** dans ton back-end (`Program.cs`) :

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClient",
        policy => policy.WithOrigins("http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});
```

Et active-le :

```csharp
app.UseCors("AllowClient");
```

---

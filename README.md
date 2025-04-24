# ✅ TasneemSami ToDoList API

This is a clean architecture ASP.NET Core Web API project for managing tasks using a layered approach.  
It supports JWT authentication and authorization with Admin/User roles, Entity Framework Core, and SQL Server integration.

---

## 🧱 Architecture

The project is organized into three main layers:

- **TasneemSami.ToDoList.Api**: The API layer (Presentation)
- **TasneemSami.ToDoList.Services**: Business logic and services
- **TasneemSami.ToDoList.Database**: Data access using Entity Framework Core

---

## 🚀 Features

- ✅ JWT Authentication & Role-based Authorization
- ✅ Admin can Add/Edit/Delete tasks
- ✅ Users can only view tasks
- ✅ Entity Framework Core with Code-First Migrations
- ✅ Unit Testing with Moq
- ✅ Swagger UI for API testing
- ✅ Docker support (API + SQL Server)

---

## 🧪 Run Locally

### 🔧 Prerequisites
- [.NET SDK 6.0+](https://dotnet.microsoft.com/)
- [Docker](https://www.docker.com/)
- [Visual Studio or VS Code](https://visualstudio.microsoft.com/)

### ▶️ Steps

```bash
# Clone the repository
git clone https://github.com/TASNEEM304/ToDoList.git
cd ToDoList

# Run the project using Docker
docker-compose up --build
///////////////////////////////////////////////////////////////
Technologies Used
ASP.NET Core Web API

Entity Framework Core

JWT Authentication

xUnit + Moq

SQL Server

Docker + Docker Compose

Swagger 
/////////////////////////////////////////////////////////////////////

ToDoList/
├── TasneemSami.ToDoList.Api/          # API controllers & startup
├── TasneemSami.ToDoList.Services/     # Business logic & interfaces
├── TasneemSami.ToDoList.Database/     # EF Core context and migrations
├── TasneemSami.ToDoList.Tests/        # Unit tests
├── docker-compose.yml                 # Docker config
└── README.md
/////////////////////////////////////////////////////////////////////
👤 Author
Tasneem Sami
////////////////////////////////////////////////////////////////////





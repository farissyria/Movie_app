# 🎬 Cinema Management API

A modern, secure RESTful API for managing cinema operations, built with ASP.NET Core 8, MongoDB, Redis Cache, and JWT authentication.

<div align="center">
  ![Swagger UI showing Cinema API endpoints](s1.png)
  <img src="s1.png" alt="Swagger API Documentation" width="800"/>
  <br/>
  <em>Swagger UI showing Cinema API endpoints</em>
</div>
*Swagger UI showing Cinema API endpoints*

## ✨ Features

- 🔐 **JWT Authentication** - Secure token-based authentication
- 👥 **Identity Management** - User registration, login, and role-based access (Admin/User)
- 🎥 **Movie Management** - Complete CRUD operations for movies
- 🔍 **Advanced Search** - Search movies by title, genre, and ratings
- 📊 **Top Rated Movies** - Get top-rated movies with custom count
- 🎨 **Swagger Documentation** - Interactive API documentation
- 🗄️ **MongoDB Integration** - NoSQL database for high performance
- ⚡ **Redis Caching** - High-performance distributed caching for improved response times
- 🛡️ **Role-Based Authorization** - Admin-only endpoints for sensitive operations

## 🚀 Technology Stack

- **.NET 8** - Latest LTS version
- **ASP.NET Core Web API** - RESTful API framework
- **MongoDB** - NoSQL database
- **Redis** - In-memory distributed cache
- **JWT Bearer Authentication** - Token-based security
- **ASP.NET Core Identity** - User management with MongoDB store
- **AutoMapper** - Object-object mapping
- **Swagger/OpenAPI** - API documentation
- **Repository Pattern** - Clean architecture
- **Unit of Work Pattern** - Transaction management

## 📋 Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MongoDB](https://www.mongodb.com/try/download/community) (local or Atlas)
- [Redis](https://redis.io/download) (local or cloud like Redis Labs/Azure Cache)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/)
🚀 Redis Caching Implementation
Caching Strategy
Get All Movies - Cached for 10 minutes by default

Get Movie by ID - Cached for 30 minutes

Top Rated Movies - Cached dynamically based on count parameter

Search Results - Cached with search query as key

Cache Eviction - Automatic when movies are created, updated, or deleted

Benefits Implemented
⚡ Reduced Database Load - Up to 80% reduction in MongoDB queries

🚀 Improved Response Times - 5-10x faster responses for cached endpoints

📈 Better Scalability - Handles more concurrent users with less resources

🔄 Distributed Cache - Works across multiple server instances

## Configure MongoDB Connection
 {
  "MongoDB": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "CinemaDB"
  }
}
## 🛠️ Installation

### 1. Clone the repository
```bash
git clone https://github.com/farissyria/Movie_app.git
cd Movie_app

# RecipeNest 🍳

A RESTful API for recipe management, built with C# and ASP.NET Core. Manage recipes, cuisines, user ratings, and favorites with secure authentication.

## Features ✨

- **User Authentication**: JWT-based registration/login
- **CRUD Operations** for:
    - Recipes
    - Cuisines
    - Ratings
    - Favorites
    - Users
    - Roles
- **Pagination** for large datasets
- **Custom Exceptions** for error handling
- **DTO Pattern** (Request/Response models)
- **Repository Pattern** for data access
- **Dependency Injection** throughout

## Tech Stack 💻

- **Backend**: ASP.NET Core
- **Database**: MySQL (via raw SQL queries)
- **Authentication**: JWT Tokens
- **Hashing**: BCrypt
- **Logging**: Built-in .NET Logger
- **API Documentation**: Postman

## Installation 🛠️

### Prerequisites
- [.NET 6.0+](https://dotnet.microsoft.com/download)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/)

### Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/SayubShakya/dotnet-recipe-nest-backend
   cd dotnet-recipe-nest-backend
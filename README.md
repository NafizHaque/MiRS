# Mirs API

   

---

<p align="center">
  <img src="https://upload.wikimedia.org/wikipedia/commons/7/7d/Microsoft_.NET_logo.svg" height="200" />
     &nbsp;&nbsp;&nbsp;
  <img src="https://www.svgrepo.com/show/303229/microsoft-sql-server-logo.svg" height="200" />
     &nbsp;&nbsp;&nbsp;
  <img src="https://www.docker.com/wp-content/uploads/2022/03/Moby-logo.png" height="200" />
</p>
<br><br>
<p align="center">
  <img src="https://img.shields.io/badge/.NET-8.0-blueviolet" alt=".NET 8" />
  <img src="https://img.shields.io/badge/SQL%20Server-2022-red" alt="SQL Server 2022" />
  <img src="https://img.shields.io/badge/Azure-Ready-blue" alt="Docker" />
</p>

---

## Getting Started

Follow these steps to get your project running locally:

#### Configure database

```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_DB_SERVER;Database=YOUR_DB_NAME;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;"
  }
}
```

#### Install dependencies
```
dotnet restore
```

#### Github Actions CI/CD

- All pushes and pull requests trigger the GitHub Actions workflow.
- The workflow automatically builds, tests, and deploys your project.
- Make sure your repository secrets (like DB_CONNECTION_STRING) are set in GitHub for CI/CD.

---

## Completed

-

---

## TO-DO 

-Admin Section
  - AddUserToTeamInGuild Logic
  - Remove Al users from team logic
  - Delete team logic

-User 
  -Update user (my) details

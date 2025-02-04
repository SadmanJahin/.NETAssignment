# Users CRUD Application

This is a simple **Users CRUD** application built to manage user data. It allows users to perform Create, Read, Update, and Delete operations on user records.
In List View We can change data source and search from that data source.

## Features

- **Create**: Add new users to the system.
- **Read**: View the list of users or view details of a specific user.
- **Update**: Edit user details.
- **Delete**: Remove users from the system.
- **Dynamic Search Api**: Dynamic Search Api for Users.

## Tech Stack

- **Backend**: [.NET Core 8.0] 
- **Database**: [MSSQL & JSON] 
- **Frontend**: [Angular 19.0] 
  - **CSS Framework**: [Bootstrap, PrimeNG 19]

- **Other Libraries / Tools**:
  - **API Testing**: [Swagger / Postman]

## Installation

### 1. Clone the repository

```bash
git clone https://github.com/SadmanJahin/.NETAssignment
```
### 2. Setup  Frontend & Run

```bash
cd client
npm install
ng serve
```
### 2. Setup  Backend & Run
```bash
cd WebApi
dotnet restore
dotnet build
dotnet run
```

### 3. Publishing the Project
```bash
ng build --prod // frontend
dotnet publish -c Release -o publish //backend
```

### 4. Running on Docker

```
  cd WebApi/WebApi
  docker build -t image
  docker run -p 8080:80 image
```

## Database
To change database connection connection strings can be changed from appsettings.json. SQL Server connection can be provided.
After proper connection established, run command in the package manager console
```bash
 Update-Database
```

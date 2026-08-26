<div align="center">

[![English](https://img.shields.io/badge/English-4A90E2?style=for-the-badge&logoColor=white)](README.md)
[![Spanish](https://img.shields.io/badge/Spanish-FFDE59?style=for-the-badge&logoColor=white)](README_es.md)


# 🛒 Sales Management System

A full-stack Sales and Order Management Application designed to streamline transaction recording, sales history tracking, and itemized invoice details.

<!-- Tech Stack Badges -->
![.NET](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core_Web_API-5C2D91?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)
![Angular](https://img.shields.io/badge/angular-%23DD0031.svg?style=for-the-badge&logo=angular&logoColor=white)
![TypeScript](https://img.shields.io/badge/TypeScript-3178C6?style=for-the-badge&logo=typescript&logoColor=white)
![Node.js](https://img.shields.io/badge/node.js-6DA55F?style=for-the-badge&logo=node.js&logoColor=white)
![Microsoft SQL Server](https://img.shields.io/badge/Microsoft%20SQL%20Sever-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-7952B3?style=for-the-badge&logo=bootstrap&logoColor=white)

</div>

---

## 🚀 Overview

This application serves as a comprehensive point-of-sale and sales registry tool. It enables businesses to record customer transactions with real-time price calculations, manage multiple payment methods, paginate through transaction records, and inspect granular sale details.

---

## ✨ Features

- **Sales Processing:** Dynamic transaction entry with live subtotal/total calculations and validation guards.
- **Payment Method Handling:** Supports Cash, Card, and Bank Transfer with automated type mapping.
- **Server-Side Pagination:** High-performance data retrieval using SQL queries optimized with `AsNoTracking` and skip/take logic.
- **Detailed Audit View:** Inspect full itemized purchase breakdowns per sale ID.
- **API Documentation & Testing:** Integrated with **Scalar** for modern, OpenAPI-compliant endpoint exploration.
- **Unified API Response Standard:** Consistent generic wrapper (`ApiResponse<T>`) for clean error and success response formatting.

---

## 🛠️ Tech Stack

### Backend
- **Framework:** ASP.NET Core Web API (.NET 10)
- **Database ORM:** Entity Framework Core (SQL Server Provider)
- **Database:** Microsoft SQL Server / LocalDB
- **API Documentation:** Scalar API Reference / Microsoft OpenApi
- **Architecture:** Separation of Concerns (Controllers, Business Logic Layer, DTOs, Enums, Context)

### Frontend
- **Framework:** Angular (Signals, Standalone Components)
- **UI Framework:** Bootstrap 5 & NgBootstrap (Pagination)
- **Alerts & Modals:** SweetAlert2
- **State & Reactivity:** Angular Signals & RxJS Observables

---

## 🏗️ Architecture & Database Design

### Entity Relationship Model
- **`Sale` (Header):** `SaleId`, `CustomerName`, `PaymentType`, `Total`, `SaleDate`
- **`SaleDetail` (Items):** `SaleDetailId`, `SaleId` (FK), `ProductName`, `Quantity`, `UnitPrice`

---

## 🚦 Getting Started

### Prerequisites
- [.NET SDK (v10.0+) ↗](https://dotnet.microsoft.com/)
- [Node.js (LTS version) ↗](https://nodejs.org/)
- [Angular CLI ↗](https://angular.dev/)
- [SQL Server Express / LocalDB ↗](https://www.microsoft.com/sql-server/)
---

### Backend Setup

1. Navigate to the backend directory:
   ```bash
        cd src/backend/SalesWebApi
    ```

2. Configure your connection string in `appsettings.json`:
    ```json
        "ConnectionStrings": {
            "DefaultConnection": 
                "Server=localhost\\SQLEXPRESS;
                Database=SalesAppDb;
                Trusted_Connection=True;
                TrustServerCertificate=True;"
        }
    ```

3. Apply database migrations:
    ```bash
        dotnet ef database update
    ```

4. Run the API:
    ```bash
        dotnet run
    ```

*Access Scalar API Reference at `http://localhost:<port>/scalar*`

---

### Frontend Setup

1. Navigate to the frontend directory:
    ```bash
        cd src/frontend/sale-app
    ```

2. Install dependencies:
    ```bash
        npm install
    ```

3. Update the API URL in `src/environments/environment.development.ts`:
    ```typescript
        export const environment = {
            production: false,
            apiUrl: 'http://localhost:<YOUR_BACKEND_PORT>/api'
        };
    ```

4. Start the development server:
    ```bash
        ng serve -o
    ```

*Application runs at `http://localhost:4200*`

---

## 📌 API Endpoints

| Method | Endpoint | Description |
| --- | --- | --- |
| `POST` | `/api/sale` | Creates a new sale with item details |
| `GET` | `/api/sale?page={page}&pageSize={size}` | Retrieves paginated sales records |
| `GET` | `/api/sale/{id}` | Fetches complete details of a specific sale |

---

## 📄 License

This project is licensed under **All Rights Reserved** for portfolio demonstration purposes only. See the [LICENSE](LICENSE) file for details.


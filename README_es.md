<div align="center">

[![English](https://img.shields.io/badge/English-4A90E2?style=for-the-badge&logoColor=white)](README.md)
[![Spanish](https://img.shields.io/badge/Spanish-FFDE59?style=for-the-badge&logoColor=white)](README_es.md)

# 🛒 Sistema de Gestión de Ventas

Aplicación full-stack de gestión de ventas y pedidos diseñada para registrar transacciones, consultar historiales de ventas y auditar detalles de compras.

<!-- Badges de Tecnologías -->
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

## 🚀 Descripción General

Esta aplicación funciona como un punto de venta y registro integral de pedidos. Permite a los comercios registrar transacciones de clientes con cálculo dinámico de precios en tiempo real, gestionar múltiples métodos de pago, paginar listados de ventas en el servidor y consultar el desglose detallado de cada comprobante.

## ✨ Características Principales

- **Registro Dinámico de Ventas:** Formulario reactivo con cálculo automático de subtotales/totales y validaciones en tiempo real.
- **Gestión de Métodos de Pago:** Soporte para Efectivo, Tarjeta y Transferencia con mapeo mediante enumeraciones (`Enums`).
- **Paginación en Servidor:** Consultas SQL de alto rendimiento optimizadas mediante `AsNoTracking` y paginación con `Skip` y `Take`.
- **Vista de Detalle y Auditoría:** Consulta del desglose individualizado de productos comprados por cada ID de venta.
- **Documentación de API:** Integración con **Scalar** para exploración y pruebas interactivas de endpoints compatibles con OpenAPI.
- **Respuestas Estandarizadas:** Envoltorio genérico (`ApiResponse<T>`) para unificar la estructura de respuestas exitosas y de error.

---

## 🛠️ Stack Tecnológico

### Backend
- **Framework:** ASP.NET Core Web API (.NET 10)
- **ORM:** Entity Framework Core (Proveedor SQL Server)
- **Base de Datos:** Microsoft SQL Server / LocalDB
- **Documentación de API:** Scalar API Reference / Microsoft OpenApi
- **Arquitectura:** Separación de responsabilidades (Controllers, Business Logic Layer, DTOs, Enums, Context)

### Frontend
- **Framework:** Angular (Signals, Standalone Components)
- **Framework UI:** Bootstrap 5 y NgBootstrap (Paginación)
- **Alertas y Notificaciones:** SweetAlert2
- **Estado y Reactividad:** Angular Signals y RxJS Observables

---

## 🏗️ Arquitectura y Modelo de Datos

### Entidades y Relaciones
- **`Sale` (Cabecera):** `SaleId`, `CustomerName`, `PaymentType`, `Total`, `SaleDate`
- **`SaleDetail` (Líneas de detalle):** `SaleDetailId`, `SaleId` (FK), `ProductName`, `Quantity`, `UnitPrice`

---

## 🚦 Requisitos Previos e Instalación

### Requisitos Previos
- [.NET SDK (v10.0+) ↗](https://dotnet.microsoft.com/)
- [Node.js (Versión LTS) ↗](https://nodejs.org/)
- [Angular CLI ↗](https://angular.dev/)
- [SQL Server Express / LocalDB ↗](https://www.microsoft.com/sql-server/)

---

### Configuración del Backend

1. Navega hasta la carpeta del backend:
   ```bash
   cd Backend/SalesWebApi
    ```

2. Configura tu cadena de conexión en `appsettings.json`:
    ```json
        "ConnectionStrings": {
            "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=SalesAppDb;Trusted_Connection=True;TrustServerCertificate=True;"
        }
    ```

3. Aplica las migraciones de Entity Framework:
    ```bash
        dotnet ef database update
    ```

4. Inicia la API:
    ```bash
        dotnet run
    ```

Accede a la interfaz interactiva de Scalar en `http://localhost:5197/scalar` mientras la API se ejecute en Development.

---

### Configuración del Frontend

1. Navega hasta la carpeta del frontend:
    ```bash
        cd Frontend/sale-app
    ```

2. Instala las dependencias de Node:
    ```bash
        npm install
    ```


3. La URL base del backend ya está configurada en `src/environments/environment.development.ts`:
    ```typescript
        export const environment = {
            production: false,
            apiUrl: 'http://localhost:5197/api'
        };
    ```

4. Inicia el servidor de desarrollo:
    ```bash
        npm start
    ```

La aplicación estará disponible en `http://localhost:4200/`.

---

## 📌 Endpoints de la API

| Método | Endpoint | Descripción |
| --- | --- | --- |
| `POST` | `/api/sale` | Crea un nuevo registro de venta con sus detalles |
| `GET` | `/api/sale?page={page}&pageSize={size}` | Obtiene listado paginado de ventas |
| `GET` | `/api/sale/{id}` | Recupera el detalle completo de una venta específica |

---

## 🎥 Demo de la aplicación

[![Ver demo de la aplicación](https://img.youtube.com/vi/u2u2WNMh_lg/0.jpg)](https://youtu.be/u2u2WNMh_lg)

---

## 📄 Licencia

Este proyecto está registrado bajo **Todos los Derechos Reservados** con fines de demostración en portafolio profesional. Consulta el archivo [LICENSE](LICENSE) para más detalles.

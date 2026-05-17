# Global Logistics Management System (GLMS)

## Overview

The Global Logistics Management System (GLMS) is an ASP.NET Core MVC enterprise prototype developed for TechMove Logistics. The system centralises client management, contract management, service request processing, PDF document handling and currency conversion within a single monolithic application.

This project was developed as part of an academic enterprise software development assessment focusing on:

* ASP.NET Core MVC
* Entity Framework Core
* SQL Server
* External API Integration
* Workflow Validation
* Unit Testing using xUnit
* Enterprise Application Design

---

# Features

## Client Management

The system allows administrators to:

* Create clients
* Edit client details
* Delete clients
* Store client regions and contact details

## Contract Management

The contract module supports:

* Creating contracts linked to clients
* Contract status management
* Service Level assignment
* Start and End dates
* PDF agreement uploads
* Downloading signed agreements
* Contract filtering by date and status

## Service Request Management

The system allows:

* Creation of service requests linked to contracts
* Currency conversion from USD to ZAR
* Workflow validation rules
* Status tracking for requests

## Workflow Validation

The system enforces enterprise business rules:

* Service requests cannot be created for Expired contracts
* Service requests cannot be created for On Hold contracts
* Only Active contracts can process service requests

## Currency API Integration

GLMS integrates with an external Currency Exchange API using HttpClient and Async/Await.

Features include:

* Real-time USD to ZAR conversion
* Automatic local currency calculation
* Async API calls
* External API consumption

## File Handling

The system supports:

* PDF agreement uploads
* File storage on the server
* Downloadable contract agreements
* File validation
* Restricted file type protection

## Unit Testing

The project includes an xUnit Test Project covering:

* Currency conversion calculations
* PDF validation
* Restricted file validation (.exe)
* Edge-case testing

---

# Technologies Used

| Technology            | Purpose                     |
| --------------------- | --------------------------- |
| ASP.NET Core MVC      | Web application framework   |
| Entity Framework Core | ORM and database management |
| SQL Server            | Relational database         |
| Bootstrap 5           | Frontend styling            |
| Chart.js              | Dashboard analytics charts  |
| xUnit                 | Unit testing                |
| GitHub                | Version control             |
| HttpClient            | External API integration    |

---

# System Architecture

The application follows a monolithic architecture where:

* Presentation Layer handles UI and Views
* Business Logic Layer handles workflows and validation
* Data Access Layer communicates with SQL Server using EF Core

---

# Database Entities

## Client

* CompanyName
* Email
* PhoneNumber
* Address
* Region

## Contract

* ContractNumber
* StartDate
* EndDate
* Status
* ServiceLevel
* SignedAgreementFileName
* ClientId

## ServiceRequest

* RequestTitle
* Description
* RequestDate
* Status
* AmountUsd
* AmountZar
* ContractId

---

# Validation Rules

The following validations are implemented:

* Required field validation
* PDF-only uploads
* Restricted file type blocking
* Workflow validation for expired contracts
* Currency conversion validation

---

# Unit Testing

The xUnit Test Project validates:

* Correct currency calculations
* File extension validation
* Invalid file handling
* Edge-case scenarios

All tests pass successfully in Test Explorer.

---

# Setup Instructions

## Clone Repository

```bash
git clone https://github.com/yourusername/GLMS-ASP.NET-Core-MVC.git
```

## Open Solution

Open the solution in Visual Studio 2022.

## Configure Database

Update the SQL Server connection string in:

```json
appsettings.json
```

## Run Database Migration

Open Package Manager Console and run:

```powershell
Update-Database
```

## Run Application

Press:

```text
Ctrl + F5
```

---

# Future Improvements

Future enterprise enhancements may include:

* Authentication and authorisation
* Role-based access control
* Azure cloud deployment
* Audit logging
* Email notifications
* Advanced reporting
* REST API architecture
* Microservices migration

---

# Developer

Developed by Promise Khoza

Global Logistics Management System (GLMS)
2026

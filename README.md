# Customer Management System

Welcome to the Customer Management System repository! This project focuses on developing a robust application that adheres to SOLID design principles, implements intelligent validation, provides RESTful APIs, and integrates with a user-friendly UI for managing customer data.

## Key Features

- **SOLID Refactoring:**  
  The codebase has been refactored so that each model and service conforms to SOLID principles. Each class has a single responsibility, can be extended without modification, and relies on abstractions rather than concrete implementations.

- **Intelligent Field Validation:**  
  Advanced validation logic has been implemented for both form inputs and API endpoints. For example, if a US state is provided, the Postal Code/ZIP field is validated against the expected formats (`#####` or `#####-####`), and vice versa.

- **RESTful API Endpoints:**  
  Fully implemented RESTful APIs offer complete CRUD (Create, Read, Update, Delete) functionality for customer data:
  - **Create:** `POST /api/customers`
  - **Read (Single):** `GET /api/customers/{id}`
  - **Read (All):** `GET /api/customers`
  - **Update:** `PUT /api/customers/{id}`
  - **Delete:** `DELETE /api/customers/{id}`

- **UI Integration:**  
  The UI includes dynamic dropdown functionality that allows users to update and delete customer data directly, with the interface refreshing automatically to reflect changes.

## Development Steps

### 1. Model Refactoring
- **Objective:** Refactor the model code to follow SOLID principles.
- **Details:**
  - **Single Responsibility:** Each class now addresses a specific aspect of customer data management.
  - **Open/Closed & Dependency Inversion:** Abstractions and interfaces have been introduced so that new functionality can be added without modifying existing code.

### 2. Intelligent Field Validation
- **Objective:** Implement robust and intelligent validation for customer fields.
- **Details:**
  - Validation is performed on both the client forms and via API endpoints.
  - For instance, when a US state is provided, the ZIP code is validated against the formats `#####` or `#####-####`. Conversely, a ZIP code input triggers validation for the corresponding state.

### 3. RESTful API Implementation
- **Objective:** Provide a full set of CRUD operations for managing customer data.
- **Details:**
  - **POST /api/customers:** Create a new customer.
  - **GET /api/customers/{id}:** Retrieve the details of a specific customer.
  - **GET /api/customers:** Retrieve all customer records.
  - **PUT /api/customers/{id}:** Update an existing customer's data.
  - **DELETE /api/customers/{id}:** Delete a customer from the system.

### 4. UI Integration for Dropdown Actions
- **Objective:** Empower users with the ability to update and delete customer data directly through the UI.
- **Details:**
  - The customer dropdown list on the UI has been enhanced so that selections trigger RESTful API calls to update or delete the corresponding customer records.
  - The UI refreshes automatically to showcase the latest data after any modifications.

### 5. Testing and Continuous Integration
- **Objective:** Ensure the reliability and quality of the application through comprehensive testing.
- **Details:**
  - **Unit Tests:** MSTest-based tests have been added to validate individual methods, such as field validation logic.
  - **Integration Tests:** End-to-end testing ensures all RESTful endpoints work as expected.
  - Regular commits and continuous integration practices help ensure stable builds and code quality.

## Installation and Setup

### Build the Solution:
Open the solution file in Visual Studio.

Build the project to verify that all dependencies and references compile correctly.

### Restore Dependencies:
Restore the NuGet packages using Visual Studio's built-in package restore feature.

### Run the Application:
Launch the application from Visual Studio.

Test the API endpoints using tools such as Postman or via the Swagger UI if configured.

### Run Tests:
Open the Test Explorer in Visual Studio.

Execute all unit and integration tests to verify that everything is functioning as expected.

## Technology Stack

- **Backend:** ASP.NET Web API
- **Frontend:** Web Forms
- **Database:** Entity Framework
- **Unit Testing:** MSTest
- **Dependency Injection:** Simple Injector
- **Validation:** Custom regex-based and business logic validations

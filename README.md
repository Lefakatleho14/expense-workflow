# Expense Approval & Reimbursement Management System

A full-stack, enterprise-style expense management system built with ASP.NET Core (.NET 10) and Supabase (PostgreSQL), demonstrating real-world backend architecture, secure authentication, role-based workflows, and clean service-oriented design.

This project is designed as a flagship portfolio application to showcase job-ready backend and full-stack engineering skills.


🚀 Key Features

🔐 Authentication & Security

* JWT-based authentication
* Secure password hashing (BCrypt)
* Role-based authorization
* Protected API endpoints

👥 Role-Based Access

* **Employee** – submit expenses
* **Manager** – approve or reject submitted expenses
* **Finance** – process approved expenses and mark as paid
* **Admin** – system oversight (extensible)

🔄 Expense Workflow (Strictly Enforced)

```
Submitted → ManagerApproved → FinanceApproved → Paid
                    ↘
                   Rejected
```

* Invalid state transitions are blocked
* Employees cannot approve
* Managers cannot mark as paid
* Finance cannot approve before Manager
* All transitions are audited

📊 Reporting & Dashboards

* Monthly expense totals
* Category-based expense breakdown
* Role-specific dashboards
* Aggregation queries via EF Core

📁 Attachments (Optional Extension)

* Receipt uploads using Supabase Storage
* File paths stored in database
* Ready for cloud deployment


🧱 Architecture Overview

Backend

* **ASP.NET Core Web API (.NET 10)**
* Clean layered architecture
* Thin controllers
* Business logic in services
* EF Core with PostgreSQL (Supabase)
* Dependency Injection throughout

Frontend

* Static HTML, CSS, JavaScript
* Fetch API with JWT authorization
* Role-based UI rendering
* Served via ASP.NET `wwwroot`


🗂️ Project Structure

```
ExpenseSystem.Api
│
├── Controllers
│   ├── AuthController.cs
│   ├── ExpensesController.cs
│   ├── DashboardController.cs
│   └── ReportsController.cs
│
├── Services
│   ├── AuthService.cs
│   ├── ExpenseService.cs
│   └── ReportingService.cs
│
├── Models
│   ├── User.cs
│   ├── Expense.cs
│   ├── ExpenseStatusHistory.cs
│   └── Attachment.cs
│
├── DTOs
│   ├── RegisterDto.cs
│   ├── LoginDto.cs
│   ├── CreateExpenseDto.cs
│   └── ChangeExpenseStatusDto.cs
│
├── Data
│   └── AppDbContext.cs
│
├── wwwroot
│   ├── index.html
│   ├── login.html
│   ├── dashboard.html
│   ├── submit-expense.html
│   ├── css/
│   └── js/
│
├── Program.cs
└── appsettings.json
```


🗄️ Database Schema (Supabase / PostgreSQL)

users

* id (uuid)
* full_name
* email
* password_hash
* role
* created_at

expenses

* id (uuid)
* user_id
* amount
* category
* description
* expense_date
* status
* created_at

expense_status_history

* id (uuid)
* expense_id
* old_status
* new_status
* changed_by_user_id
* changed_at

attachments

* id (uuid)
* expense_id
* file_path
* uploaded_at


⚙️ Setup Instructions

Prerequisites

* .NET 10 SDK
* Visual Studio 2025
* Supabase account
* PostgreSQL enabled in Supabase

Configuration

1. Create Supabase project
2. Apply database schema
3. Update `appsettings.json`:

```json
"ConnectionStrings": {
  "Default": "Host=YOUR_SUPABASE_HOST;Database=postgres;Username=postgres;Password=YOUR_PASSWORD"
},
"Jwt": {
  "Key": "SUPER_SECRET_KEY",
  "Issuer": "ExpenseSystem",
  "Audience": "ExpenseSystemUsers"
}
```

Run

* Press ▶ in Visual Studio
* Open:

  * `https://localhost:7081/swagger`
  * `https://localhost:7081/index.html`


🧪 API Testing

* Swagger UI enabled in Development
* JWT-secured endpoints
* Role-based access enforced


🧠 Engineering Principles Demonstrated

* Clean Architecture
* Domain-driven thinking
* Explicit workflow rules
* Secure authentication
* Separation of concerns
* Async programming
* Cloud-ready design


📈 Why This Project Matters

This system goes beyond tutorials by implementing:

✔ Real business workflows
✔ Production-style backend architecture
✔ Security best practices
✔ Role-aware authorization
✔ Auditable state transitions

It reflects the **expectations of a junior developer ready for professional environments.


🔮 Future Enhancements

* Admin management UI
* Advanced analytics
* Email notifications
* Expense export (CSV / PDF)
* CI/CD pipeline

👤 Author

Lefa
Junior Software Engineer (Backend / Full Stack)
Portfolio project demonstrating production readiness


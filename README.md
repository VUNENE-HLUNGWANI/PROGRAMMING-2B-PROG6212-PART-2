# PROGRAMMING-2B-PROG6212-PART-2
Part 2 implementation of the RaceDay system - an ASP.NET Core Web API, Entity Framework Core database integration, session-based authentication, xUnit unit tests and automated GitHub Actions CI/CD workflows

# RaceDay Event Management System

> A comprehensive, enterprise-grade road race event management platform engineered to streamline participant registration, race categorization, result tracking, and administrative workflows.

---

## 📋 Table of Contents
- [Overview](#overview)
- [Key Features](#key-features)
- [Tech Stack & Architecture](#tech-stack--architecture)
- [System Architecture & Database Design](#system-architecture--database-design)
- [CI/CD Pipeline & Automation](#cicd-pipeline--automation)
- [Screenshots & UI Showcase](#screenshots--ui-showcase)
- [Video Demonstration](#video-demonstration)
- [Getting Started & Installation](#getting-started--installation)
- [API Documentation](#api-documentation)
- [License](#license)

---

## 🚀 Overview

**RaceDay** is designed to eliminate the logistical friction of organizing endurance and road running events. By providing a robust RESTful backend coupled with a relational database and automated delivery pipelines, RaceDay empowers race directors and administrators to manage events seamlessly from participant onboarding to final standings.

---

## ✨ Key Features

- **Participant Registration & Profile Management:** Secure registration workflows for runners, categorizing participants by age, gender, and race distance (e.g., 5km, 10km, 21.1km, 42.2km).
- **Event & Category Configuration:** Dynamic creation and management of race events, start times, and capacity limits.
- **RESTful Web API Architecture:** Clean, decoupled endpoints built with ASP.NET Core supporting CRUD operations and structured error handling.
- **Automated CI/CD Workflows:** Integrated GitHub Actions pipelines ensuring automated build verification, test execution, and deployment readiness on every push.
- **Relational Data Integrity:** Normalized SQL Server database schema enforcing strict primary/foreign key constraints and efficient indexing.

---

## 🛠️ Tech Stack & Architecture

- **Backend:** C#, ASP.NET Core Web API, Entity Framework Core (ORM)
- **Database:** Microsoft SQL Server (Transact-SQL)
- **CI/CD & Version Control:** Git, GitHub, GitHub Actions
- **Testing & Documentation:** Swagger / OpenAPI UI

---

## 🗄️ System Architecture & Database Design

The system follows a standard **N-Tier / Clean Architecture** separation of concerns:
1. **API Controllers Layer:** Handles HTTP requests, input validation, and response serialization.
2. **Business Logic / Services Layer:** Encapsulates core domain rules and race categorization logic.
3. **Data Access Layer:** Manages database interactions via Entity Framework Core and LINQ.

### Database Schema Highlights
- **Participants Table:** Stores runner demographics, contact information, and emergency contacts.
- **Events Table:** Tracks event metadata, dates, venues, and maximum capacities.
- **Registrations Table:** Junction table mapping participants to specific events and categories with status flags.

---

## ⚙️ CI/CD Pipeline & Automation

RaceDay utilizes **GitHub Actions** to automate the software delivery lifecycle. On every push and pull request to the main branch, the pipeline executes the following stages:
1. **Build:** Restores NuGet packages and compiles the solution across the API and test projects.
2. **Test:** Executes automated unit and integration test suites.
3. **Code Quality & Verification:** Verifies dependency injection configurations and database context migrations.

### CI / CD 
<img width="1920" height="1020" alt="Screenshot 2026-10-15 115949" src="https://github.com/user-attachments/assets/64145cc9-78d7-4cfb-8f00-d3f9b831199c" />


---

### 3. GitHub Actions CI/CD Pipeline
> *Successful build and test automation runs.*
> ![CI Pipeline Workflow] (<img width="1920" height="1020" alt="Screenshot 2026-10-15 120021" src="https://github.com/user-attachments/assets/5e4c4f61-0ade-4cf8-a9b2-05b137653249" />)


---

## 🎥 Video Demonstration

Watch a walkthrough of the RaceDay system architecture, API endpoints and automated CI/CD pipeline execution:

👉 **[Link to RaceDay Video Demonstration](https://youtu.be/YtjxY_UeXxQ)** *(YouTube)*

---

## 💻 Getting Started & Installation

### Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/) (latest version compatible with your project)
- [Microsoft SQL Server](https://www.microsoft.com/sql-server) or SQL Server Express
- Visual Studio Community or Visual Studio Code


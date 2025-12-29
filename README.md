# Finwell - Personal Finance Simulation

A small personal-finance simulation application built with .NET 8, featuring a Windows Forms UI and a shared library architecture.

## Project Overview

Finwell is a comprehensive personal finance simulation tool that helps users understand financial decision-making through interactive scenarios. The application consists of two main components:

- **FinwellUI**: Windows Forms user interface for user interaction
- **FinwellLibrary**: Shared library containing business logic, data access, and models

The BusinessLogic folder contains pure-game logic used by the library with no UI dependencies, designed to be consumed by both the UI and data-access layers.

## Architecture

### FinwellLibrary (.NET 8 Class Library)
The core library containing all business logic, data models, and data access components.

**Dependencies:**
- Dapper 2.1.66 (ORM for database operations)
- System.Configuration.ConfigurationManager 9.0.10
- System.Data.SqlClient 4.9.0

### FinwellUI (.NET 8 Windows Forms Application)
The user interface layer that provides an interactive experience for the finance simulation.

**Dependencies:**
- FinwellLibrary (project reference)
- Windows Forms framework

## Project Structure

### FinwellLibrary

#### **BusinessLogic/**
- `MonthlySimulationManager.cs` - Manages monthly simulation logic and calculations
- `RandomEventGenerator.cs` - Generates random financial events during simulation

#### **DataAccess/**
- `IDataConnection.cs` - Interface for database connection abstraction
- `SqlConnector.cs` - SQL Server implementation of data connection

#### **Models/**
- `LeaderboardEntryModel.cs` - Model for leaderboard entries
- `leaderboardModel.cs` - Leaderboard data structure
- `montlyDecisionModel.cs` - Monthly decision data model
- `randomEventModel.cs` - Random event data structure
- `scenarioModel.cs` - Financial scenario model
- `simRandomEventModel.cs` - Simulation random event model
- `simulationModel.cs` - Core simulation data model
- `simulationResultModel.cs` - Simulation results model
- `userModel.cs` - User account data model

#### **Configuration Files**
- `GlobalConfig.cs` - Global configuration and connection management
- `FinwellLibrary.csproj` - Project configuration file

### FinwellUI

#### **User Interface Forms**
- `authPage.cs/.Designer.cs/.resx` - User authentication page
- `LeadershipPage.cs/.Designer.cs/.resx` - Leaderboard display page
- `ResultPage.cs/.Designer.cs/.resx` - Simulation results display page
- `scenarioPage.cs/.Designer.cs/.resx` - Scenario selection page
- `montlyIteration.cs/.Designer.cs/.resx` - Monthly decision iteration page

#### **Configuration Files**
- `Program.cs` - Application entry point
- `App.config` - Application configuration
- `FinwellUI.csproj` - Project configuration file

## Key Features

1. **User Authentication** - Secure user login and registration
2. **Financial Scenarios** - Various real-world financial situations
3. **Monthly Decision Making** - Interactive monthly financial decisions
4. **Random Events** - Unexpected financial events simulation
5. **Results Analysis** - Comprehensive simulation results
6. **Leaderboard System** - Competitive ranking among users
7. **Data Persistence** - SQL Server database integration

## Technology Stack

- **.NET 8** - Latest .NET framework
- **Windows Forms** - Desktop UI framework
- **Dapper** - Lightweight ORM for database operations
- **SQL Server** - Database backend
- **C#** - Primary programming language

## Database Integration

The application uses SQL Server for data persistence with Dapper as the ORM layer. Connection strings are managed through configuration files, and the data access layer follows an interface-based design for testability and flexibility.

## Getting Started

1. Clone the repository
2. Configure database connection in App.config
3. Build the solution in Visual Studio
4. Run FinwellUI to start the application

## Design Principles

- **Separation of Concerns** - Clear separation between UI, business logic, and data access
- **Dependency Injection** - Interface-based design for loose coupling
- **Configuration Management** - Centralized configuration through GlobalConfig
- **Model-View Architecture** - Clean separation between data models and UI components

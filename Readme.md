# Expense Tracker Console Application

## Description
A simple console-based expense tracker application that helps users manage their finances. It allows users to add, delete, and view expenses categorized into various types like housing, utilities, transportation, and more. The application uses JSON for storing data persistently and features a modular design with a state-based user interface.

---

## Features
- **User Management**:
  - Add new users.
  - Check existing usernames.
  - Save and load users persistently using JSON.
- **Expense Management**:
  - Add, view, and delete expenses.
  - Expenses are categorized (e.g., Housing, Utilities, Transportation).
  - Display expense summaries with totals.
- **State-Based Navigation**:
  - Implements a state pattern for rendering different sections of the application, like the main menu or expense-related features.
- **Data Persistence**:
  - Uses JSON for storing users and expenses.
- **Console UI**:
  - Provides a tabular view for listing expenses and a clean, interactive console interface.

---

## Technologies and Tools
- **Programming Language**: C#
- **Storage**: JSON serialization using `System.Text.Json`
- **Design Patterns**:
  - **State Pattern**: For managing different screens (e.g., main menu, expense actions).
  - **Service Layer**: Separates business logic into services (`UserService`, `ExpenseService`).
  - **Command Pattern**: Handles different user actions in the system, making it easy to add new commands without changing existing code.
  - **Helper Utility**: Provides utility methods for input validation and display (`Helper` class).
- **Enum Usage**:
  - `Category` enum for predefined expense categories.

---

## Code Organization

### **Key Components**

#### 1. `Program.cs`
   - Entry point of the application.
   - Initializes services and state manager.
   - Registers an event to reset user data on exit.

#### 2. `StateManager.cs`
   - Core engine for managing application states.
   - Switches between states dynamically based on user interaction.

#### 3. `Helper.cs`
   - Provides utility methods:
     - Validates numeric input.
     - Displays expenses in a tabular format.
     - Configures JSON serializer options.

#### 4. `Catagories.cs`
   - Defines the `Category` enum for expense categorization.
   - Includes categories like Housing, Utilities, Medical, etc.

#### 5. **Services** (not included but referenced):
   - **`UserService`**: Manages user data (e.g., names, save/load operations).
   - **`ExpenseService`**: Manages expenses (e.g., CRUD operations, calculations).

---

## Data Storage
- **Format**: JSON
- **Persistence**:
  - Users and expenses are saved and loaded from JSON files.
  - JSON ensures human-readable storage and easy integration with other tools.
- **Helper Methods**:
  - Configures JSON serializer options for consistent parsing.

---

## Patterns Used

### **State Pattern**
- Centralized in `StateManager`.
- Manages transitions between different parts of the application (e.g., main menu, adding expenses).
- Provides a flexible way to expand the application's functionality.

### **Service Pattern**
- Business logic is abstracted into service classes (`UserService`, `ExpenseService`).
- Keeps the application modular and testable.

### **Helper Utility**
- The `Helper` class is a static utility for:
  - Input validation (`IsInputNumber`).
  - Displaying expenses in a structured format (`ShowExpensesList`).

### **Enum for Categories**
- Uses the `Category` enum for predefined expense categories.
- Ensures type safety and prevents invalid categories.

---

## How to Run

### 1. Clone the Repository:
```bash
git clone <repository_url>
cd ExpenseTracker
```

### 2. Build the Project:
Use Visual Studio or run:
```bash
dotnet build
```

### 3. Run the Application:
```bash
dotnet run
```

### 4. Navigate the Console Menu:
- Follow the interactive prompts to manage expenses.

---

## Future Improvements

- **Authentication**: Add user authentication for personalized expense tracking.
- **Data Validation**: Enhance validation for user inputs and ensure stricter checks.
- **File Handling**: Implement backup and recovery for JSON data files.
- **Graphical UI**: Expand to a GUI using a framework like WPF or integrate with a web-based front end.
- **Reporting**: Add features for generating and exporting financial reports.

---

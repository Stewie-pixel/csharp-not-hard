# Object-Oriented Development Practical Tasks in C#

This repository collects practical work for **SIT232: Object-Oriented Development**. It starts with C# fundamentals such as selection, casting, and loops, then builds toward object-oriented design, testing, inheritance, polymorphism, state machines, UML diagrams, and debugging exercises.

The project is organized as a learning portfolio: each folder under `src/` contains source code, task briefs, analysis notes, diagrams, or tests for a specific practical topic.

## Repository Structure

```text
.
├── LICENSE
├── README.md
├── docs/
│   ├── CODE_OF_CONDUCT.md
│   └── SECURITY.md
├── script/
│   └── auto_commit.sh
└── src/
    ├── arrays_and_lists/
    ├── buggysoft/
    ├── classes_and_objects/
    ├── inheritance/
    ├── polynomial/
    ├── polymorphism/
    ├── repetition/
    ├── selection_and_casting/
    ├── state_diagram/
    └── the_account_class/
```

## Learning Areas

| Area | Folder | Coverage |
| --- | --- | --- |
| Selection and casting | `src/selection_and_casting` | `if`, `switch`, explicit casting, microwave examples, and debugging snippets. |
| Repetition | `src/repetition` | `for`, `while`, `do...while`, number guessing, divisibility, loop rewrites, and debugging exercises. |
| Classes and objects | `src/classes_and_objects` | Mobile, employee, and car console applications with xUnit tests. |
| Account class and transactions | `src/the_account_class` | Bank account modeling, deposits, withdrawals, transfers, transaction classes, UML, and NUnit tests. |
| Arrays and lists | `src/arrays_and_lists` | Introductory collection handling in C#. |
| Polynomial app | `src/polynomial` | Polynomial modeling plus a separate test console project. |
| BuggySoft | `src/buggysoft` | Debugging and code revision exercises with original, revised, and final code versions. |
| Inheritance | `src/inheritance` | Zoo examples without inheritance, with inheritance, overloading, and class analysis. |
| Polymorphism | `src/polymorphism` | Bird, duck, and penguin polymorphism examples. |
| State diagrams | `src/state_diagram` | Simple and enhanced reaction-machine controllers, console apps, WinForms app, tester projects, UML, and state-transition documentation. |

## Documents and Supporting Files

The repository includes task briefs, analysis notes, diagrams, and policy documents. These are the main non-source documents to check when navigating the project.

### Root and Project Policy

| Document | Purpose |
| --- | --- |
| `README.md` | Main project overview and navigation guide. |
| `LICENSE` | Project license. |
| `docs/CODE_OF_CONDUCT.md` | Contributor Covenant-based participation guidelines. |
| `docs/SECURITY.md` | Security policy template and vulnerability reporting section. |

### Task Briefs and Analysis Documents

| Document | Related area |
| --- | --- |
| `src/selection_and_casting/1.1P_request.pdf` | Practical 1.1P brief for selection and casting. |
| `src/selection_and_casting/Analysis.md` | Analysis of selection, casting, and debugging snippets. |
| `src/repetition/1.2P_request.pdf` | Practical 1.2P brief for repetition. |
| `src/repetition/analyze.md` | Loop structure comparison, debugging analysis, and rewrite notes. |
| `src/classes_and_objects/SIT232-2.1P.pdf` | Practical 2.1P brief for classes and objects. |
| `src/classes_and_objects/README.md` | Detailed guide for the Mobile, Employee, and Car tasks. |
| `src/the_account_class/docs/sit232_2.2P.pdf` | Account class practical brief. |
| `src/the_account_class/docs/SIT232-3.2P.pdf` | Later account-class task brief. |
| `src/the_account_class/docs/SIT232-5.2P.pdf` | Later account-class task brief. |
| `src/the_account_class/docs/SIT232-6.2P.pdf` | Later account-class task brief. |
| `src/the_account_class/docs/SIT232-7.1P.pdf` | Later account-class task brief. |
| `src/arrays_and_lists/SIT232-3.1P.pdf` | Arrays and lists practical brief. |
| `src/polynomial/SIT232-3.3D.pdf` | Polynomial practical brief. |
| `src/buggysoft/SIT232-4.2C.pdf` | BuggySoft debugging practical brief. |
| `src/inheritance/SIT232-5.1P.pdf` | Inheritance practical brief. |
| `src/inheritance/Question 6 Answer.pdf` | Answer document for inheritance question 6. |
| `src/polymorphism/SIT232-6.1P.pdf` | Polymorphism practical brief. |
| `src/state_diagram/SIT232-5.3D.pdf` | State diagram practical brief. |
| `src/state_diagram/SIT232-5.4HD.pdf` | Extended state diagram practical brief. |
| `src/state_diagram/Notes on Finite State Machines.pdf` | Reference notes for finite state machines. |

### Diagrams

| Document | Purpose |
| --- | --- |
| `src/the_account_class/docs/UML_TheAccountClass.mmd` | Mermaid source for the account-class UML diagram. |
| `src/the_account_class/docs/UML_TheAccountClass.png` | Rendered account-class UML diagram. |
| `src/state_diagram/docs/UMLDiagram.md` | Mermaid class and state diagrams for the simple reaction machine. |
| `src/state_diagram/docs/StateTransitionDiagram.md` | State transition explanation for the simple reaction machine. |
| `src/state_diagram/docs/StateTransitionDiagram.xml` | XML version of the state transition diagram. |
| `src/state_diagram/docs/EnhancedController.mmd` | Mermaid source for the enhanced controller diagram. |
| `src/state_diagram/docs/EnhancedController.png` | Rendered enhanced controller diagram. |

## Solutions and Projects

| Solution or project | Description |
| --- | --- |
| `2.1P.sln` | Root solution file. |
| `src/classes_and_objects/SIT232_Practical_2.1P.slnx` | Classes and objects solution. |
| `src/classes_and_objects/src/Task1_Mobile/Task1_Mobile.csproj` | Mobile account console app. |
| `src/classes_and_objects/src/Task2_Employee/Task2_Employee.csproj` | Employee salary and tax console app. |
| `src/classes_and_objects/src/Task3_Car/Task3_Car.csproj` | Car fuel and mileage console app. |
| `src/classes_and_objects/tests/SIT232_Practical_2.1P.Tests/SIT232_Practical_2.1P.Tests.csproj` | xUnit tests for the classes and objects tasks. |
| `src/the_account_class/src/theaccountclass.slnx` | Account class solution. |
| `src/the_account_class/src/theaccountclass.csproj` | Banking and transaction console app. |
| `src/the_account_class/test/Account.Tests.csproj` | NUnit tests for account behavior. |
| `src/arrays_and_lists/ArraysAndLists.slnx` | Arrays and lists solution. |
| `src/arrays_and_lists/src/ArraysAndLists.csproj` | Arrays and lists console app. |
| `src/polynomial/src/PolynomialApp.csproj` | Polynomial source project. |
| `src/polynomial/test/TestPolynomialApp.csproj` | Polynomial test driver project. |
| `src/inheritance/5.1P.sln` | Inheritance solution. |
| `src/inheritance/src/Part1_NoInheritance/Part1_NoInheritance.csproj` | Zoo example before inheritance. |
| `src/inheritance/src/Part2_WithInheritance/Part2_WithInheritance.csproj` | Zoo example using inheritance. |
| `src/inheritance/src/Part5_Overloading/Part5_Overloading.csproj` | Method overloading example. |
| `src/inheritance/src/Part6_ClassAnalysis/Part6_ClassAnalysis.csproj` | Class relationship analysis example. |
| `src/polymorphism/PolymorphismSolution.sln` | Polymorphism solution. |
| `src/polymorphism/src/PolymorphismConsoleApp.csproj` | Bird polymorphism console app. |
| `src/state_diagram/src/EnhancedReactionMachineSolution.slnx` | Enhanced reaction-machine solution. |
| `src/state_diagram/src/SimpleReactionMachineConsoleApp/SimpleReactionMachineConsoleApp.csproj` | .NET console version of the simple reaction machine. |
| `src/state_diagram/src/SimpleReactionMachineTesterConsole/SimpleReactionMachineTesterConsole.csproj` | .NET console tester for the simple reaction machine. |
| `src/state_diagram/src/SimpleReactionMachine WinForms/SimpleReactionMachine.sln` | .NET Framework WinForms reaction-machine solution. |
| `src/state_diagram/src/SimpleReactionMachine WinForms/SimpleReactionMachine/SimpleReactionMachine.csproj` | WinForms reaction-machine app. |
| `src/state_diagram/src/SimpleReactionMachine TesterApp/SimpleReactionMachineTesterApp.csproj` | .NET Framework tester app. |
| `src/state_diagram/src/EnhancedSimpleReactionMachine/EnhancedSimpleReactionMachine/EnhancedReactionController.csproj` | Enhanced reaction controller implementation. |
| `src/state_diagram/src/EnhancedSimpleReactionMachine Tester/EnhancedSimpleReactionMachine Tester.csproj` | Enhanced reaction-machine tester. |

## Prerequisites

- .NET SDK 10.0 or newer for projects targeting `net10.0`.
- .NET SDK 8.0 for projects targeting `net8.0`.
- Visual Studio 2022 or compatible build tools for .NET Framework 4.6.1 and WinForms projects.
- Visual Studio Code with C# Dev Kit, Visual Studio, or JetBrains Rider for editing and debugging.

## Build and Run

Run commands from the repository root unless noted otherwise.

```bash
# Classes and objects
dotnet run --project src/classes_and_objects/src/Task1_Mobile/Task1_Mobile.csproj
dotnet run --project src/classes_and_objects/src/Task2_Employee/Task2_Employee.csproj
dotnet run --project src/classes_and_objects/src/Task3_Car/Task3_Car.csproj

# Account class
dotnet run --project src/the_account_class/src/theaccountclass.csproj

# Arrays and lists
dotnet run --project src/arrays_and_lists/src/ArraysAndLists.csproj

# Polynomial app
dotnet run --project src/polynomial/src/PolynomialApp.csproj

# Inheritance and polymorphism
dotnet run --project src/inheritance/src/Part2_WithInheritance/Part2_WithInheritance.csproj
dotnet run --project src/polymorphism/src/PolymorphismConsoleApp.csproj

# State-machine console app
dotnet run --project "src/state_diagram/src/SimpleReactionMachineConsoleApp/SimpleReactionMachineConsoleApp.csproj"
```

## Testing

```bash
# Classes and objects xUnit tests
dotnet test src/classes_and_objects/tests/SIT232_Practical_2.1P.Tests/SIT232_Practical_2.1P.Tests.csproj

# Account class NUnit tests
dotnet test src/the_account_class/test/Account.Tests.csproj

# Polynomial test driver
dotnet run --project src/polynomial/test/TestPolynomialApp.csproj

# State-machine tester console
dotnet run --project "src/state_diagram/src/SimpleReactionMachineTesterConsole/SimpleReactionMachineTesterConsole.csproj"
```

## Notes for Maintainers

- Keep each practical task self-contained inside its topic folder.
- Place task briefs, analysis notes, and rendered diagrams beside the code they explain.
- Prefer adding project-specific documentation to the relevant `src/<topic>/` folder, then link it from this README.
- Use automated tests where practical, especially for business logic such as accounts, transactions, calculations, and state transitions.

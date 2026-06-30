# AI_CONTEXT.md

> **Purpose**
>
> This document is the living memory of the PBI ModelLens project.
> It provides enough context for a new developer (or AI assistant) to continue development without reading the entire project history.

---

# Project

**Name**

PBI ModelLens

**Mission**

Understand your Power BI model with confidence.

PBI ModelLens is an open-source desktop application that helps Power BI developers understand, navigate and analyze Power BI projects.

The application focuses on answering questions such as:

* Where is this measure used?
* Which visuals depend on this column?
* Which objects are unused?
* What is the impact of deleting this object?

---

# Vision

PBI ModelLens is **not** another editor.

It is an analysis and navigation tool.

The application should help developers understand existing Power BI projects before making changes.

---

# MVP

The first production-ready version should support:

* Opening PBIP projects
* Exploring reports
* Exploring semantic models
* Dependency analysis
* Detecting unused objects

PBIP is the only supported format for the MVP.

---

# Architecture

Current projects:

* PowerBICleanup.Core
* PowerBICleanup.Engine
* PowerBICleanup.UI
* PowerBICleanup.Tests

Project renaming will happen after v0.1.

Planned names:

* PbiModelLens.Domain
* PbiModelLens.Application
* PbiModelLens.Infrastructure
* PbiModelLens.UI
* PbiModelLens.Tests

---

# Architectural Principles

* Clean Architecture
* Domain-driven design (lightweight)
* MVVM for the UI
* Small services with a single responsibility
* Small commits
* Small Pull Requests

Views contain no business logic.

Business logic belongs in Engine/Application.

---

# Current Domain Model

ModelLensProject

Properties:

* Name
* RootFolder
* PbipFile

Future additions:

* Report
* SemanticModel

---

# Current Engine

Readers

* PbipProjectReader

Services

* ProjectService

Current flow:

UI

↓

ProjectService

↓

PbipProjectReader

↓

ModelLensProject

---

# Current UI

Folders:

Views

ViewModels

Commands

Current classes:

* MainWindow
* MainWindowViewModel
* RelayCommand

MVVM has been introduced.

Dependency Injection has **not** been implemented yet.

---

# Coding Standards

* Nullable enabled
* Implicit usings enabled
* Use required where appropriate
* Prefer guard clauses
* Keep methods small
* One responsibility per class

Avoid:

* Helper classes
* Utils classes
* Large ViewModels

---

# Git Workflow

One Issue = One Branch

One Branch = One Pull Request

One Pull Request = One Feature

Never commit directly to main.

Merge using Pull Requests.

Delete feature branches after merge.

---

# Current Sprint

Sprint 1 – Explorer

Completed

* Project foundation
* Documentation
* Application shell
* MVVM foundation
* ModelLensProject
* PbipProjectReader
* ProjectService

In Progress

* Open PBIP folder
* Detect PBIP project

Upcoming

* Read .pbip file
* Read report pages
* Populate Explorer TreeView

---

# Backlog

Short term

* Dependency Injection
* Unit tests
* Explorer population
* Read report pages
* Read visuals

Long term

* Dependency analysis
* Search
* Duplicate DAX detection
* Unused objects
* Export
* CLI

---

# ADRs

ADR-0001

Use Clean Architecture.

ADR-0002

Support PBIP before PBIX.

---

# Development Philosophy

Observe.

Understand.

Improve.

We optimize for maintainability over speed.

The application should answer:

"What am I looking at?"

before attempting to modify anything.

---

# Team Conventions

Before implementing a feature:

* Review architecture.
* Keep the solution buildable.
* Keep commits small.
* Prefer readability over cleverness.

When in doubt:

Choose the simplest solution that can evolve.

---

# Current Technical Debt

* Introduce Dependency Injection.
* Replace concrete readers with interfaces throughout the application.
* Improve RelayCommand implementation.
* Remove remaining WinForms dependency by introducing an abstraction for folder selection.
* Rename projects after v0.1.

---

# Project Mascot

🦉

The owl represents observation, understanding and insight.

It reminds us that ModelLens exists to understand a model before changing it.

## Milestones

### v0.1
- Solution structure
- MVVM
- Open PBIP folder
- Read .pbip

### Current Focus
- Build Explorer UI
---

# Latest Milestone

## Milestone 1 Completed

The project now builds successfully from a clean `main` branch.

Implemented:

* Solution foundation
* MVVM foundation
* Open PBIP folder flow
* PBIP project detection
* Reading `.pbip` metadata
* Basic `ModelLensProject` domain model
* Initial project service orchestration

Current flow:

```text
UI
 ↓
MainWindowViewModel
 ↓
ProjectService
 ↓
IProjectReader / PbipProjectReader
 ↓
PbipFileReader
 ↓
ModelLensProject
```

The application currently uses a folder picker.
The user must select the folder that contains the `.pbip` file, not the `.pbip` file itself.

Current limitation:

* No Explorer UI yet
* No report pages loaded yet
* No semantic model loaded yet
* MessageBox is still used for temporary feedback

---

# Next Focus

## Milestone 2 – Explorer Foundation

Goal:

Turn the application from a simple MessageBox prototype into the first real ModelLens UI.

Planned steps:

1. Replace the basic button with an application shell.
2. Add a menu item: `File > Open PBIP Folder...`
3. Add left-side Explorer panel.
4. Add right-side Details panel.
5. Add bottom StatusBar.
6. Store the loaded project in `CurrentProject`.
7. Add an `ExplorerItem` UI model.
8. Bind a `TreeView` to an `ObservableCollection<ExplorerItem>`.
9. Populate root Explorer nodes:

```text
Project
 ├── Report
 └── Semantic Model
```

Pages and visuals are not part of this milestone.

---

# Suggested Folder Direction

Keep domain models in:

```text
src/PowerBICleanup.Core/Models
```

Examples:

* ModelLensProject
* Report
* ReportPage

Keep UI-specific models in the UI project, for example:

```text
src/PowerBICleanup.UI/Explorer
```

Examples:

* ExplorerItem

Keep readers organized by responsibility:

```text
Readers
 ├── Projects
 ├── Pbip
 ├── Reports
 └── SemanticModel
```

Folder reorganization is allowed, but keep namespaces clean and the build green after each move.

---

# Working Rules

* Keep `main` clean.
* One issue = one branch.
* One feature = one pull request.
* Keep every commit buildable.
* Do not introduce Dependency Injection yet.
* Do not introduce large frameworks yet.
* Prefer simple, readable C#.
* Grow the domain model one level at a time.
* Avoid reading visuals before report pages are working.
* Avoid dependency analysis until report and semantic model objects exist.

---

# Immediate Next Step

Create a new branch for Milestone 2:

```bash
git checkout -b feature/explorer-foundation
```

Then implement the application shell first:

```text
Menu
Explorer panel
Details panel
Status bar
```

Do not read pages yet.

The goal of the next step is only to make the app feel like a real desktop application.

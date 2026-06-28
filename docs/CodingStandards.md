# Coding Standards

## General

- Nullable enabled
- One class per file
- Constructor injection where appropriate
- Avoid static helper classes
- Keep methods short and focused

## Naming

- PascalCase for public members
- camelCase for locals
- Private fields start with _

## Architecture

- No business logic in the UI
- UI communicates through ViewModels
- Domain has no dependency on WPF

## Git

- Every change starts with an Issue
- Every Issue gets its own branch
- Use Pull Requests

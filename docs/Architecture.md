# Architecture

PBI ModelLens follows a layered architecture.

## Layers

UI

↓

Application

↓

Domain

↓

Infrastructure

## Principles

- The Domain Model is the heart of the application.
- The UI never reads PBIP or TMDL directly.
- Infrastructure reads external formats and maps them to the Domain Model.
- The Application layer coordinates use cases.

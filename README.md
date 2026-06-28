# Power BI Cleanup Tool

Power BI Cleanup Tool is a desktop application for analysing Power BI PBIP projects.

The goal is to help Power BI developers understand:

- which measures are used in visuals
- which columns are used in visuals
- where fields are used
- which measures may be cleanup candidates
- how report pages, visuals and semantic model objects relate to each other

## Current status

Early development.

## Tech stack

- .NET 8
- WPF
- System.Text.Json
- xUnit

## Roadmap

### v0.1
- Read PBIP pages
- List pages in UI

### v0.2
- Read visuals
- List visuals per page

### v0.3
- Read measures from semantic model

### v0.4
- Map measures to visuals
# Vision

## PBI ModelLens

**Understand your Power BI model.**

PBI ModelLens helps Power BI developers understand, analyze and maintain enterprise Power BI semantic models and reports.

The project focuses on making complex Power BI solutions easier to explore, maintain and improve.

PBI ModelLens does not aim to replace Tabular Editor, DAX Studio or Measure Killer. It complements them by providing insights that are difficult to obtain in one place.

## Why

Large enterprise Power BI solutions often contain many pages, visuals, measures, columns, relationships and DAX dependencies.

Understanding how these objects relate to each other is difficult.

PBI ModelLens makes these relationships visible.

## Design principles

### Read-only by default

PBI ModelLens never modifies a Power BI project unless the user explicitly requests it.

### Evidence over assumptions

The tool should clearly distinguish between facts and candidates.

For example:

- Fact: a measure is used in a visual.
- Candidate: a measure appears unused based on available metadata.

### Enterprise first

The project is designed for real-world enterprise models with many measures, visuals and report pages.

### Explain before optimizing

Before suggesting cleanup or improvements, the tool must first explain the current model.

### Engine independent from UI

The analysis engine should not depend on WPF or any other UI technology.

## Scope

PBI ModelLens focuses on:

- Report exploration
- Semantic model exploration
- Visual dependency analysis
- Measure and column usage analysis
- Cleanup candidates
- Model health insights
- Documentation generation

## Out of scope

PBI ModelLens is not intended to become:

- A Power BI report editor
- A replacement for Tabular Editor
- A replacement for DAX Studio
- A deployment tool
- An ETL tool

## Success

PBI ModelLens succeeds when Power BI developers spend less time searching through reports and models, and more time improving them.

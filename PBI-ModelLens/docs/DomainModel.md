# Domain Model

## Goal

The Domain Model represents a Power BI project independently from the underlying file format (PBIP, TMDL, etc.).

The UI, analyzers and exporters work only with the Domain Model.

---

# ModelLensProject

Represents a loaded Power BI project.

Contains:
- Report
- Semantic Model

---

# Report

Represents the report definition.

Contains:
- Report Pages
- Bookmarks
- Theme

---

# Report Page

Represents a single report page.

Contains:
- Visuals

---

# Report Visual

Represents a visual on a report page.

Contains:
- Field References
- Filters

---

# Field Reference

Represents a reference to a semantic model object used by a visual.

Can reference:
- Measure
- Column
- Hierarchy
- Parameter

Contains:
- Object Type
- Table
- Name
- Visual Role (Values, Axis, Legend, Tooltip, Filter, etc.)

---

# Semantic Model

Represents the semantic model.

Contains:
- Tables
- Relationships
- Perspectives
- Calculation Groups

---

# Semantic Table

Contains:
- Columns
- Measures

---

# Semantic Column

Represents a column in a semantic model.

---

# Semantic Measure

Represents a measure in a semantic model.

---

# Relationship

Represents a relationship between two tables.

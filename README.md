# MockupWorkflow.Admin

A Blazor Server administration portal for managing end-to-end product creation workflows.

MockupWorkflow.Admin provides the operational dashboard for a multi-service automation platform that manages POD (Print-on-Demand) products from import through batch processing and production.

---

## Features

### Batch Import

* Import POD items from JSON
* Automatically assign Batch IDs
* Select product type during import
* Store product metadata in MongoDB

### Automated Folder Creation

After importing a batch, the application automatically calls the FolderCreator API to create the required production folder structure.

Example:

```
/data/builds/
    └── 1782560251042/
        └── tshirt/
```

No manual folder creation is required.

### Batch Management

View all imported batches including:

* Batch ID
* Product Type
* Item Count
* Mockup Processing Status
* Last Modified Date

This provides a single location for tracking workflow progress.

---

## Architecture

```
               Import JSON
                     │
                     ▼
          MockupWorkflow.Admin
                     │
        REST API Requests
                     │
                     ▼
        PhotoshopAutomation.Api
              │              │
              │              ▼
              │          MongoDB
              │
              ▼
       FolderCreator.API
              │
              ▼
      Shared Docker Volume
```

---

## Technology Stack

* ASP.NET Core
* Blazor Server
* MudBlazor
* MongoDB
* Docker
* REST APIs
* C#

---

## Related Projects

### MockupWorkflow.Shared

Shared business models, MongoDB collections, and workflow objects.

### PhotoshopAutomation.Api

REST API responsible for importing POD items, managing batches, and coordinating workflow processing.

### FolderCreator.API

Creates and manages batch folder structures within the shared Docker volume.

---

## Current Workflow

1. Import POD items
2. Generate Batch ID
3. Save records to MongoDB
4. Automatically create production folders
5. View batches
6. Generate Photoshop mockups *(in progress)*
7. Upload generated assets *(planned)*
8. Publish products *(planned)*

---

## Project Status

Actively under development.

Current focus includes:

* Batch management
* Photoshop automation integration
* Automated asset generation
* Production workflow tracking

---

## Screenshots

*(To be added)*

* Import page
* Batch Management page
* Production dashboard

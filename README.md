# MockupWorkflow.Admin

![.NET 10](https://img.shields.io/badge/.NET-10-purple)
![Blazor Server](https://img.shields.io/badge/Blazor-Server-512BD4)
![MudBlazor](https://img.shields.io/badge/MudBlazor-UI-594AE2)
![MongoDB](https://img.shields.io/badge/MongoDB-Database-47A248)
![Docker](https://img.shields.io/badge/Docker-Enabled-2496ED)
![Status](https://img.shields.io/badge/Status-Active_Development-success)

MockupWorkflow.Admin is the Blazor Server administration application for the Mockup Workflow Platform. It provides a centralized interface for importing production batches, monitoring workflow progress, reviewing processing status, and managing automated Photoshop production workflows.

The application integrates with PhotoshopAutomation.Api and supporting platform services to coordinate batch processing, monitor workflow execution, and provide operational visibility across the automation pipeline.

---

## Features

### Batch Import

- Import workflow records from JSON manifests
- Automatically generate and assign batch identifiers
- Select product type during import
- Validate imported records before processing
- Store workflow records in MongoDB

### Batch Monitoring

Monitor every production batch from a centralized dashboard, including:

- Batch ID
- Product type
- Record count
- Processing status
- Mockup generation status
- Last modified timestamp

This provides operators with real-time visibility into workflow progress.

### Workflow Management

Track workflow execution across multiple processing stages, including:

- Imported
- Pending
- Processing
- Completed
- Failed

Individual records expose detailed processing information to simplify troubleshooting and recovery.

### Folder Provisioning

Following a successful import, the application automatically calls the FolderCreator API to provision the required production folder structure for each batch. This eliminates manual setup and ensures a consistent directory layout across the workflow platform.

### Platform Integration

MockupWorkflow.Admin coordinates with the platform's backend services through REST APIs, including:

- PhotoshopAutomation.Api
- FolderCreator.Api
- PNGAPI
- MongoDB

Together these services provide an end-to-end workflow for managing production batches and automated asset generation.

### Responsive Blazor Interface

Built with Blazor Server and MudBlazor, the administration interface provides:

- Interactive data tables
- Live status updates
- Batch detail pages
- Import workflows
- Responsive layouts
- Material Design components
---

## Architecture

MockupWorkflow.Admin serves as the operational dashboard for the Mockup Workflow Platform. It provides a web-based interface for importing workflow records, monitoring batch execution, reviewing processing status, and coordinating automated production workflows.

The application communicates with the platform's backend services through REST APIs while persisting workflow data in MongoDB.

<p align="center">
  <img src="docs/images/architecture.png"
       alt="Mockup Workflow Admin Architecture"
       width="900">
</p>

## Workflow Overview

The administration application supports the complete operational workflow:

1. Import workflow records from JSON.
2. Validate and persist records in MongoDB.
3. Automatically provision production folders.
4. Monitor batch processing.
5. Review individual record status.
6. Track Photoshop mockup generation.
7. Verify generated assets.
8. Prepare completed batches for downstream publishing.

### Service Responsibilities

| Component | Responsibility |
|-----------|----------------|
| **MockupWorkflow.Admin** | Blazor Server administration interface for operators |
| **PhotoshopAutomation.Api** | Coordinates workflow execution and batch processing |
| **FolderCreator.Api** | Creates standardized production folder structures |
| **PNGAPI** | Manages input and generated asset storage |
| **MongoDB** | Stores workflow records, batch metadata, and processing state |
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

REST API responsible for importing production records, managing batches, and coordinating workflow processing.

### FolderCreator.API

Creates and manages batch folder structures within the shared Docker volume.

---

## Current Workflow

1. Import production records
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

### Batch Overview

The Batch Overview page provides operators with real-time visibility into workflow execution, processing progress, and mockup generation status across an entire production batch.

<p align="center">
  <img src="screenshots/batch-overview.png"
       alt="Batch Overview"
       width="1000">
</p>

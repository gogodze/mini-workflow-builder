# Mini Workflow Builder

A Blazor WebAssembly application that provides a canvas-based workflow builder with drag-and-drop functionality, node editing capabilities, and visual workflow creation.


![](https://github.com/gogodze/mini-workflow-builder/blob/master/Desktop%202025.07.09%20-%2015.24.47.05.mp4)


## 🎯 Overview

This project implements a trimmed-down version of a workflow canvas featuring:
- Drag-and-drop node creation from a toolbar
- Interactive canvas with Basic and Complex (For Each) nodes
- Right-side drawer for node configuration
- Real-time two-way data binding
- Sample workflow loading from JSON

## 🛠 Technology Stack

- **Framework**: Blazor WebAssembly (.NET 8)
- **Diagramming**: Z.Blazor.Diagrams v3.0.3
- **Styling**: Tailwind CSS
- **Data**: JSON deserialization with System.Text.Json

## 📋 Prerequisites

Before running this project, ensure you have:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- A modern web browser (Chrome, Firefox, Edge, Safari)
- [Node.js](https://nodejs.org/) (for Tailwind CSS compilation)

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone <repository-url>
cd MiniWorkflowBuilder
```

### 2. Install Dependencies

```bash
# Restore .NET packages
dotnet restore

# Install Node.js dependencies (for Tailwind)
npm install
```

### 3. Build Tailwind CSS

```bash
# Build Tailwind CSS
npm run build-css
```

### 4. Run the Application

```bash
# Start the development server
dotnet run
```

Or alternatively:

```bash
# Watch mode for development
dotnet watch
```

## 🎮 Usage

### Basic Operations

1. **Creating Nodes**: Drag icons from the left toolbar onto the canvas
2. **Editing Nodes**: Click any node to open the configuration drawer
3. **Node Properties**: Edit label and filters in the drawer with real-time updates
4. **Filters**: Add/remove filter conditions with field name, condition type, and values

## ⚙️ Configuration

### Tailwind CSS

The project uses Tailwind CSS for styling. Configuration is in `tailwind.config.js`:

```javascript
module.exports = {
  content: ["./**/*.{razor,html}"],
  theme: {
    extend: {
      // Custom design tokens can be added here
    }
  }
}
```

### Sample Data Format

The `sample-workflow.json` follows this structure:

```json
{
  "Workflow": {
    "WorkflowName": "Demo",
    "Steps": [...]
  },
  "Application": {
    "ApplicationId": 1,
    "Activities": [...]
  }
}
```

## 🔧 Development

### Building for Production

```bash
dotnet publish -c Release
```

### Running Tests

```bash
dotnet test
```

### Implemented Features ✅
- Basic drag-and-drop functionality
- Node creation and rendering
- Drawer-based node editing
- JSON data loading
- Filter management
- Responsive layout foundation


**Time Investment**: ~12-16 hours (≈ 2 working days)  
**Assignment Goal**: Demonstrate front-end craft, component structure, and UX polish

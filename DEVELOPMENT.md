# Development Guide

This document provides instructions for setting up, deploying, and debugging the VSCode Project Manager extension for Command Palette.

## Prerequisites

- **Visual Studio 2022** with the "Windows app development" workload.
- **Windows App SDK** installed (handled via NuGet).
- **VS Code** with the [Project Manager](https://marketplace.visualstudio.com/items?itemName=alefragnani.project-manager) extension installed and configured.

## Project Structure

- `Program.cs`: Entry point handling COM registration.
- `ProjectManager.cs`: Main extension class and provider management.
- `ProjectManagerCommandsProvider.cs`: Defines the top-level commands.
- `Pages/ProjectManagerPage.cs`: Implements the project list view.
- `Commands/OpenProjectCommand.cs`: Logic for launching VS Code.
- `Package.appxmanifest`: MSIX package configuration and extension registration.

## Deployment

To make the extension visible in Command Palette, it must be deployed as an MSIX package:

1. Open the solution in Visual Studio.
2. Set the build configuration to **x64** (or your target architecture).
3. Right-click the **ProjectManager** project in Solution Explorer.
4. Select **Deploy**.
5. Ensure the output window shows "Deploy succeeded."

## How to Launch the Extension

After deployment, you need to enable and launch the extension within Command Palette:

1. Open **Command Palette**.
2. Go to **Settings** (usually via the gear icon or by searching "Settings").
3. Navigate to the **Extensions** tab.
4. Locate **VSCode Project Manager** in the list.
5. Ensure it is **Enabled**.
6. You can launch it directly from the Settings page using the **Launch** button, or search for "VSCode Project Manager" in the main Command Palette search bar.

> **Note:** If the extension does not appear after deployment, try restarting Command Palette (right-click the tray icon and select "Quit", then relaunch).

## Debugging

Since the extension is hosted by Command Palette, standard F5 debugging requires an extra step.

### Attaching to Process
1. Deploy the extension as described above.
2. Launch the extension from Command Palette.
3. In Visual Studio, go to **Debug > Attach to Process...** (Ctrl+Alt+P).
4. Find and select `ProjectManager.exe`.
5. You can now hit breakpoints in your code.

### Debugging Startup Logic
To debug code that runs during the initial activation:
1. Open `Program.cs`.
2. Uncomment `System.Diagnostics.Debugger.Launch();`.
3. **Deploy** the project again.
4. Launch the extension from Command Palette.
5. A "Just-In-Time Debugger" dialog will appear. Select your running instance of Visual Studio.

## Troubleshooting

- **Extension not showing:** Check `Package.appxmanifest` to ensure the `ClassId` matches the `[Guid]` attribute in `ProjectManager.cs`.
- **JSON Parsing Errors:** Ensure your VS Code `projects.json` is located at `%AppData%\Code\User\globalStorage\alefragnani.project-manager\projects.json`.
- **Command Fails:** Check if `code` is in your system's PATH. The extension uses PowerShell to launch VS Code.

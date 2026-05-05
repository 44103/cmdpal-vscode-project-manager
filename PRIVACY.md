# Privacy Policy for VSCode Project Manager (Command Palette Extension)

Last updated: May 5, 2026

## Overview
The VSCode Project Manager extension ("the Extension") is designed to be a local-only utility for the Command Palette. We value your privacy and aim to be transparent about how data is handled.

## Data Collection and Usage
- **No Personal Information:** The Extension does not collect, store, or transmit any personal information (data that could be used to identify a person).
- **Local Data Access Only:** The Extension reads the local configuration file of the VS Code "Project Manager" extension (typically located at `%AppData%\Code\User\globalStorage\alefragnani.project-manager\projects.json`).
- **Purpose of Access:** This data is accessed solely to display your project list within the Command Palette UI and to facilitate opening them in VS Code.
- **No Data Transmission:** No data read by the Extension is ever transmitted to external servers or third parties. All processing happens locally on your machine.

## Permissions
- **runFullTrust:** The Extension requires this capability to launch the VS Code executable (`code`) via PowerShell on your behalf.
- **Internet Access:** This Extension does not require or use internet connectivity.

## Third-Party Services
The Extension interacts with:
- **Visual Studio Code:** To open the projects you select.
- **Project Manager (VS Code Extension):** To read the list of projects you have defined.
We do not control how these third-party applications handle data. Please refer to their respective privacy policies.

## Contact
If you have any questions about this Privacy Policy, please contact the developer via the GitHub repository issue tracker.

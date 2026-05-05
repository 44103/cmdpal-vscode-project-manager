// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace ProjectManager;

public partial class ProjectManagerCommandsProvider : CommandProvider
{
    private readonly ICommandItem[] _commands;

    public ProjectManagerCommandsProvider()
    {
        DisplayName = "VSCode Project Manager";
        Icon = IconHelpers.FromRelativePath("Assets\\vscode.png");
        _commands = [
            new CommandItem(new ProjectManagerPage()) { Title = DisplayName },
        ];
    }

    public override ICommandItem[] TopLevelCommands()
    {
        return _commands;
    }

}

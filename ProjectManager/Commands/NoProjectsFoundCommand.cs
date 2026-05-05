using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace ProjectManager.Commands;

internal sealed partial class NoProjectsFoundCommand : InvokableCommand
{
    private readonly string? _errorMessage;

    public NoProjectsFoundCommand(string? errorMessage = null)
    {
        _errorMessage = errorMessage;
    }

    public override ICommandResult Invoke()
    {
        if (_errorMessage != null)
        {
            return CommandResult.ShowToast(_errorMessage);
        }
        return CommandResult.ShowToast("No projects were found in VSCode Project Manager.");
    }
}

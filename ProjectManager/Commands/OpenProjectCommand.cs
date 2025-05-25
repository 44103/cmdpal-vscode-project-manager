using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ProjectManager.Commands;

class Resource
{
    public required string Name { get; set; }
    public required bool IsRemote { get; set; }

    public override string ToString()
    {
        if (IsRemote)
            return $"--remote {Name}";
        return "";
    }
}

partial class OpenProjectCommand : InvokableCommand
{
    public Resource Resource { get; set; }
    public string Path { get; set; }

    private static readonly Regex remoteRegex = new Regex(@"^vscode-remote://(.*?)(/.*)?$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public OpenProjectCommand(Project project)
    {
        Match match = remoteRegex.Match(project.RootPath);
        Resource = match switch
        {
            { Success: true } m => new Resource() { Name = m.Groups[1].Value, IsRemote = true },
            _ => new Resource() { Name = "local", IsRemote = false },
        };
        Path = match switch
        {
            { Success: true } m => m.Groups[2].Value,
            _ => project.RootPath,
        };
    }

    public override ICommandResult Invoke()
    {
        string command = $"code {this.Resource} {this.Path}";
        Process.Start(new ProcessStartInfo("powershell.exe")
        {
            Arguments = $"-NoProfile -ExecutionPolicy Bypass {command}",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true,
        });
        return CommandResult.Hide();
    }
}

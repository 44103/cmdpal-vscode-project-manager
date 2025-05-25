// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using ProjectManager.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProjectManager;

public class Project
{
    public required string Name { get; set; }
    public required string RootPath { get; set; }
    public IList<string> Paths { get; set; } = new List<string>();
    public IList<string> Tags { get; set; } = new List<string>();
    public bool Enabled { get; set; }
}

[JsonSerializable(typeof(List<Project>))]
public partial class AppJsonSerializerContext : JsonSerializerContext { }

internal sealed partial class ProjectManagerPage : ListPage
{
    public ProjectManagerPage()
    {
        Icon = IconHelpers.FromRelativePath("Assets\\StoreLogo.png");
        Title = "VSCode Project Manager";
        Name = "Open";
    }

    public override IListItem[] GetItems()
    {
        string path = $"{GlobalStoragePath}\\alefragnani.project-manager\\projects.json";

        string jsonString = File.ReadAllText(path);

        List<Project> projects = JsonSerializer.Deserialize<List<Project>>(jsonString, options)!;

        return projects.Select(static x => new ListItem(new OpenProjectCommand(x)) { Title = x.Name })
                       .Cast<IListItem>()
                       .ToArray();
    }

    private static string GlobalStoragePath
    {
        get
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(appDataPath, "Code", "User", "globalStorage");
        }
    }

    private static readonly JsonSerializerOptions options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        TypeInfoResolver = AppJsonSerializerContext.Default,
    };
}

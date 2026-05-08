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

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true)]
[JsonSerializable(typeof(List<Project>))]
public partial class AppJsonSerializerContext : JsonSerializerContext { }

internal sealed partial class ProjectManagerPage : ListPage
{
    public ProjectManagerPage()
    {
        Icon = IconHelpers.FromRelativePath("Assets\\proj-mng-icon.svg");
        Title = "VSCode Project Manager";
        Name = "Open";
    }

    public override IListItem[] GetItems()
    {
        string path = Path.Combine(GlobalStoragePath, "alefragnani.project-manager", "projects.json");

        if (!File.Exists(path))
        {
            return [new ListItem(new NoProjectsFoundCommand()) { Title = "No projects found" }];
        }

        try
        {
            string jsonString = File.ReadAllText(path);
            var projects = JsonSerializer.Deserialize(jsonString, AppJsonSerializerContext.Default.ListProject) ?? new List<Project>();

            return projects.Select(static x => new ListItem(new OpenProjectCommand(x)) { Title = x.Name })
                           .Cast<IListItem>()
                           .ToArray();
        }
        catch (Exception ex)
        {
            return [new ListItem(new NoProjectsFoundCommand(ex.Message)) { Title = "Error loading projects" }];
        }
    }

    private static string GlobalStoragePath
    {
        get
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(appDataPath, "Code", "User", "globalStorage");
        }
    }
}

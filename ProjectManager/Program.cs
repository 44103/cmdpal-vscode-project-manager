// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CommandPalette.Extensions;
using Shmuelie.WinRTServer;
using Shmuelie.WinRTServer.CsWinRT;
using System;
using System.Linq;
using System.Threading;

namespace ProjectManager;

public class Program
{
    [MTAThread]
    public static void Main(string[] args)
    {
        if (args.Any(arg => arg.Equals("-RegisterProcessAsComServer", StringComparison.OrdinalIgnoreCase)))
        {
            global::Shmuelie.WinRTServer.ComServer server = new();

            ManualResetEvent extensionDisposedEvent = new(false);

            ProjectManager extensionInstance = new(extensionDisposedEvent);
            server.RegisterClass<ProjectManager, IExtension>(() => extensionInstance);
            server.Start();

            extensionDisposedEvent.WaitOne();
            server.Stop();
            server.UnsafeDispose();
        }
        else
        {
            Console.WriteLine("VSCode Project Manager Extension");
            Console.WriteLine("This executable is intended to be launched as a Command Palette extension.");
            Console.WriteLine("Arguments passed: " + string.Join(" ", args));
        }
    }
}

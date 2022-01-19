using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IngameDebugConsole;

public static class ConsoleCommands 
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    public static void AddCommands()
    {
        //DebugLogConsole.AddCommand("print", "print something", (string t) =>
        //{
        //    Debug.Log(t);
        //});

        //DebugLogConsole.AddCommand<string>("loadscene", "Load a new scene with single mode", SceneCommands.SwitchScene);
        DebugLogConsole.AddCommand("showAllTagged", "Print all tagged Transform", RBRXDebugCommands.ShowAllTagged);
        DebugLogConsole.AddCommand<string>("fakeEvent", "", RBRXDebugCommands.FakeEvent);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public static class RBRXDebugCommands 
{
    public static void ShowAllTagged()
    {
        foreach (var tag in CollectiveService.Tagged)
        {
            foreach (var i in tag.Value)
            {
                Debug.Log(tag.Key + "->" + i.Value.name + "->ID:" + i.Value.GetInstanceID());
            }
        }
    }

    public static void FakeEvent(string eventName)
    {
        StandardEvents.OnNext(eventName, "DebugConsole", Unit.Default);
    }
}

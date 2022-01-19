using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public static class SceneConfig 
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void AddControlConsole()
    {
        var controlConsole = Resources.Load("RBRX/IngameDebugConsole") as GameObject;
        Assert.IsNotNull(controlConsole, "Failed to find IngameDebugConsole.prefab in Resource folder");
        GameObject.Instantiate(controlConsole, Vector3.zero, Quaternion.identity);
    }
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void AddRBRXControlEntity()
    {

    }

}

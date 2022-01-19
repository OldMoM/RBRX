using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public static class CommandSetHandleSystem 
{
    private static void LoadScene(string[] words)
    {
        var sceneName = words[1];
        SceneManager.LoadScene(sceneName);
    }

    public static Dictionary<string, Action<string[]>> actions = new Dictionary<string, Action<string[]>>()
    {
        {"loadscene",LoadScene}
    };
}

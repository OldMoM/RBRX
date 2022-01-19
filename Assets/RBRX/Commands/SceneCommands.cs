using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCommands 
{
    public static void SwitchScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public static void ReplaceScene(string replaceScene,string newScene)
    {

    }
}

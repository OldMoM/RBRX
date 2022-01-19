using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Assertions;

public class CommandsSet : MonoBehaviour
{
    public List<string> commands;
    // Start is called before the first frame update
    void Start()
    {
        foreach (string command in commands)
        {
            var words = command.Split('_');
            var commandStart = words[0];
            var hasKey = CommandSetHandleSystem.actions.ContainsKey(commandStart);
            Assert.IsTrue(hasKey, "¥ÌŒÛ÷∏¡Ó:" + command);
            CommandSetHandleSystem.actions[commandStart](words);
        }
    }
}

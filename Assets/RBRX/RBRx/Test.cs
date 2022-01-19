using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.InputSystem;
using System;

public class Test : MonoBehaviour
{
    PlayerStateData player = new PlayerStateData();
    private Subject<ObservableArg> onKeyboardPressed = new Subject<ObservableArg>();
    private void Start()
    {
        
    }
}

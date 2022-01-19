using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class GUIEvents 
{
    public  Subject<string> onMouse1ClickedButton = new Subject<string>();
    public  Subject<string> onPointerEnterButton  = new Subject<string>();
    public  Subject<string> onPointerExitButton   = new Subject<string>();
}

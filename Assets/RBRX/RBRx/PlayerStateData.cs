using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class PlayerStateData 
{
    public IntReactiveProperty hp   = new IntReactiveProperty(100);
    public IntReactiveProperty mana = new IntReactiveProperty(100);
}

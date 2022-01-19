using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public static class Standalizor 
{
    public static IObservable<ObservableArg> ToObservableArg(IntReactiveProperty intReactive,string sender)
    {
        return intReactive.Select(x => new ObservableArg(sender, x));
    }
}

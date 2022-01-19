using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public static class StandardEvents
{
    public static GUIEvents gui = new GUIEvents();

    private static Dictionary<string, IObserver<ObservableArg>>   onNextTable = new Dictionary<string, IObserver<ObservableArg>>();
    private static Dictionary<string, IObservable<ObservableArg>> subscribeTable = new Dictionary<string, IObservable<ObservableArg>>();

    private static Hashtable observabels = new Hashtable();

    private static Dictionary<string, Subject<ObservableArg>> midBodys = new Dictionary<string, Subject<ObservableArg>>();

    //public static IObservable<ObservableArg> SubscribeEvent(string name)
    //{
    //    var hasEvent = subscribeTable.ContainsKey(name);
    //    if (!hasEvent)
    //    {
    //        var subject = new Subject<ObservableArg>();
    //        subscribeTable.Add(name, subject);
    //        onNextTable.Add(name, subject);
    //    }
    //    Debug.Log("subscribe operation: " + onNextTable[name].GetHashCode());

    //    return subscribeTable[name];
    //}
    //public static void OnNextEvent(string name,object message,string sender)
    //{
    //    var hasEvent = onNextTable.ContainsKey(name);
    //    if (!hasEvent)
    //    {
    //        var subject = new Subject<ObservableArg>();
    //        onNextTable.Add(name, subject);
    //        subscribeTable.Add(name, subject);
    //    }
    //    onNextTable[name].OnNext(new ObservableArg(sender, message));
    //}
    //public static void RegisterObservable(string name,IObservable<ObservableArg> observable)
    //{
    //    var hasKey = subscribeTable.ContainsKey(name);
    //    if (!hasKey)
    //    {
    //        subscribeTable.Add(name, observable);
    //        Debug.Log("registor operation " + observable.GetHashCode());
    //    }
    //}

    public static void OnNext(string name,string sender,object message)
    {
        var hasKey = midBodys.ContainsKey(name);
        if (!hasKey)
        {
            var subject = new Subject<ObservableArg>();
            midBodys.Add(name, subject);
        }
        midBodys[name].OnNext(new ObservableArg(sender, message));
    }
    public static IObservable<ObservableArg> GetObservable(string name)
    {
        var hasEvent = midBodys.ContainsKey(name);
        if (!hasEvent)
        {
            var subject = new Subject<ObservableArg>();
            midBodys.Add(name, subject);
        }
        return midBodys[name];
    }
}

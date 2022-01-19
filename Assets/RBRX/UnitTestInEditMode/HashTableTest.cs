using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UniRx;
using System;

public class HashTableTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void HashTableTestSimplePasses()
    {
        var hashTable = new Hashtable();

        hashTable.Add("test", 123);
        hashTable.Add("mouseClick", new Subject<Unit>());

        IObservable<Unit> onMouseClick = hashTable["mouseClick"] as IObservable<Unit>;

        

        Type type = hashTable["mouseClick"].GetType();
        var members = type.GetProperty("outObserver");
        
    }

}

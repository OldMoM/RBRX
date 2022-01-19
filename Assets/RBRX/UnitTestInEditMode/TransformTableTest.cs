using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TransformTableTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void TransformTableTestSimplePasses()
    {
        var table = new TransformTable<int>()
        {
            {(state:"Idle",isGround:true), 1},
            {(state:"Idle",isGround:false), 2}
        };

        var key = (state: "Idle", isGround: true);
        int value = 0;
        table.TryGetValue(key,out value);
        Debug.Log(value);
        Assert.AreEqual(1, value);
    }
    [Test]
    public void ComplicatedSearch()
    {
        var table = new TransformTable<int>()
        {
            {((state:"Idle",isGround:false),"ToWalk"),2 },
            {(state:"Idle",isGround:false), 2}
        };
        var key = ((state: "Idle", isGround: false), "ToWalk");
        int value = 0;
        table.TryGetValue(key, out value);
        Debug.Log(value);
        Assert.AreEqual(2, value);
    }
    [Test]
    public void UseObjectAsKey()
    {
        var table = new TransformTable<int>()
        {
            {((state:"Idle",isGround:false),"ToWalk"),2 },
            {(state:"Idle",isGround:false), 2}
        };
        object key = ((state: "Idle", isGround: false), "ToWalk");
        int value = 0;
        table.TryGetValue(key, out value);
        Debug.Log(value);
        Assert.AreEqual(2, value);
    }
}

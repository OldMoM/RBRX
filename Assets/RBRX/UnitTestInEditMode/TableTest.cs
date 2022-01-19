using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;

public class TableTest
{
    // A Test behaves as an ordinary method
    [Test]
    [Description("测试：使用_连接字符串作为转化表查找Key值")]
    public void UesStringArrayAsKey_false()
    {
        var key1 = new string[2] { "Idle", "Grounded" };

        var table = new Dictionary<string[], bool>()
        {
            {new string[2] { "Idle", "Grounded" },true },
        };
        Assert.IsFalse(table.ContainsKey(key1));

    }
    [Test]
    [Description("测试：使用_连接字符串作为转化表查找Key值")]
    public void UseContinousStringAsKey_true()
    {
        var key = "Idle_Grounded";
        var table = new Dictionary<string, bool>()
        {
            {"Idle_Grounded",true }
        };

        Assert.IsTrue(table.ContainsKey(key));
    }
    [Test]
    [Description("测试：使用TupleValue字符串作为转化表查找Key值")]
    public void UseStringTupleValueAsKey_true()
    {
        var key = ("Idle", "Ground");
        var table = new Dictionary<(string, string), bool>()
        {
            {("Idle", "Ground"),true },
        };
        Assert.IsTrue(table.ContainsKey(key));
    }
    [Test]
    [Description("测试：使用List<string>字符串作为转化表查找Key值")]
    public void UseStringListValueAsKey_false()
    {
        var key = new List<string>() { "Idle", "Ground" };
        var table = new Dictionary<List<string>, bool>()
        {
            {new List<string>() { "Idle", "Ground" },true }
        };

        Assert.IsFalse(table.ContainsKey(key));
    }
    [Test]
    [Description("测试：元组Key值相等比较")]
    public void CompareValueTuple_SameKeyName_true()
    {
        var value1 = (state: 1, isground: true);
        var value2 = (state: 1, isground: true);
        Assert.AreEqual(value1, value2);
    }
    [Test]
    [Description("测试：元组Key值相等比较。即使Key名不同，不影响Value的比较")]
    public void CompareValueTuple_DifferentKeyName_true()
    {
        var value1 = (state1: 1, grounded: true);
        var value2 = (state: 1, isground: true);
        Assert.AreEqual(value1, value2);
    }
    [Test]
    [Description("测试：以元组作为Key，查找HashTable中的Value")]
    //但是HashTable的Value不能指定类型呀
    public void UseValueTupleAsKey_SearchValueInHashTable_true()
    {
       
        Hashtable hashtable = new Hashtable()
        {
            {("Idle",true),true }
        };

        var t1 = ("Idle", true);
        Assert.IsTrue(hashtable.ContainsKey(t1));
    }

}

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
    [Description("���ԣ�ʹ��_�����ַ�����Ϊת�������Keyֵ")]
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
    [Description("���ԣ�ʹ��_�����ַ�����Ϊת�������Keyֵ")]
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
    [Description("���ԣ�ʹ��TupleValue�ַ�����Ϊת�������Keyֵ")]
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
    [Description("���ԣ�ʹ��List<string>�ַ�����Ϊת�������Keyֵ")]
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
    [Description("���ԣ�Ԫ��Keyֵ��ȱȽ�")]
    public void CompareValueTuple_SameKeyName_true()
    {
        var value1 = (state: 1, isground: true);
        var value2 = (state: 1, isground: true);
        Assert.AreEqual(value1, value2);
    }
    [Test]
    [Description("���ԣ�Ԫ��Keyֵ��ȱȽϡ���ʹKey����ͬ����Ӱ��Value�ıȽ�")]
    public void CompareValueTuple_DifferentKeyName_true()
    {
        var value1 = (state1: 1, grounded: true);
        var value2 = (state: 1, isground: true);
        Assert.AreEqual(value1, value2);
    }
    [Test]
    [Description("���ԣ���Ԫ����ΪKey������HashTable�е�Value")]
    //����HashTable��Value����ָ������ѽ
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

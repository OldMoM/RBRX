using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UniRx;

public class CollectiveServiceTest
{
    [Test]
    public void AddTag_EmptyDictionary()
    {
        var gameObject = new GameObject();
        CollectiveService.AddTag(gameObject.transform, "Frame");

        var actual = CollectiveService.Tagged;

        Assert.IsNotNull(actual);
    }
    [Test]
    public void AddTag_HasTag()
    {
        CollectiveService.Clear();

        var gameObject1 = new GameObject();
        var gameObject2 = new GameObject();
        CollectiveService.AddTag(gameObject1.transform, "Frame");
        CollectiveService.AddTag(gameObject2.transform, "Frame");

        var actual = CollectiveService.Tagged["Frame"].Count;
        var expected = 2;

        Assert.AreEqual(expected, actual);
    }
    [Test]
    public void AddTag_HasSameEntityAtSameTag_Reject()
    {
        CollectiveService.Clear();
        var gameObject = new GameObject();
        CollectiveService.AddTag(gameObject.transform, "Frame");
        CollectiveService.AddTag(gameObject.transform, "Frame");

        var actual = CollectiveService.Tagged["Frame"].Count;
        Assert.AreEqual(1, actual);
    }
    [Test]
    public void AddTag_SameTagSameNameDifferentEntity_2()
    {
        CollectiveService.Clear();
        var gameObject1 = new GameObject();
        var gameObject2 = new GameObject();
        gameObject1.name = "TestOne";
        gameObject2.name = "TestOne";

        CollectiveService.AddTag(gameObject1.transform, "Frame");
        CollectiveService.AddTag(gameObject2.transform, "Frame");

        var frameTaggedEntity = CollectiveService.Tagged["Frame"];
        foreach (var item in frameTaggedEntity)
        {
            Debug.Log(item.Key);
        }

        var actual = frameTaggedEntity.Count;
        Assert.AreEqual(2, actual);
    }
    [Test]
    public void GetInstanceAddedSignal_EventMark_true()
    {
        CollectiveService.Clear();
        var gameObject = new GameObject();

        var eventMark = false;
        CollectiveService.GetInstanceAddedSignal("Frame")
            .Subscribe(x =>
            {
                eventMark = true;
            });

        Assert.IsTrue(eventMark);
    }
    [Test]
    public void GetTagged_2Element()
    {
        CollectiveService.Clear();
        var gameObject1 = new GameObject();
        var gameObject2 = new GameObject();

        CollectiveService.AddTag(gameObject1.transform, "Frame");
        CollectiveService.AddTag(gameObject2.transform, "Frame");

        List<Transform> list = new List<Transform>();

        CollectiveService.GetTagged("Frame")
            .Subscribe(x =>
            {
                list.Add(x);
                Debug.Log(x.name);
            });

        var actual = list.Count;
        Assert.AreEqual(2, actual);
    }
    [Test]
    public void GetTag_HasTagOnTransform_Frame()
    {
        var gameobject = new GameObject();
        CollectiveService.AddTag(gameobject.transform, "Frame");

        var actual = CollectiveService.GetTag(gameobject.transform);
        Assert.AreEqual("Frame", actual);
    }
    [Test]
    public void GetTransformByName_SameNameTranform_2()
    {
        CollectiveService.Clear();
        var gameObject1 = new GameObject();
        var gameObject2 = new GameObject();
        gameObject1.name = "TestOne";
        gameObject2.name = "TestOne";

        CollectiveService.AddTag(gameObject1.transform, "Frame");
        CollectiveService.AddTag(gameObject2.transform, "Frame");

        var list = new List<Transform>();
        CollectiveService.GetTransformByName("Frame", "TestOne")
            .Subscribe(x =>
            {
                list.Add(x);
                Debug.Log(x.name);
            });

        var actual = list.Count;
        Assert.AreEqual(2, actual);

    }

}

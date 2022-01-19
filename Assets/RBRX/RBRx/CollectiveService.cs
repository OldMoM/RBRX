using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;


public static class CollectiveService
{
    private static Dictionary<string, Dictionary<string, Transform>> taggedEntity = new Dictionary<string, Dictionary<string, Transform>>();

    private static ReplaySubject<Tuple<string, Transform>> onTagAdded = new ReplaySubject<Tuple<string, Transform>>();

    /// <summary>Get copy of all the tagged.</summary>
    /// <value>The tagged.</value>
    public static Dictionary<string, Dictionary<string, Transform>> Tagged => taggedEntity;

    /// <summary>
    ///   <para>
    /// Adds the tag to instance.
    /// The transform, Tag componet added, will be tagged automatically.</para>
    ///   <para>I connect instanceId with name by "-" as the only key identifier.</para>
    /// </summary>
    /// <param name="transform">The transform.</param>
    /// <param name="tag">The tag.</param>
    /// <seealso cref="CollectiveServiceTest" />
    public static void AddTag(Transform transform, string tag)
    {
        var id = transform.GetInstanceID();
        var entityKey = transform.name + id;
        var hasKey = taggedEntity.ContainsKey(tag);
        //Check has Tag component
        var tagComponent = transform.GetComponent<Tag>();
        if (tagComponent == null)
        {
            transform.gameObject.AddComponent<Tag>()._Tag = tag;
        }


        if (hasKey)
        {
            var hasAdded = taggedEntity[tag].ContainsKey(entityKey);
            if (!hasAdded)
            {
                taggedEntity[tag].Add(entityKey, transform);
            }
        }
        else
        {
            var newDictionary = new Dictionary<string, Transform>();
            newDictionary.Add(entityKey, transform);
            taggedEntity.Add(tag, newDictionary);
        }
        onTagAdded.OnNext(new Tuple<string, Transform>(tag, transform));
    }

    /// <summary>Fires when a certain instance was tagged</summary>
    /// <param name="tag">The tag.</param>
    /// <returns>The tagged transform</returns>
    public static IObservable<Transform> GetInstanceAddedSignal(string tag)
    {
        return
            onTagAdded
            .Where(x => x.Item1 == tag)
            .Select(x => x.Item2);
    }

    /// <summary>Gets all of the tagged transform.</summary>
    /// <param name="tag">The tag.</param>
    /// <returns>
    ///   <br />
    /// </returns>
    public static IObservable<Transform> GetTagged(string tag)
    {
        var hasTag = taggedEntity.ContainsKey(tag);
        if (hasTag is false)
        {
            throw new Exception("Not found tag: " + tag);
        }
        return new Dictionary<string, Transform>(taggedEntity[tag]).ToObservable().Select(x => x.Value);
    }

    public static bool HasTag(Transform transform, string tag)
    {
        return taggedEntity.ContainsKey(tag);
    }

    /// <summary>Gets the tramfrom's tag.</summary>
    /// <param name="transform">The transform.</param>
    /// <returns>
    ///   <br />
    /// </returns>
    public static string GetTag(Transform transform)
    {
        var tagComponent = transform.GetComponent<Tag>();
        Assert.IsNotNull(tagComponent, "No Tag at " + transform.name);
        return tagComponent._Tag;
    }

    public static IObservable<Transform> GetTransformByName(string tag, string name)
    {
        var hasTag = taggedEntity.ContainsKey(tag);
        if (!hasTag) throw new Exception("Error tag name: " + tag);

        return taggedEntity[tag]
            .ToObservable()
            .Where(x =>
            {
                var words = x.Key.Split('-');
                var _name = words[0];

                return _name == name;
            })
            .Select(x => x.Value);
    }

    public static void RemoveTag(string tag)
    {

    }
    public static void RemoveTransform(Transform transform)
    {

    }


    /// <summary>Clear all tag instance info.</summary>
    public static void Clear()
    {
        taggedEntity.Clear();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    public static void ClearWhenSceneUnload()
    {
        SceneManager.sceneUnloaded += (Scene scene) =>
        {
            taggedEntity.Clear();
            onTagAdded = new ReplaySubject<Tuple<string, Transform>>();
        };
    }
}

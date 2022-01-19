using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

/// <summary>
///   <para>1.ʹ�ò�ֵͬ���Ͳ�����ΪKey��һ��Ϊint��bool��string�Լ�Ԫ��</para>
///   <para>2.����ָ������T</para>
///   <para>3.ӵ�й�����</para>
///   <para>4.��Ϊת����ʹ��</para>
/// </summary>
/// <typeparam name="T"></typeparam>
public class TransformTable<T>:IEnumerable
{
    private Hashtable hashtable = new Hashtable();
    public void Add(object key,T value)
    {
        var hasKey = hashtable.ContainsKey(key);
        if (hasKey) throw new System.Exception("Same key has been added to table:" + key);

        hashtable.Add(key, value);
    }
    public bool TryGetValue(object key, out T value)
    {
        value = default(T);
        var hasKey = hashtable.ContainsKey(key);
        if (hasKey)
        {
            value = (T)hashtable[key];
        }
        return hasKey;
    }
    public IObservable<T> TryGetValue(object key)
    {
        var hasKey = hashtable.ContainsKey(key);
        return Observable.Create<T>((observer) =>
        {
            T value = (T)default;
            if (hasKey)
            {
                value = (T)hashtable[key];
            }
            observer.OnNext(value);
            observer.OnCompleted();
            return Disposable.Empty;
        }).Where(x=>hasKey);
    }
    public IEnumerator GetEnumerator()
    {
        return ((IEnumerable)hashtable).GetEnumerator();
    }
    public bool ContainsKey(object key)
    {
        return hashtable.ContainsKey(key);
    }
    public T Get(object key)
    {
        return (T)hashtable[key];
    }
}

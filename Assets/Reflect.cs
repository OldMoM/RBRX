using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Reflect : MonoBehaviour
{
    public HashSet<string> hs = new HashSet<string>();
    public Hashtable hashtable = new Hashtable();
    // Start is called before the first frame update
    void Start()
    {
        hashtable.Add("first", 1);
        hashtable.Add("secone", 1.7f);

        var a = hashtable["first"];
        Type type = a.GetType();

        print(type.Name);
    }
}

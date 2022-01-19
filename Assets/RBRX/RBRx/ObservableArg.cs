using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ObservableArg 
{
    public string sender;
    public object messgae;

    public ObservableArg(string sender,object message)
    {
        this.sender  = sender;
        this.messgae = message;
    }
}

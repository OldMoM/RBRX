using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tag : MonoBehaviour
{
    public string _Tag;
    public string[] commands;

    private void OnEnable()
    {
        CollectiveService.AddTag(transform, _Tag);
        this.enabled = false;
    }
}

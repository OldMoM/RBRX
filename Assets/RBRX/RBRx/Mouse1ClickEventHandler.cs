using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class Mouse1ClickEventHandler : MonoBehaviour
{
    private static Action<string[]> switchFrame = (x) =>
    {
        var name = x[1];
        CollectiveService.GetTagged("Frame")
        .Where(x => x.name == name)
        .Subscribe(x1 =>
        {
            foreach (Transform item in x1)
            {
                var state = item.gameObject.activeInHierarchy;
                item.gameObject.SetActive(!state);
            }
        });
    };
    private static Action<string[]> closeFrame = (x) =>
    {
        var name = x[1];
        print(22);
        CollectiveService.GetTagged("Frame")
        .Where(x => x.name == name)
        .Subscribe(x1 =>
        {
            foreach (Transform item in x1)
            {
                item.gameObject.SetActive(false);
            }
        });
    };

    private Dictionary<string, Action<string[]>> actions = new Dictionary<string, Action<string[]>>
    {
        {"switch",switchFrame},
        {"close" , closeFrame}
    };

    // Start is called before the first frame update
    void Start()
    {
        StandardEvents.gui.onMouse1ClickedButton
            .Subscribe(x =>
            {
                var words = x.Split('_');
                print(words[0]);
                if (actions.ContainsKey(words[0]))
                {
                    actions[words[0]](words);
                }
            });
    }
}

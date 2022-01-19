using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class NewBehaviourScript 
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

    private static Dictionary<string, Action<string[]>> actions = new Dictionary<string, Action<string[]>>
    {
        {"switch",switchFrame},
        {"close" , closeFrame}
    };
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void test()
    {
        StandardEvents.gui.onMouse1ClickedButton
        .Subscribe(x =>
        {
            var words = x.Split('_');
            if (actions.ContainsKey(words[0]))
            {
              actions[words[0]](words);
            }
      });
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;


public class NewBehaviourScript1 
{
    [RuntimeInitializeOnLoadMethod()]
    public static void Init()
    {
        CollectiveService.GetInstanceAddedSignal("Button")
        .Subscribe(x =>
        {
            var button = x.GetComponent<Button>();
            ObservableTriggerExtensions.OnPointerClickAsObservable(button)
            .Subscribe(x1 =>
            {
                StandardEvents.gui.onMouse1ClickedButton.OnNext(x.name);
            });
        });
    }
}

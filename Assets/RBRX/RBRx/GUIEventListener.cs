using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using UniRx.Triggers;

public class GUIEventListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
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

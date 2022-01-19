using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UniRx;

public class PlayerInputListener : MonoBehaviour
{
    public void OnPlayerMoved(InputAction.CallbackContext callback)
    {
        StandardEvents.OnNext(EventList.playerMove, this.name, callback.ReadValue<Vector2>());
    }
    public void OnPlayerJump(InputAction.CallbackContext callback)
    {
        StandardEvents.OnNext(EventList.playerJump, this.name, Unit.Default);
    }
    public void OnPlayerSprint(InputAction.CallbackContext callback)
    {
        if (callback.started)
        {
            StandardEvents.OnNext(EventList.playerSprint, this.name, Unit.Default);
        }
    }
}

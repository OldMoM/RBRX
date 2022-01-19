using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EventTransformSystem
{
     public static void TransformState(PlayerStateComponent state,string input,TransformTable<PlayerState> table)
     {
        var current = (state.state.Value, state.isGrounded);
        var transformMatrix = (current, input);
        table.TryGetValue(transformMatrix)
            .Subscribe(x =>
            {
                state.state.Value = x;
            });
     }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using UniRx;

public class InputHandleSystem : IInitializeSystem
{
    public void Initialize()
    {
        StandardEvents.GetObservable(EventList.playerMove)
            .Subscribe(x =>
            {
                var entities = Context<Default>.AllOf<InputComponent>();
                foreach (Entity e in entities)
                {
                    var input = e.Modify<InputComponent>();
                    input.direction = (Vector2)x.messgae;
                }
            });
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using UniRx;
using System;

public class PlayerSprintSystem : IExecuteSystem,IInitializeSystem
{
    private Action state = () => { };
    private Vector2 dir;

    private float gravity;

    private void SprintState()
    {
        var entities = Context<Default>.AllOf<VelocityCompnent, PlayerStateComponent>();
        foreach (Entity e in entities)
        {
            var velocity = e.Modify<VelocityCompnent>();
            velocity.velocity += dir * velocity.sprintAcc * Time.deltaTime;

            var speed = velocity.velocity.magnitude;
            if (speed <= 1)
            {
                StandardEvents.OnNext(EventList.sprintEnd, "MovementSystem", Unit.Default);
                state = () => { };
            }
        }
    }
    public void Execute()
    {
        state();
    }

    public void Initialize()
    {
        StandardEvents.GetObservable(EventList.playerState)
        .Where(x => (PlayerState)x.messgae == PlayerState.SPRINT)
        .Subscribe(x =>
        {
            var entities = Context<Default>.AllOf<VelocityCompnent, InputComponent>();
            foreach (Entity e in entities)
            {
                var velocity = e.Get<VelocityCompnent>();
                var input = e.Get<InputComponent>();
                dir = velocity.face;

                velocity.velocity = dir * velocity.sprintSpeed;
            }
            state = SprintState;
        });
    }
}

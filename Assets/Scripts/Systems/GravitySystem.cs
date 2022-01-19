using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class GravitySystem :IInitializeSystem,IExecuteSystem
{
    public void Execute()
    {
        var entities = Context<Default>.AllOf<InputComponent, VelocityCompnent>();
        foreach (Entity e in entities)
        {
            var input = e.Get<InputComponent>();
            var velocity = e.Modify<VelocityCompnent>();
            var state = e.Get<PlayerStateComponent>();
            if (input.direction != Vector2.zero)
            {
                velocity.face = input.direction;
            }

            if (!state.isGrounded)
            {
                velocity.velocity.y += -9.81f * Time.deltaTime;
            }
        }
    }

    public void Initialize()
    {
        
    }
}

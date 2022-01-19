using UnityEngine;
using Entitas;
using UniRx;
using System;

public class MovementSystem:IInitializeSystem,IExecuteSystem
{
    private Action state = () => { };


    private void FallState()
    {
        var entities = Context<Default>.AllOf<VelocityCompnent, InputComponent>();
        foreach (Entity e in entities)
        {
            var velocity = e.Modify<VelocityCompnent>();
            var input = e.Get<InputComponent>();

            var currentVelocity_x = velocity.velocity.x;
            var tagetVeclocity_x = input.direction.x * velocity.speed;
            currentVelocity_x = Mathf.Lerp(currentVelocity_x, tagetVeclocity_x, 0.2f);

            velocity.velocity.x = tagetVeclocity_x;
            //velocity.velocity.y += -9.81f * Time.deltaTime;
        }
    }
    private void IdleState() { }
    public void Initialize()
    {
        StandardEvents.GetObservable(EventList.playerState)
            .Where(x => (PlayerState)x.messgae == PlayerState.WALK)
            .Subscribe(x =>
            {
                state = IdleState;
                var entities = Context<Default>.AllOf<VelocityCompnent, InputComponent>();
                foreach (Entity e in entities)
                {
                    var velocity = e.ModifyComponent<VelocityCompnent>();
                    var input = e.Get<InputComponent>();
                    velocity.velocity = velocity.speed * new Vector2(input.direction.x, 0);
                }
            });

        StandardEvents.GetObservable(EventList.playerState)
            .Where(x => (PlayerState)x.messgae == PlayerState.IDLE)
            .Subscribe(x =>
            {
                state = IdleState;

                var entities = Context<Default>.AllOf<VelocityCompnent>();
                foreach (Entity e in entities)
                {
                    var velocity = e.ModifyComponent<VelocityCompnent>();
                    velocity.velocity = Vector2.zero;
                }
            });

        StandardEvents.GetObservable(EventList.playerState)
            .Where(x => (PlayerState)x.messgae == PlayerState.JUMP)
            .Subscribe(x =>
            {
                var entities = Context<Default>.AllOf<VelocityCompnent>();
                foreach (Entity e in entities)
                {
                    
                    var velocity = e.ModifyComponent<VelocityCompnent>();
                    var jumpSpeed = Mathf.Sqrt(2 * 9.81f * velocity.jumpHeight);
                    velocity.velocity = new Vector2(0, jumpSpeed);

                    StandardEvents.OnNext(EventList.jumpEnd, "MovementSystem", Unit.Default);
                }
            });

        StandardEvents.GetObservable(EventList.playerState)
            .Where(x => (PlayerState)x.messgae == PlayerState.FALL)
            .Subscribe(x =>
            {
                state = FallState;
            });


    }
    public void Execute()
    {
        if (state != null)
        {
            state();
        }

        var entities = Context<Default>.AllOf<InputComponent,VelocityCompnent>();
        foreach (Entity e in entities)
        {
            var input = e.Get<InputComponent>();
            var velocity = e.Modify<VelocityCompnent>();
            if (input.direction != Vector2.zero)
            {
                velocity.face = input.direction;
            }
        }
    }
}

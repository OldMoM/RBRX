using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using UniRx;

public class PlayerStateSystem //: IInitializeSystem
{
    private static void SwtichTargetState((PlayerState,bool) current, (PlayerState, bool) next,PlayerStateComponent stateComponent)
    {
        var key = (current, next);
        (PlayerState, bool) value;
        var hasKey = PlayerStateFilterTables.filter.TryGetValue(key, out value);
        if (hasKey)
        {
            stateComponent.state.Value      = value.Item1;
            stateComponent.isGrounded = value.Item2;
        }
    }

    private static (PlayerState,bool) GetCurrentState(PlayerStateComponent component)
    {
        return (component.state.Value, component.isGrounded);
    }

    public static void OnPlayerGrounded(ObservableArg arg)
    {
        var entities = Context<Default>.AllOf<PlayerStateComponent,InputComponent>();
        foreach (Entity e in entities)
        {
            var stat = e.GetComponent<PlayerStateComponent>();
            stat.isGrounded = true;
            var input = e.Get<InputComponent>();
            var current = GetCurrentState(stat);
            var next = current;
            next.Item1 = input.direction.x == 0 ? PlayerState.IDLE : PlayerState.WALK;
            SwtichTargetState(current, next, stat);
        }
    }

    public void Initialize()
    {
        StandardEvents.GetObservable(EventList.playerMove)
            .Subscribe(x =>
            {
                var dir = (Vector2)x.messgae;
                var entities = Context<Default>.AllOf<PlayerStateComponent>();
                foreach (Entity e in entities)
                {
                    var stateComponent = e.Modify<PlayerStateComponent>();
                    var current   = GetCurrentState(stateComponent);
                    var next = current;
                    next.Item1 = (dir.x == 0) ? PlayerState.IDLE : PlayerState.WALK;
                    SwtichTargetState(current, next, stateComponent);
                }
            });

        StandardEvents.GetObservable(EventList.playerJump)
            .Subscribe(x =>
            {
                var entities = Context<Default>.AllOf<PlayerStateComponent>();
                foreach (Entity e in entities)
                {
                    var stateComponent = e.Get<PlayerStateComponent>();
                    var current = GetCurrentState(stateComponent);
                    var next = current;
                    next.Item1 = PlayerState.JUMP;
                    SwtichTargetState(current, next, stateComponent);
                }
            });

        StandardEvents.GetObservable(EventList.playerSprint)
            .Subscribe(x =>
            {
                var entities = Context<Default>.AllOf<PlayerStateComponent>();
                foreach (Entity e in entities)
                {
                    var stateComponent = e.Get<PlayerStateComponent>();
                    var current = GetCurrentState(stateComponent);
                    var next = current;
                    next.Item1 = PlayerState.SPRINT;
                    SwtichTargetState(current, next, stateComponent);
                }
            });

        StandardEvents.GetObservable(EventList.playerGrounded)
            .Where(x => (bool)x.messgae)
            .Where(x1 =>
            {
                var entities = Context<Default>.AllOf<PlayerStateComponent, VelocityCompnent>();
                foreach (Entity e in entities)
                {
                    var velocity = e.Get<VelocityCompnent>();
                    return velocity.velocity.y < -0.1f;
                }
                return false;
            })
            .Subscribe(OnPlayerGrounded);

        StandardEvents.GetObservable(EventList.playerGrounded)
            .Where(x => !(bool)x.messgae)
            .Subscribe(x =>
            {
                var entities = Context<Default>.AllOf<PlayerStateComponent, VelocityCompnent>();
                foreach (Entity e in entities)
                {
                    var state = e.Modify<PlayerStateComponent>();
                    state.isGrounded = false;
                    //state.state.Value = PlayerState.FALL;
                    var current = GetCurrentState(state);
                    var next = current;
                    next.Item1 = PlayerState.FALL;
                    SwtichTargetState(current, next, state);
                }
            });

        StandardEvents.GetObservable(EventList.sprintEnd)
            .Subscribe(x =>
            {
                var entities = Context<Default>.AllOf<PlayerStateComponent,InputComponent>();
                foreach (Entity e in entities)
                {
                    var state = e.Modify<PlayerStateComponent>();
                    var input = e.Get<InputComponent>();
                    var current = GetCurrentState(state);
                    var next = current;
                    next.Item1 = input.direction.x == 0 ? PlayerState.IDLE : PlayerState.WALK;
                    SwtichTargetState(current, next, state);
                }
            });
        StandardEvents.GetObservable(EventList.jumpEnd)
            .Subscribe(x =>
            {
                var entities = Context<Default>.AllOf<PlayerStateComponent>();
                foreach (Entity e in entities)
                {
                    var state = e.Modify<PlayerStateComponent>();
                    state.state.Value = PlayerState.FALL;
                }
            });
    }
}

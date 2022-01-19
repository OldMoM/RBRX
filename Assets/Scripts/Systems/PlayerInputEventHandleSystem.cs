using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using UniRx;

public class PlayerInputEventHandleSystem : IInitializeSystem
{
    public void Initialize()
    {
        StandardEvents.GetObservable(EventList.playerMove)
            .Subscribe(x =>
            {
                var directon = (Vector2)x.messgae;
                var entities = Context<Default>.AllOf<PlayerStateComponent>();
                foreach (Entity e in entities)
                {
                    var state = e.Modify<PlayerStateComponent>();
                    var input = (directon.x == 0) ? "ToIdle" : "ToWalk";
                    EventTransformSystem.TransformState(state, input, PlayerStateFilterTables.table);
                }
            });

        StandardEvents.GetObservable(EventList.playerGrounded)
            .Subscribe(x =>
            {
                var entities = Context<Default>.AllOf<PlayerStateComponent>();
                foreach (Entity e in entities)
                {
                    var state = e.Modify<PlayerStateComponent>();
                    state.isGrounded = (bool)x.messgae;
                }
            });
        //落地检测
        StandardEvents.GetObservable(EventList.playerGrounded)
            .Where(x=>(bool)x.messgae)
            .Where(x =>
            {
                var entities = Context<Default>.AllOf<PlayerStateComponent, VelocityCompnent>();
                foreach (Entity e in entities)
                {
                    var velocity = e.Get<VelocityCompnent>();
                    return velocity.velocity.y < -0.1f;
                }
                return false;
            })
            .Subscribe(x =>
            {
                var entities = Context<Default>.AllOf<PlayerStateComponent>();
                foreach (Entity e in entities)
                {
                    var state = e.Modify<PlayerStateComponent>();
                    var keyboardInput = e.Get<InputComponent>();
                    var input = keyboardInput.direction.x == 0 ? "ToIdle" : "ToWalk";
                    EventTransformSystem.TransformState(state, input, PlayerStateFilterTables.table);
                }
            });
        //自由落体检测
        StandardEvents.GetObservable(EventList.playerGrounded)
           .Where(x => !(bool)x.messgae)
           .Subscribe(x =>
           {
               var entities = Context<Default>.AllOf<PlayerStateComponent>();
               foreach (Entity e in entities)
               {
                   var state = e.Modify<PlayerStateComponent>();
                   var input = "ToFall";
                   EventTransformSystem.TransformState(state, input, PlayerStateFilterTables.table);
               }
           });
        //收到跳跃指令
        StandardEvents.GetObservable(EventList.playerJump)
            .Subscribe(x =>
            {
                var entities = Context<Default>.AllOf<PlayerStateComponent>();
                foreach (Entity e in entities)
                {
                    var stateComponent = e.Get<PlayerStateComponent>();
                    var input = "ToJump";
                    EventTransformSystem.TransformState(stateComponent, input, PlayerStateFilterTables.table);
                }
            });
        //跳跃加速阶段完成
        StandardEvents.GetObservable(EventList.jumpEnd)
            .Subscribe(x =>
            {
                
                var entities = Context<Default>.AllOf<PlayerStateComponent>();
                foreach (Entity e in entities)
                {
                    Debug.Log("Player jump end");
                    var stateComponent = e.Get<PlayerStateComponent>();
                    var input = "JumpEnd";
                    EventTransformSystem.TransformState(stateComponent, input, PlayerStateFilterTables.table);
                }
            });
    }
}

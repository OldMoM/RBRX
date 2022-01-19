using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using UniRx;

public class PlayerViewSystem : MonoBehaviour
{
    Rigidbody2D rigid;

    private void Start()
    {
        CollectiveService.GetInstanceAddedSignal("Player")
            .Subscribe(x =>
            {
                rigid = x.GetComponent<Rigidbody2D>();
            });
    }
    private void FixedUpdate()
    {
        var entities = Context<Default>.AllOf<VelocityCompnent>();
        foreach (Entity e in entities)
        {
            var velocityComponent = e.Get<VelocityCompnent>();
            var rigidBodyVelocity_y = rigid.velocity.y;
            //rigid.velocity = new Vector2(velocityComponent.velocity.x, rigidBodyVelocity_y);
            rigid.velocity = velocityComponent.velocity;
        }
    }
}

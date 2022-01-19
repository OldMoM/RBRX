using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Entitas;
public class PlayerGroundedEventListeners : MonoBehaviour
{
    // Start is called before the first frame update
    Transform player;
    CapsuleCollider2D capsule;

    public Vector2 point;
    public BoolReactiveProperty isGround = new BoolReactiveProperty();
    public float bottom;
    public float ceiling;
    private float height;
    private float radius;

    private float width;

    void Start()
    {
        CollectiveService.GetInstanceAddedSignal("Player")
             .Subscribe(x =>
             {
                 player = x;
                 capsule = x.GetComponent<CapsuleCollider2D>();

                 
                 height = capsule.size.y;
                 width = capsule.size.x * 0.5f;

             });

        isGround.Subscribe(x => { StandardEvents.OnNext(EventList.playerGrounded, transform.name, x); });
    }
    private void LateUpdate()
    {
        var colliders = new List<ContactPoint2D>();
        var filter = new ContactFilter2D();
       
        filter.layerMask = LayerMask.GetMask("Environment");
        filter.useLayerMask = true;
        //capsule.GetContacts(filter, colliders);
        var results = new List<Collider2D>();
        //var point = colliders[0].point;
        var point = new Vector2(player.position.x, player.position.y);
        var size = new Vector2(width, height);

        Physics2D.OverlapBox(point, size, 0,filter,results);

        var pointY_bottom = player.transform.position.y - height / 2f;
        var pointY_ceiling = pointY_bottom + 0.25f * radius;
        var entities = Context<Default>.AllOf<PlayerStateComponent>();

        bottom = pointY_bottom;
        ceiling = pointY_ceiling;


        isGround.Value = results.Count > 0;

        //if (colliders.Count == 0)
        //{
        //    foreach (Entity e in entities)
        //    {
        //        isGround.Value = false;
        //        return;
        //    }
        //}

        //foreach (ContactPoint2D contact in colliders)
        //{
        //    point = contact.point;
        //    if (point.y >= pointY_bottom && point.y < pointY_ceiling)
        //    {
        //        isGround.Value = true;
        //        return;
        //    }
        //    else
        //    {
        //        StandardEvents.OnNext(EventList.playerGrounded, transform.name, false);
        //        isGround .Value = false;

        //    }
        //}
    }
}

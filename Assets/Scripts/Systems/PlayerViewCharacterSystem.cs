using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Entitas;

public class PlayerViewCharacterSystem : MonoBehaviour
{
    CharacterController controller;
    public bool isGround;
    // Start is called before the first frame update
    void Start()
    {
        CollectiveService.GetInstanceAddedSignal("PlayerCharacter")
            .Subscribe(x =>
            {
                controller = x.GetComponent<CharacterController>();
            });
    }

    // Update is called once per frame
    void Update()
    {
        var entities = Context<Default>.AllOf<VelocityCompnent, PlayerStateComponent>();
        foreach (Entity e in entities)
        {
            var velocity_com = e.Get<VelocityCompnent>();
            controller.Move(new Vector3(velocity_com.velocity.x, 0, 0) * Time.deltaTime);
        }
        isGround = controller.isGrounded;
    }
}

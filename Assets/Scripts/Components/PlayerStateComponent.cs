using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using UniRx;

public class PlayerStateComponent : IComponent
{
    public ReactiveProperty<PlayerState> state = new ReactiveProperty<PlayerState>(PlayerState.IDLE);
    public bool isGrounded = false;
    public ReactiveProperty<string> inputEvent = new ReactiveProperty<string>();

    public PlayerStateComponent()
    {
        state.Subscribe(x =>
        {
            StandardEvents.OnNext(EventList.playerState, "PlayerStateComponent", x);   
        });
        inputEvent.Subscribe(x =>
        {
            StandardEvents.OnNext("PlayerInput", "PlayerStateComponent", x);
        });
    }
    
}
public enum PlayerState
{
    IDLE,
    WALK,
    JUMP,
    DIED,
    FALL,
    SPRINT,
    ANY,
}

public struct PlayerStateSpace
{
    public PlayerState state;
    public bool isGround;

    static readonly PlayerStateSpace @default = new PlayerStateSpace();
    public static PlayerStateSpace Default => @default;

    public PlayerStateSpace(PlayerState m_state,bool m_isGround)
    {
        state = m_state;
        isGround = m_isGround;
    }

    public PlayerStateSpace(PlayerStateComponent component)
    {
        state    = component.state.Value;
        isGround = component.isGrounded;
    }
}

using System.Collections;
using System.Collections.Generic;
using System;

public static class PlayerStateFilterTables 
{
    public static PlayerStateSpace Space(PlayerState state,bool isGround)
    {
        return new PlayerStateSpace(state, isGround);
    }
    static PlayerState Idle => PlayerState.IDLE;
    static PlayerState Walk => PlayerState.WALK;
    static PlayerState Jump => PlayerState.JUMP;
    static PlayerState Fall => PlayerState.FALL;
    static PlayerState Sprint => PlayerState.SPRINT;

    public readonly static Dictionary<(PlayerStateSpace, PlayerStateSpace), bool> filterTable = new Dictionary<(PlayerStateSpace, PlayerStateSpace), bool>()
    {
        //CurrentState-----------------InputState-------- TargetState------
        {(Space(PlayerState.IDLE,true),Space(PlayerState.WALK,true)) ,true},
        {(Space(PlayerState.IDLE,true),Space(PlayerState.JUMP,false)),true},

        {(Space(PlayerState.IDLE,true),Space(PlayerState.SPRINT,true)),true},


        {(Space(PlayerState.WALK,true)   ,Space(PlayerState.IDLE,true)) ,true},
        {(Space(PlayerState.WALK,true)   ,Space(PlayerState.JUMP,false)),true},
        {(Space(PlayerState.WALK,true)   ,Space(PlayerState.SPRINT,true)),true},

        {(Space(PlayerState.FALL,false)  ,Space(PlayerState.IDLE,true)),true},
        {(Space(PlayerState.FALL,false)  ,Space(PlayerState.WALK,true)),true},
        {(Space(PlayerState.FALL,false)  ,Space(PlayerState.SPRINT,false)),true},

        {(Space(PlayerState.SPRINT,true) ,Space(PlayerState.IDLE,true)),true},
        {(Space(PlayerState.SPRINT,false),Space(PlayerState.FALL,false)),true},
    };
    public readonly static Dictionary<((PlayerState, bool), (PlayerState, bool)), (PlayerState, bool)> filter = new Dictionary<((PlayerState, bool), (PlayerState, bool)), (PlayerState, bool)>()
    {
       //CurrentState---Input---OutputState-------------
        {((Idle,true)   ,(Walk,true))   ,(Walk  ,true )},
        {((Idle,true)   ,(Jump,true))   ,(Jump  ,false)},
        {((Idle,true)   ,(Sprint,true)) ,(Sprint,true )},
        {((Idle,false)  ,(Fall,false))  ,(Fall,false)},
       //-----------------------------------------------
        {((Walk,true)   ,(Idle,true))   ,(Idle,  true )},
        {((Walk,true)   ,(Jump,true ))  ,(Jump  ,false)},
        {((Walk,true)   ,(Sprint,true )),(Sprint,true)},
        {((Walk,false)  ,(Fall,false))  ,(Fall,false  )},
       //------------------------------------------------
        {((Fall,true)  ,(Idle,true))  ,(Idle  ,true  )},
        {((Fall,true)  ,(Walk,true))  ,(Walk  ,true  )},
        {((Fall,false)  ,(Sprint,false)) ,(Sprint,false)},
        //-----------------------------------------------
        {((Sprint,true),(Idle,true)), (Idle,true) },
        {((Sprint,false),(Idle,false)),(Fall,false) },
        {((Sprint,false),(Walk,false)),(Fall,false) },
        {((Sprint,true),(Walk,true)),(Walk,true) },
        //在空中一进入冲刺状态，会自动转到Fall状态
    };

    public readonly static TransformTable<PlayerState> table = new TransformTable<PlayerState>()
    {
        {((Idle,true) ,"ToWalk" ) ,Walk },
        {((Idle,true) ,"ToJump" ) ,Jump },
        {((Idle,false),"Fall"   ) ,Fall },


        {((Walk,true) ,"ToIdle" ) ,Idle },
        {((Walk,true) ,"ToJump" ) ,Jump },
        {((Walk,false) ,"ToFall" ) ,Fall },

        {((Jump,true),"JumpEnd") ,Fall },

        {((Fall,true) ,"ToIdle" ) ,Idle },
        {((Fall,true) ,"ToWalk" ) ,Walk },
    };
}   

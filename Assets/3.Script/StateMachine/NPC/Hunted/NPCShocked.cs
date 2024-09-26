using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCShocked : BaseState
{
    private Transform npc;
    private NPCStateMachine npcStateMachine;

    public NPCShocked(StateMachine stateMachine, Transform _player) : base("NPCShocked", stateMachine)
    {

        this.npc = _player;
        this.npcStateMachine = (NPCStateMachine)stateMachine;
    }

    public override void Enter()
    {
        npcStateMachine.PlayAnimation("Shocked");
        
    }

    public override void Update()
    {
        //  npcStateMachine.ChangeState(new PlayerAttack_Start(stateMachine, player));


    }

    public override void Exit()
    {
        // Exit ·ÎÁ÷
    }


}




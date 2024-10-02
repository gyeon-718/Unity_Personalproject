using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCIdle : BaseState
{
    private Transform npc;
    private NPCStateMachine npcStateMachine;

    public NPCIdle(StateMachine stateMachine, Transform _player) : base("NPCIdle", stateMachine)
    {

        this.npc = _player;
        this.npcStateMachine = (NPCStateMachine)stateMachine;
    }

    public override void Enter()
    {
        npcStateMachine.PlayAnimation("Idle");
    }

    public override void Update()
    {
        if (npcStateMachine.playerStatemachine.npc == this.npc
            && npcStateMachine.playerStatemachine.isWarningEnd)
        {
            npcStateMachine.ChangeState(new NPCShocked(stateMachine, npc));
        }



    }

    public override void Exit()
    {
        // Exit ·ÎÁ÷
    }


}




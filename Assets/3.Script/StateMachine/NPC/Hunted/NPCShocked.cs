using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCShocked : BaseState
{
    private Transform npc;
    private NPCStateMachine npcStateMachine;

    public NPCShocked(StateMachine stateMachine, Transform _npc) : base("NPCShocked", stateMachine)
    {

        this.npc = _npc;
        this.npcStateMachine = (NPCStateMachine)stateMachine;
    }

    public override void Enter()
    {
        npcStateMachine.PlayAnimation("Shocked");
        
    }

    public override void Update()
    {
      if(npcStateMachine.npcType==NPCType.DEAD)
        {
            npcStateMachine.ChangeState(new NPCDead(stateMachine, npc));
        }


    }

    public override void Exit()
    {

    }


}




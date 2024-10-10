using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWalk : BaseState
{
    private Transform npc;
    private NPCStateMachine npcStateMachine;
  //  private Transform targetPoint;


    public NPCWalk(StateMachine stateMachine, Transform _player) : base("NPCIdle", stateMachine)
    {

        this.npc = _player;
        this.npcStateMachine = (NPCStateMachine)stateMachine;
    }


    public override void Enter()
    {
        npcStateMachine.PlayAnimation("Walk");
        Debug.Log("워크");
        npcStateMachine.selectPoint = WaypointManager.instance.GetRandomPoint();
      //  npcStateMachine.selectPoint = targetPoint;
        npcStateMachine.npcNavmesh.SetDestination(npcStateMachine.selectPoint.position);

    }

    public override void Update()
    {
        if (npcStateMachine.playerStatemachine.npc == this.npc
    && npcStateMachine.playerStatemachine.isWarningEnd)
        {
            npcStateMachine.npcNavmesh.isStopped = true; // 목적지 이동 중지
            npcStateMachine.ChangeState(new NPCShocked(stateMachine, npc));
        }
        else
        {
            if(Vector3.Distance(npc.transform.position, npcStateMachine.selectPoint.position)<=0.5f)
            {
                npcStateMachine.ChangeState(new NPCTyping(stateMachine, npc));
            }
        }


    }

    public override void Exit()
    {
    }
}

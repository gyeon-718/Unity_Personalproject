using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCIdle : BaseState
{
    private Transform npc;
    private NPCStateMachine npcStateMachine;

    private float currenttime = 0;
    private int randomSecond;


    public NPCIdle(StateMachine stateMachine, Transform _player) : base("NPCIdle", stateMachine)
    {

        this.npc = _player;
        this.npcStateMachine = (NPCStateMachine)stateMachine;
    }

    public override void Enter()
    {
        npcStateMachine.PlayAnimation("Idle");
        randomSecond = Random.Range(5, 10);
        Debug.Log(randomSecond);
    }

    public override void Update()
    {
        currenttime += Time.deltaTime;
        if (npcStateMachine.playerStatemachine.npc == this.npc
            && npcStateMachine.playerStatemachine.isWarningEnd)
        {
            npcStateMachine.ChangeState(new NPCShocked(stateMachine, npc));
        }
        else
        {
            if (currenttime >= randomSecond)

            {
                npcStateMachine.ChangeState(new NPCWalk(stateMachine, npc));
            }
        }


    }

    public override void Exit()
    {
        currenttime = 0;
    }


}




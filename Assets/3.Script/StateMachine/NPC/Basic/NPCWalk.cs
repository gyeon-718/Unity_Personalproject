using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWalk : BaseState
{
    private Transform npc;
    private NPCStateMachine npcStateMachine;
    public NPCWalk(StateMachine stateMachine, Transform _player) : base("NPCIdle", stateMachine)
    {

        this.npc = _player;
        this.npcStateMachine = (NPCStateMachine)stateMachine;
    }


    public override void Enter()
    {
        npcStateMachine.PlayAnimation("Walk");
        Debug.Log("워크");
    }

    public override void Update()
    {




    }

    public override void Exit()
    {
        // Exit 로직
    }
}

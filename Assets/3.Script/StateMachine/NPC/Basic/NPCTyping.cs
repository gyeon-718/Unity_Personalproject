using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTyping : BaseState
{
    private Transform npc;
    private NPCStateMachine npcStateMachine;
    private float currentTime;

    public NPCTyping(StateMachine stateMachine, Transform _player) : base("NPCTyping", stateMachine)
    {

        this.npc = _player;
        this.npcStateMachine = (NPCStateMachine)stateMachine;
    }

    public override void Enter()
    {
        npcStateMachine.PlayAnimation("Typing");
        currentTime = 0;
    }

    public override void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime>=15.0f)
        {
            npcStateMachine.ChangeState(new NPCWalk(stateMachine, npc));
        }

    }

    public override void Exit()
    {
        npcStateMachine.ReturnPoint(npcStateMachine.selectPoint);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDead : BaseState
{
    private Transform npc;
    private NPCStateMachine npcStateMachine;

    public NPCDead(StateMachine stateMachine, Transform _player) : base("NPCDead", stateMachine)
    {

        this.npc = _player;
        this.npcStateMachine = (NPCStateMachine)stateMachine;
    }

    public override void Enter()
    {
        npcStateMachine.deadBody.gameObject.SetActive(true);
        npcStateMachine.aliveBody.gameObject.SetActive(false);
        Debug.Log("Á×À½");

    }

    public override void Update()
    {


    }

    public override void Exit()
    {
        // Exit ·ÎÁ÷
    }
}

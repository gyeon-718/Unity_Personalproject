using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack_Start : BaseState
{
    private Transform player;
    private PlayerStateMachine playerStateMachine;
    private Transform targetNPC;
    private NPCStateMachine npc;


    public PlayerAttack_Start(StateMachine stateMachine, Transform _player) : base("PlayerAttack_Start", stateMachine)
    {

        this.player = _player;
        this.playerStateMachine = (PlayerStateMachine)stateMachine;
    }

    public override void Enter()
    {
        playerStateMachine.playereye_ani.SetBool("isCrazy", true);
        playerStateMachine.PlayAnimation("Attack_Start");
    }

    public override void Update()
    {

        if (playerStateMachine.isAnimationEnd("Attack_Start"))
        {
            playerStateMachine.ChangeState(new PlayerAttack_Running(stateMachine, player));
        }
    }

    public override void Exit()
    {
        // Exit ·ÎÁ÷
    }


}




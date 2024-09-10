using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack_Start : BaseState
{
    private Transform player;
    private PlayerStateMachine playerStateMachine;


    public PlayerAttack_Start(StateMachine stateMachine, Transform _player) : base("PlayerAttack_Start", stateMachine)
    {

        this.player = _player;
        this.playerStateMachine = (PlayerStateMachine)stateMachine;
    }

    public override void Enter()
    {
        playerStateMachine.PlayAnimation("Attack_Start");
        // 애니메이션
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
        // Exit 로직
    }


}




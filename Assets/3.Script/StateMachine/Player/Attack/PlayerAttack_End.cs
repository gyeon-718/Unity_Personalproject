using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack_End : BaseState
{
    private Transform player;
    private PlayerStateMachine playerStateMachine;


    public PlayerAttack_End(StateMachine stateMachine, Transform _player) : base("PlayerAttack_End", stateMachine)
    {

        this.player = _player;
        this.playerStateMachine = (PlayerStateMachine)stateMachine;
    }

    public override void Enter()
    {
        playerStateMachine.PlayAnimation("Attack_End");
        // 애니메이션
    }

    public override void Update()
    {

        if (playerStateMachine.isAnimationEnd("Attack_End"))
        {
            playerStateMachine.ChangeState(new PlayerIdle(stateMachine, player));
        }
    }

    public override void Exit()
    {
        ScreenManager.instance.KillingScreen_Active();
    }


}




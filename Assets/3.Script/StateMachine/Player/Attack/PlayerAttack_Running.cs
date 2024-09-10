using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack_Running : BaseState
{
    private Transform player;
    private PlayerStateMachine playerStateMachine;

    public PlayerAttack_Running(StateMachine stateMachine, Transform _player) : base("PlayerAttack_Running", stateMachine)
    {

        this.player = _player;
        this.playerStateMachine = (PlayerStateMachine)stateMachine;
    }

    public override void Enter()
    {
        playerStateMachine.PlayAnimation("Attack_Running");
        
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        // Exit ·ÎÁ÷
    }


}




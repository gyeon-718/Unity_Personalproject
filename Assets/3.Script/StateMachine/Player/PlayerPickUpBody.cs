using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpBody : BaseState
{
    private PlayerStateMachine playerStateMachine;
    private Transform player;

    public PlayerPickUpBody(StateMachine stateMachine, Transform player) : base("PlayerPickUpBody", stateMachine)
    {
        this.player = player;
        this.playerStateMachine = (PlayerStateMachine)stateMachine;
    }

    public override void Enter()
    {
        playerStateMachine.PlayAnimation("PickUp_Body");
        Debug.Log("픽업 들어온다");
    }

    public override void Update()
    {
        if (playerStateMachine.isAnimationEnd("PickUp_Body"))
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                playerStateMachine.ChangeState(new PlayerCarryBody(stateMachine, player));
            }
        }

    }

    public override void Exit()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpBody : BaseState
{
    private float moveSpeed = 2.5f;
    private float rotateSpeed = 5f;

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
    }

    public override void Update()
    {
      

        



    }

    public override void Exit()
    {

    }
}

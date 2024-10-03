using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarryBody : BaseState
{
    private float moveSpeed = 2.5f;
    private float rotateSpeed = 5f;

    private PlayerStateMachine playerStateMachine;
    private Transform player;
    public PlayerCarryBody(StateMachine stateMachine, Transform player) : base("PlayerCarryBody", stateMachine)
    {
        this.player = player;
        this.playerStateMachine = (PlayerStateMachine)stateMachine;
    }

    public override void Enter()
    {
        playerStateMachine.PlayAnimation("CarryBody_Walk");
    }

    public override void Update()
    {
        Walk();

        if(Input.GetKeyDown(KeyCode.E))
        {

        }

        else if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            playerStateMachine.ChangeState(new PlayerIdle(stateMachine, player));
        }
    }

    public override void Exit()
    {
        
    }
    private void Walk()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(horizontal, 0, vertical);
        moveDir = moveDir.normalized;

        moveDir = Quaternion.Euler(0, 45, 0) * moveDir;

        if (moveDir != Vector3.zero)
        {
            player.forward = Vector3.Slerp(player.forward, moveDir, Time.deltaTime * rotateSpeed);
            player.position += moveDir * moveSpeed * Time.deltaTime;
        }
    }
}

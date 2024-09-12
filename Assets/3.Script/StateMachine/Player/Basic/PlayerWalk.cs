using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : BaseState
{
    private float moveSpeed = 3f;
    private float rotateSpeed = 5f;

    private PlayerStateMachine playerStateMachine;
    private Transform player;

    public PlayerWalk(StateMachine stateMachine, Transform player) : base("PlayerWalk", stateMachine)
    {
        this.player = player;
        this.playerStateMachine = (PlayerStateMachine)stateMachine;
    }

    public override void Enter()
    {
        playerStateMachine.PlayAnimation("Walk");
    }

    public override void Update()
    {
        Walk();
        if (playerStateMachine.isWarningEnd)
        {
            playerStateMachine.ChangeState(new PlayerAttack_Start(stateMachine, player));
        }
        else
        {

            if (Input.GetMouseButton(0))
            {
                switch (playerStateMachine.playerType)
                {
                    case PlayerType.Vacuum:
                        playerStateMachine.ChangeState(new PlayerVacuum(stateMachine, player));
                        break;
                    case PlayerType.Wash:
                        playerStateMachine.ChangeState(new PlayerWash(stateMachine, player));
                        break;

                }
            }

            else if (Input.GetKeyDown(KeyCode.Q))
            {
                playerStateMachine.player_ani.SetTrigger("isSwitching");
                switch (playerStateMachine.playerType)
                {
                    case PlayerType.Wash:
                        playerStateMachine.playerType = PlayerType.Vacuum;
                        playerStateMachine.vacuumNozzle.SetActive(true);
                        playerStateMachine.washerNozzle.SetActive(false);
                        break;
                    case PlayerType.Vacuum:
                        playerStateMachine.playerType = PlayerType.Wash;
                        playerStateMachine.vacuumNozzle.SetActive(false);
                        playerStateMachine.washerNozzle.SetActive(true);
                        break;
                }
            }

            else if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            {
                playerStateMachine.ChangeState(new PlayerIdle(stateMachine, player));
            }

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
        if (moveDir != Vector3.zero)
        {
            player.forward = Vector3.Slerp(player.forward, moveDir, Time.deltaTime * rotateSpeed);
            player.position += moveDir * moveSpeed * Time.deltaTime;
        }
    }

}




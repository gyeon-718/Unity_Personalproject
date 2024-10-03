using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : BaseState
{
    private Transform player;
    private PlayerStateMachine playerStateMachine;
    private NPCStateMachine npc;

    public PlayerIdle(StateMachine stateMachine, Transform _player) : base("PlayerIdle", stateMachine)
    {

        this.player = _player;
        this.playerStateMachine = (PlayerStateMachine)stateMachine;
    }

    public override void Enter()
    {
        playerStateMachine.PlayAnimation("Idle");
        // npc = playerStateMachine.npc.GetComponent<NPCStateMachine>();
    }

    public override void Update()
    {
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
            else if (playerStateMachine.isCarryingDeadBody)
            {

                playerStateMachine.ChangeState(new PlayerPickUpBody(stateMachine, player));

            }
            else
            {
                if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                {
                    if (!playerStateMachine.isCarryingDeadBody)
                    {
                        playerStateMachine.ChangeState(new PlayerWalk(stateMachine, player));
                    }
                    else
                    {
                        playerStateMachine.ChangeState(new PlayerCarryBody(stateMachine, player));
                    }

                }
            }

        }
    }

    public override void Exit()
    {
        // Exit ·ÎÁ÷
    }


}




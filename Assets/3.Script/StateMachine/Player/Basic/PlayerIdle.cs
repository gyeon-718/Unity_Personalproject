using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : BaseState
{
    private Transform player;
    private PlayerStateMachine playerStateMachine;

    public PlayerIdle(StateMachine stateMachine, Transform _player) : base("PlayerIdle", stateMachine)
    {

        this.player = _player;
        this.playerStateMachine = (PlayerStateMachine)stateMachine;
    }

    public override void Enter()
    {
        playerStateMachine.PlayAnimation("Idle");
    }

    public override void Update()
    {
       //if(playerStateMachine.transform.position-)
        
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

        else
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                playerStateMachine.ChangeState(new PlayerWalk(stateMachine, player));
            }
        }
    }

    public override void Exit()
    {
        // Exit ·ÎÁ÷
    }


}




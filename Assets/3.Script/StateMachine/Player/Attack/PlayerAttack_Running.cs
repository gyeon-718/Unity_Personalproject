using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack_Running : BaseState
{

    private PlayerStateMachine playerStateMachine;
    private Transform player;
    public PlayerAttack_Running(StateMachine stateMachine, Transform _player) : base("PlayerAttack_Running", stateMachine)
    {
        this.player = _player;
        this.playerStateMachine = (PlayerStateMachine)stateMachine;

    }

    public override void Enter()
    {
        playerStateMachine.PlayAnimation("Attack_Running");
        //Debug.Log("러닝");
        playerStateMachine.navmeshAgent.enabled = true;
       // playerStateMachine.player.LookAt(playerStateMachine.npc.position);
        playerStateMachine.navmeshAgent.SetDestination(playerStateMachine.npc.position);
    }

    public override void Update()
    {
        // float newDistanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        // 플레이어가 npc랑 가까워지면
        if (Vector3.Distance(playerStateMachine.player.transform.position, playerStateMachine.npc.position) < 1f) 
        {
            playerStateMachine.ChangeState(new PlayerAttack_End(stateMachine, player));
        }
    }


    public override void Exit()
    {
        playerStateMachine.navmeshAgent.enabled = false;
    }


}




using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack_End : BaseState
{
    private Transform player;
    private PlayerStateMachine playerStateMachine;
    private RangeView npcRange;
    private NPCStateMachine npc;

    public PlayerAttack_End(StateMachine stateMachine, Transform _player) : base("PlayerAttack_End", stateMachine)
    {

        this.player = _player;
        this.playerStateMachine = (PlayerStateMachine)stateMachine;
    }

    public override void Enter()
    {
        playerStateMachine.PlayAnimation("Attack_End");
        ScreenManager.instance.KillingScreen_Active();
        npcRange = playerStateMachine.npc.GetComponent<RangeView>();
        npc = playerStateMachine.npc.GetComponent<NPCStateMachine>();  // 엔피씨 트랜스폼의 스테이트머신 가져와서 타입 바꾸기
        npc.npcType = NPCType.DEAD;
        //    Debug.Log("어택앤드");
        // 애니메이션
    }

    public override void Update()
    {
        AnimatorStateInfo state = ScreenManager.instance.killingAni.GetCurrentAnimatorStateInfo(0);
        if (state.normalizedTime >= 1.0f)
        {
            //   Debug.Log("넘어간다");
            playerStateMachine.ChangeState(new PlayerIdle(stateMachine, player));
        }
    }

    public override void Exit()
    {
        ScreenManager.instance.WarningScreen_Disactive();
        ScreenManager.instance.KillingScreen_Disactive();
        ScreenManager.instance.npcList.Remove(npcRange);
        playerStateMachine.isWarningEnd = false;

        // playerStateMachine.npc.gameObject.SetActive(false);  // 임시
        playerStateMachine.playereye_ani.SetBool("isCrazy", false);





    }


}




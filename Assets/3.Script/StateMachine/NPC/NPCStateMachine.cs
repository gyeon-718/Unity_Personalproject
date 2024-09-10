using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateMachine : StateMachine
{
    public Transform npc;
    public Animator npc_ani;

 //   protected override BaseState GetInitialState()
 //   {
 //       return new PlayerIdle(this, npc);
 //   }
    private void Start()
    {
        base.Start();
    }

    private void Update()
    {
        base.Update();
    }


    public void PlayAnimation(string animationName, float fixedTransitionDuration = 0.25f)
    {
        npc_ani.CrossFadeInFixedTime(animationName, fixedTransitionDuration);
    }

    public bool isAnimationEnd(string animationName)
    {
        AnimatorStateInfo stateInfo = npc_ani.GetCurrentAnimatorStateInfo(0);
        Debug.Log(stateInfo);
        return stateInfo.IsName(animationName) && stateInfo.normalizedTime >= 1.0f;
    }
}

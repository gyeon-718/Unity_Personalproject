using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum NPCType
{
    ALIVE,
    DEAD
};
public class NPCStateMachine : StateMachine
{
    public Transform npc;
    public Transform aliveBody;
    public Transform deadBody;
    public Animator npc_ani;
    public PlayerStateMachine playerStatemachine;
    public NPCType npcType;

    public List<Transform> npcTargetPoints;  // 모든 타겟포인트 리스트
    private List<Transform> canTargetingPoints;  // 사용할 수 있는 타겟팅 리스트

  [HideInInspector]  public NavMeshAgent npcNavmesh;
    public Transform selectPoint;
    public bool isPickedUpByPlayer=false;

    protected override BaseState GetInitialState()
    {
        return new NPCIdle(this, npc);
    }
    private void Start()
    {
        playerStatemachine = FindObjectOfType<PlayerStateMachine>();
        npcNavmesh = GetComponent<NavMeshAgent>();
        npcType = NPCType.ALIVE;
        selectPoint = null;

        canTargetingPoints = new List<Transform>(npcTargetPoints);
        base.Start();
    }

    private void Update()
    {
        base.Update();
       // Debug.Log(npcType);
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

    public void OnDead()
    {
        Destroy(gameObject);
    }

    public Transform GetRandomPoint()  // 
    {
        if(canTargetingPoints.Count==0)
        {
            return null;
        }
        int randomIndex = Random.Range(0, canTargetingPoints.Count);
        Transform selectPoint = canTargetingPoints[randomIndex];
        canTargetingPoints.RemoveAt(randomIndex);
        return selectPoint;
    }

    public void ReturnPoint(Transform wayPoint)
    {
        if(!canTargetingPoints.Contains(wayPoint))
        {
            canTargetingPoints.Add(wayPoint);
        }    
    }


}

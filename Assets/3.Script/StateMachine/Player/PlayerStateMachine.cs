using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum PlayerType
{
    Vacuum,
    Wash
};
public class PlayerStateMachine : StateMachine
{
    public Transform player;
    public Transform npc;  // 추적하기 위해..
    public Animator player_ani;

    [HideInInspector] public Animator playereye_ani;
    public PlayerType playerType;
    public ParticleSystem vacuumParticle;
    public ParticleSystem waterParticle;
    public ParticleSystem Water_groundHit;
    public ParticleSystem Water_wallHit;
    public GameObject[] npcs; // 맵에 있는 npc들 담아놓을 배열

    public GameObject vacuumNozzle;
    public GameObject washerNozzle;

    public GameObject cleanRange;
    [HideInInspector] public NavMeshAgent navmeshAgent;
  
    
    [HideInInspector] public bool isCleanRangeActive = false;
    [HideInInspector] public bool isWarningEnd = false;
    [HideInInspector] public bool isCarryingDeadBody = false;   // 시체 들어올렸을 때 상태전환
    protected override BaseState GetInitialState()
    {
        return new PlayerIdle(this, player);
    }
    private void Start()
    {
        playerType = PlayerType.Vacuum;
        player_ani = GetComponent<Animator>();
        npcs = GameObject.FindGameObjectsWithTag("NPC");
        navmeshAgent = GetComponentInChildren<NavMeshAgent>();
        navmeshAgent.enabled = false;

        base.Start();
    }

    private void Update()
    {
        base.Update();

    }


    public void PlayAnimation(string animationName, float fixedTransitionDuration = 0.25f)
    {
        player_ani.CrossFadeInFixedTime(animationName, fixedTransitionDuration);
    }

    public bool isAnimationEnd(string animationName)
    {
        AnimatorStateInfo stateInfo = player_ani.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(animationName) && stateInfo.normalizedTime >= 1.0f;
    }
}

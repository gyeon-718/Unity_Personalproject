using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeView : MonoBehaviour
{
    // 시야 영역의 반지름과 시야 각도
    public float viewRadius;

    [Range(0, 360)]
    public float viewAngle_Warn;

    public float viewAngle_Direct;

    // 마스크 2종
    public LayerMask targetMask, obstacleMask;

    // Target mask에 속하고, ray hit된 transform을 보관하는 리스트
    public List<Transform> visibleTargets = new List<Transform>();

    private PlayerStateMachine player;
    private NPCStateMachine npcStatemachine;
    bool playerDetected = false; // 플레이어 감지 여부
    void Start()
    {
        player = FindObjectOfType<PlayerStateMachine>();
        npcStatemachine = GetComponent<NPCStateMachine>();
        // 0.2초 간격으로 코루틴 호출
        StartCoroutine(FindTargetsWithDelay(0.2f));
    }

    private void Update()
    {
        if (ScreenManager.instance.npcList.Count != 0)
        {
            ScreenManager.instance.WarningScreen_Active(); // 경고 스크린 활성화
                                                           //  Debug.Log("플레이어 감지: 경고 스크린 활성화");
                                                           //    Debug.Log(ScreenManager.instance.npcList.Count);
        }
        else
        {
            ScreenManager.instance.WarningScreen_Disactive(); // 경고 스크린 비활성화
                                                              //  Debug.Log("플레이어 감지 해제: 경고 스크린 비활성화");
            Debug.Log(ScreenManager.instance.npcList.Count);
        }


    }



    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets() // 시야 내 타겟을 찾는 메서드
    {
        visibleTargets.Clear(); // 기존 타겟 리스트 초기화
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask); // 시야 내 타겟 찾기


        // 타겟 탐지
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform; // 타겟 Transform
            Vector3 dirToTarget = (target.position - transform.position).normalized; // 타겟 방향 벡터

            // 시야 각도 내에 있는지 확인
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle_Warn / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position); // 타겟과의 거리 계산

                // 장애물에 가려지지 않으면 감지
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    Debug.DrawRay(transform.position, dirToTarget, Color.red, dstToTarget);
                    visibleTargets.Add(target); // 감지된 타겟 리스트에 추가
                    if (!ScreenManager.instance.npcList.Contains(this)) ScreenManager.instance.npcList.Add(this);
                    //------------------------------------------------------------------------------------------------------------------여기까진 잘돼.....
                }

            }
            else
            {
                Debug.Log("리스트제거");
                if (ScreenManager.instance.npcCount == 0) return;
                if (ScreenManager.instance.npcList.Contains(this)) visibleTargets.Remove(target);

                ScreenManager.instance.npcList.Remove(this);
                Debug.Log(ScreenManager.instance.npcList.Count + "11");
            }

            if (visibleTargets.Count != 0)
            { // 플레이어 감지됨
                player.npc = gameObject.transform; // 플레이어가 NPC를 추적하도록 설정          
            }

        }
        // 감지 여부에 따라 경고 스크린 활성화 또는 비활성화 처리
    }

    // y축 오일러 각을 3차원 방향 벡터로 변환한다.
    //angleDegree: 변환할 각도
    //angleIsGlobal: 각도가 글로벌 기준인지 로컬기준인지 여부
    public Vector3 DirFromAngle(float angleDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal) // 로컬기준이면
        {
            angleDegrees += transform.eulerAngles.y;  // y축 회전값을 추가
        }
        // 주어진 각도를 3D 방향벡터로 변환하여 반환    
        return new Vector3(Mathf.Cos((-angleDegrees + 90) * Mathf.Deg2Rad), 0, Mathf.Sin((-angleDegrees + 90) * Mathf.Deg2Rad));
    }
}
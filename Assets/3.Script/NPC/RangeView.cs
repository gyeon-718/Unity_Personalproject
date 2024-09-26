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

    void Start()
    {
        player = FindObjectOfType<PlayerStateMachine>();
        // 0.2초 간격으로 코루틴 호출
        StartCoroutine(FindTargetsWithDelay(0.2f));
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()  //  시야 내에 타켓들을 찾는 메서드
    {
        visibleTargets.Clear(); // 기존 타겟리스트 초기화

        // viewRadius를 반지름으로 한 원 영역 내 targetMask 레이어인 콜라이더를 모두 가져옴
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        // 탐지된 모든 타겟 콜라이더 탐색
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform; // [i]번째 transform 가져오기 
            Vector3 dirToTarget = (target.position - transform.position).normalized; // 타겟과의 방향벡터 계산

            // 플레이어와 forward와 target이 이루는 각이 설정한 각도 내라면
            // 캐릭터의 앞방향과 타겟 방향 벡터의 각도가 시야 각도 내라면
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle_Warn / 2)
            {
                // 타겟과의 거리 계산
                float dstToTarget = Vector3.Distance(transform.position, target.transform.position);

                // 타겟으로 가는 레이캐스트에 obstacleMask가 걸리지 않으면 visibleTargets에 Add

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                    player.npc = gameObject.transform; // 추적할 npc 설정
                    ScreenManager.instance.WarningScreen_Active();  // UI 경고 -> 애니메이션 재생
                }
            }
        }
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
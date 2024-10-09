using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class WaypointManager : MonoBehaviour
    {
    public static WaypointManager instance = null;
    public List<Transform> allTargetPoints; // 모든 타겟 포인트 리스트
        private List<Transform> canTargettingPoints; // 사용 가능한 타겟 포인트 리스트

        private void Awake()
        {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
            canTargettingPoints = new List<Transform>(allTargetPoints); // 초기화 시 모든 타겟 포인트를 사용 가능하도록 설정
    }

        // 사용 가능한 랜덤 타겟 포인트를 반환하고 리스트에서 제거
        public Transform GetRandomPoint()
        {
            if (canTargettingPoints.Count == 0)
            {
                return null;
            }

            int randomIndex = Random.Range(0, canTargettingPoints.Count);
            Transform selectedPoint = canTargettingPoints[randomIndex];
            canTargettingPoints.RemoveAt(randomIndex); // 선택된 포인트를 리스트에서 제거
            return selectedPoint;
        }

        // 타겟 포인트를 다시 사용 가능하게 설정
        public void ReturnPoint(Transform point)
        {
            if (!canTargettingPoints.Contains(point))
            {
                canTargettingPoints.Add(point);
            }
        }
    }


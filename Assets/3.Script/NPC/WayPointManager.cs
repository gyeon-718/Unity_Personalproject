using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class WaypointManager : MonoBehaviour
    {
    public static WaypointManager instance = null;
    public List<Transform> allTargetPoints; // ��� Ÿ�� ����Ʈ ����Ʈ
        private List<Transform> canTargettingPoints; // ��� ������ Ÿ�� ����Ʈ ����Ʈ

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
            canTargettingPoints = new List<Transform>(allTargetPoints); // �ʱ�ȭ �� ��� Ÿ�� ����Ʈ�� ��� �����ϵ��� ����
    }

        // ��� ������ ���� Ÿ�� ����Ʈ�� ��ȯ�ϰ� ����Ʈ���� ����
        public Transform GetRandomPoint()
        {
            if (canTargettingPoints.Count == 0)
            {
                return null;
            }

            int randomIndex = Random.Range(0, canTargettingPoints.Count);
            Transform selectedPoint = canTargettingPoints[randomIndex];
            canTargettingPoints.RemoveAt(randomIndex); // ���õ� ����Ʈ�� ����Ʈ���� ����
            return selectedPoint;
        }

        // Ÿ�� ����Ʈ�� �ٽ� ��� �����ϰ� ����
        public void ReturnPoint(Transform point)
        {
            if (!canTargettingPoints.Contains(point))
            {
                canTargettingPoints.Add(point);
            }
        }
    }


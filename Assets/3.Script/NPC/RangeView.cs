using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeView : MonoBehaviour
{
    // �þ� ������ �������� �þ� ����
    public float viewRadius;
  
    [Range(0, 360)]
    public float viewAngle_Warn;

    public float viewAngle_Direct;

    // ����ũ 2��
    public LayerMask targetMask, obstacleMask;

    // Target mask�� ���ϰ�, ray hit�� transform�� �����ϴ� ����Ʈ
    public List<Transform> visibleTargets = new List<Transform>();

    private PlayerStateMachine player;

    void Start()
    {
        player = FindObjectOfType<PlayerStateMachine>();
        // 0.2�� �������� �ڷ�ƾ ȣ��
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

    void FindVisibleTargets()  //  �þ� ���� Ÿ�ϵ��� ã�� �޼���
    {
        visibleTargets.Clear(); // ���� Ÿ�ٸ���Ʈ �ʱ�ȭ

        // viewRadius�� ���������� �� �� ���� �� targetMask ���̾��� �ݶ��̴��� ��� ������
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        // Ž���� ��� Ÿ�� �ݶ��̴� Ž��
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform; // [i]��° transform �������� 
            Vector3 dirToTarget = (target.position - transform.position).normalized; // Ÿ�ٰ��� ���⺤�� ���

            // �÷��̾�� forward�� target�� �̷�� ���� ������ ���� �����
            // ĳ������ �չ���� Ÿ�� ���� ������ ������ �þ� ���� �����
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle_Warn / 2)
            {
                // Ÿ�ٰ��� �Ÿ� ���
                float dstToTarget = Vector3.Distance(transform.position, target.transform.position);

                // Ÿ������ ���� ����ĳ��Ʈ�� obstacleMask�� �ɸ��� ������ visibleTargets�� Add

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                    player.npc = gameObject.transform; // ������ npc ����
                    ScreenManager.instance.WarningScreen_Active();  // UI ��� -> �ִϸ��̼� ���
                }
            }
        }
    }

    // y�� ���Ϸ� ���� 3���� ���� ���ͷ� ��ȯ�Ѵ�.
    //angleDegree: ��ȯ�� ����
    //angleIsGlobal: ������ �۷ι� �������� ���ñ������� ����
    public Vector3 DirFromAngle(float angleDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal) // ���ñ����̸�
        {
            angleDegrees += transform.eulerAngles.y;  // y�� ȸ������ �߰�
        }
        // �־��� ������ 3D ���⺤�ͷ� ��ȯ�Ͽ� ��ȯ    
        return new Vector3(Mathf.Cos((-angleDegrees + 90) * Mathf.Deg2Rad), 0, Mathf.Sin((-angleDegrees + 90) * Mathf.Deg2Rad));
    }
}
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
    private NPCStateMachine npcStatemachine;



    void Start()
    {
        player = FindObjectOfType<PlayerStateMachine>();
        npcStatemachine = GetComponent<NPCStateMachine>();
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

    void FindVisibleTargets() // �þ� �� Ÿ���� ã�� �޼���
    {//if(npcStatemachine.)
        visibleTargets.Clear(); // ���� Ÿ�� ����Ʈ �ʱ�ȭ
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask); // �þ� �� Ÿ�� ã��

        // Ÿ�� Ž��
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform; // Ÿ�� Transform
            Vector3 dirToTarget = (target.position - transform.position).normalized; // Ÿ�� ���� ����

            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle_Warn / 2) // �þ� ���� ���� �ִ��� Ȯ��
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position); // Ÿ�ٰ��� �Ÿ� ���

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask)) // ��ֹ��� �������� ������
                {
                    visibleTargets.Add(target); // ������ Ÿ�� ����Ʈ�� �߰�
                    Debug.Log("����");
                    player.npc = gameObject.transform;

                    // ������ Ÿ���� �÷��̾����� Ȯ��
                    if (visibleTargets.Count != 0)
                    {
                        ScreenManager.instance.WarningScreen_Active(); // ��� ��ũ�� Ȱ��ȭ
                        Debug.Log("�÷��̾� ����: ��� ��ũ�� Ȱ��ȭ");
                        if(visibleTargets.Count==0) ScreenManager.instance.WarningScreen_Disactive();
                    }
                    else
                    {
                        ScreenManager.instance.WarningScreen_Disactive(); // ��� ��ũ�� ��Ȱ��ȭ
                        Debug.Log("�÷��̾� ���� ����: ��� ��ũ�� ��Ȱ��ȭ");
                    }
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
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
    bool playerDetected = false; // �÷��̾� ���� ����
    void Start()
    {
        player = FindObjectOfType<PlayerStateMachine>();
        npcStatemachine = GetComponent<NPCStateMachine>();
        // 0.2�� �������� �ڷ�ƾ ȣ��
        StartCoroutine(FindTargetsWithDelay(0.2f));
    }

    private void Update()
    {
        if (ScreenManager.instance.npcList.Count != 0)
        {
            ScreenManager.instance.WarningScreen_Active(); // ��� ��ũ�� Ȱ��ȭ
                                                           //  Debug.Log("�÷��̾� ����: ��� ��ũ�� Ȱ��ȭ");
                                                           //    Debug.Log(ScreenManager.instance.npcList.Count);
        }
        else
        {
            ScreenManager.instance.WarningScreen_Disactive(); // ��� ��ũ�� ��Ȱ��ȭ
                                                              //  Debug.Log("�÷��̾� ���� ����: ��� ��ũ�� ��Ȱ��ȭ");
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

    void FindVisibleTargets() // �þ� �� Ÿ���� ã�� �޼���
    {
        visibleTargets.Clear(); // ���� Ÿ�� ����Ʈ �ʱ�ȭ
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask); // �þ� �� Ÿ�� ã��


        // Ÿ�� Ž��
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform; // Ÿ�� Transform
            Vector3 dirToTarget = (target.position - transform.position).normalized; // Ÿ�� ���� ����

            // �þ� ���� ���� �ִ��� Ȯ��
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle_Warn / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position); // Ÿ�ٰ��� �Ÿ� ���

                // ��ֹ��� �������� ������ ����
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    Debug.DrawRay(transform.position, dirToTarget, Color.red, dstToTarget);
                    visibleTargets.Add(target); // ������ Ÿ�� ����Ʈ�� �߰�
                    if (!ScreenManager.instance.npcList.Contains(this)) ScreenManager.instance.npcList.Add(this);
                    //------------------------------------------------------------------------------------------------------------------������� �ߵ�.....
                }

            }
            else
            {
                Debug.Log("����Ʈ����");
                if (ScreenManager.instance.npcCount == 0) return;
                if (ScreenManager.instance.npcList.Contains(this)) visibleTargets.Remove(target);

                ScreenManager.instance.npcList.Remove(this);
                Debug.Log(ScreenManager.instance.npcList.Count + "11");
            }

            if (visibleTargets.Count != 0)
            { // �÷��̾� ������
                player.npc = gameObject.transform; // �÷��̾ NPC�� �����ϵ��� ����          
            }

        }
        // ���� ���ο� ���� ��� ��ũ�� Ȱ��ȭ �Ǵ� ��Ȱ��ȭ ó��
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRaycast : MonoBehaviour
{
    private RaycastHit rayhit;
    private PlayerStateMachine player;
    public LayerMask layer;   // cleanrange �ν��ؼ� ���ܽ�Ű����..

    private float maxDistance = 5f;
    private float coneAngle = 20f;  // ���� ��� ����
    private int rayCount = 5;  // ����ĳ��Ʈ ����

    private bool isPlayerDetected = false;  //  �÷��̾ �����ƴ°�?


    private void Start()
    {
        player = FindObjectOfType<PlayerStateMachine>();
    }

    private void Update()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
        //  CastConeRay();
        Debug.DrawRay(position,transform.forward * maxDistance, Color.red);

        if (Physics.Raycast(transform.position, transform.forward, out rayhit, maxDistance,~layer))
        {
            if (rayhit.collider.gameObject.tag.Equals("Player"))
            {
                if (!isPlayerDetected) //  �÷��̾� ���� ������ �ȵ��� ���
                {
                    isPlayerDetected = true; //  �÷��̾� ������
                    if (!ScreenManager.instance.iswarningscreenActive)  //  warning ��ũ���� Ȱ��ȭ���� �ʾ��� ��
                    {
                        Debug.Log("�浹");
                        ScreenManager.instance.WarningScreen_Active();  // Ȱ��ȭ
                    }
                    player.npc = gameObject.transform;
                    Debug.Log(player.npc.name);
                }    
            }

        }

        else  // ����ĳ��Ʈ�� �ƹ��͵� �������� �ʾ��� ��
        {
            if(isPlayerDetected) // �÷��̾� ���� ������ �ȵ��� ��
            {
                isPlayerDetected = false; //  �÷��̾� �����ȵ�
                if(ScreenManager.instance.iswarningscreenActive)  // warning ��ũ���� Ȱ��ȭ���� ��
                {
                    ScreenManager.instance.WarningScreen_Disactive(); //��Ȱ��ȭ
                }
            }
        }
    }



    private void CastConeRay()
    {
        // ���� ������� �¿�� ������ ����ĳ��Ʈ �߻�
        for (int i = 0; i < rayCount; i++)
        {
            // ������ ����Ͽ� ������ ����
            float angle = Mathf.Lerp(-coneAngle, coneAngle, (float)i / (rayCount - 1));
            Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;

            // ����ĳ��Ʈ �ð�ȭ
            Debug.DrawRay(transform.position, direction * maxDistance, Color.red);

            // ����ĳ��Ʈ �߻�
            if (Physics.Raycast(transform.position, direction, out RaycastHit rayHit, maxDistance))
            {
                if (rayHit.collider.gameObject.tag.Equals("Player"))
                {

                    Debug.Log("�浹");
                    ScreenManager.instance.WarningScreen_Active();
                   

                }
                else
                {
                    ScreenManager.instance.WarningScreen_Disactive();
               
                }
            }
           // else
           // {
           //     ScreenManager.instance.WarningScreen_Disactive();
           // }
        }
    }
}




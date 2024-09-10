using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRaycast : MonoBehaviour
{
    private RaycastHit rayhit;
    private float _maxDistance = 5f;
    private PlayerStateMachine player;


    private float maxDistance = 5f;
    private float coneAngle = 20f;  // ���� ��� ����
    private int rayCount = 5;  // ����ĳ��Ʈ ����


    private void Update()
    {
        CastConeRay();
        //  Debug.DrawRay(transform.position, transform.forward * _maxDistance, Color.red);
        //
        //  if (Physics.Raycast(transform.position, transform.forward, out rayhit, _maxDistance))
        //  {
        //      if (rayhit.collider.gameObject.tag.Equals("Player"))
        //      {
        //          Debug.Log("�浹");
        //          ScreenManager.instance.WarningScreen_Active();
        //      }
        //      else
        //      {
        //          ScreenManager.instance.WarningScreen_Disactive();
        //      }
        //  }
        //
        //  else
        //  {
        //      ScreenManager.instance.WarningScreen_Disactive();
        //  }
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
            else
            {
                ScreenManager.instance.WarningScreen_Disactive();
            }
        }
    }
}




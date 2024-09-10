using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRaycast : MonoBehaviour
{
    private RaycastHit rayhit;
    private float _maxDistance = 5f;
    private PlayerStateMachine player;


    private float maxDistance = 5f;
    private float coneAngle = 20f;  // 원뿔 모양 각도
    private int rayCount = 5;  // 레이캐스트 개수


    private void Update()
    {
        CastConeRay();
        //  Debug.DrawRay(transform.position, transform.forward * _maxDistance, Color.red);
        //
        //  if (Physics.Raycast(transform.position, transform.forward, out rayhit, _maxDistance))
        //  {
        //      if (rayhit.collider.gameObject.tag.Equals("Player"))
        //      {
        //          Debug.Log("충돌");
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
        // 원뿔 모양으로 좌우로 나눠서 레이캐스트 발사
        for (int i = 0; i < rayCount; i++)
        {
            // 각도를 계산하여 방향을 설정
            float angle = Mathf.Lerp(-coneAngle, coneAngle, (float)i / (rayCount - 1));
            Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;

            // 레이캐스트 시각화
            Debug.DrawRay(transform.position, direction * maxDistance, Color.red);

            // 레이캐스트 발사
            if (Physics.Raycast(transform.position, direction, out RaycastHit rayHit, maxDistance))
            {
               if (rayHit.collider.gameObject.tag.Equals("Player"))
              {
                  Debug.Log("충돌");
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




using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRaycast : MonoBehaviour
{
    private RaycastHit rayhit;
    private PlayerStateMachine player;
    public LayerMask layer;   // cleanrange 인식해서 제외시키려고..

    private float maxDistance = 5f;
    private float coneAngle = 20f;  // 원뿔 모양 각도
    private int rayCount = 5;  // 레이캐스트 개수

    private bool isPlayerDetected = false;  //  플레이어가 감지됐는가?


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
                if (!isPlayerDetected) //  플레이어 감지 갱신이 안됐을 경우
                {
                    isPlayerDetected = true; //  플레이어 감지됨
                    if (!ScreenManager.instance.iswarningscreenActive)  //  warning 스크린이 활성화되지 않았을 때
                    {
                        Debug.Log("충돌");
                        ScreenManager.instance.WarningScreen_Active();  // 활성화
                    }
                    player.npc = gameObject.transform;
                    Debug.Log(player.npc.name);
                }    
            }

        }

        else  // 레이캐스트에 아무것도 감지되지 않았을 때
        {
            if(isPlayerDetected) // 플레이어 감지 갱신이 안됐을 때
            {
                isPlayerDetected = false; //  플레이어 감지안됨
                if(ScreenManager.instance.iswarningscreenActive)  // warning 스크린이 활성화중일 때
                {
                    ScreenManager.instance.WarningScreen_Disactive(); //비활성화
                }
            }
        }
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
           // else
           // {
           //     ScreenManager.instance.WarningScreen_Disactive();
           // }
        }
    }
}




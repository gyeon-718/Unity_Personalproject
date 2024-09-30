using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CineMachine_Set : MonoBehaviour
{
    private CinemachineBlendListCamera blendCamera;
    private GameObject playerFollowing_obj;
    private GameObject enterStage_obj;

    private CinemachineVirtualCameraBase playerFollowing;
    private CinemachineVirtualCameraBase enterStage;
    private StartTile startTile;


    private bool hasEnteredStage = false;
    public bool isEndTalk = false;

    private void Start()
    {
        blendCamera = GetComponent<CinemachineBlendListCamera>();
        blendCamera.m_Loop = false;

        // 버츄얼 카메라 컴포넌트가 들어있는 오브젝트 = 카메라
        playerFollowing_obj = GameObject.Find("PlayerFollowing");
        // 똑같음
        enterStage_obj = GameObject.Find("EnterStage");


        playerFollowing = playerFollowing_obj.GetComponent<CinemachineVirtualCameraBase>(); // 거기서 컴포넌트 불러와
        enterStage = enterStage_obj.GetComponent<CinemachineVirtualCameraBase>();

        startTile = FindObjectOfType<StartTile>();

        SetFollowingCam();
    }



    public void SetFollowingCam()
    {
        playerFollowing_obj.transform.SetParent(this.transform);
        enterStage_obj.transform.SetParent(this.transform);

        blendCamera.m_Instructions[0].m_VirtualCamera = playerFollowing;

        blendCamera.m_Instructions[0].m_Hold = 0.0f;
        blendCamera.m_Instructions[0].m_Blend.m_Time = 1.0f; // 0.5초 동안 전환

    }

    public void SetEnterCam()
    {
        playerFollowing_obj.transform.SetParent(this.transform);
        enterStage_obj.transform.SetParent(this.transform);

        blendCamera.m_Instructions[0].m_VirtualCamera = enterStage;

        blendCamera.m_Instructions[0].m_Hold = 0.0f;
        blendCamera.m_Instructions[0].m_Blend.m_Time = 1.0f; // 0.5초 동안 전환

    }


}


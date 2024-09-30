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

        // ����� ī�޶� ������Ʈ�� ����ִ� ������Ʈ = ī�޶�
        playerFollowing_obj = GameObject.Find("PlayerFollowing");
        // �Ȱ���
        enterStage_obj = GameObject.Find("EnterStage");


        playerFollowing = playerFollowing_obj.GetComponent<CinemachineVirtualCameraBase>(); // �ű⼭ ������Ʈ �ҷ���
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
        blendCamera.m_Instructions[0].m_Blend.m_Time = 1.0f; // 0.5�� ���� ��ȯ

    }

    public void SetEnterCam()
    {
        playerFollowing_obj.transform.SetParent(this.transform);
        enterStage_obj.transform.SetParent(this.transform);

        blendCamera.m_Instructions[0].m_VirtualCamera = enterStage;

        blendCamera.m_Instructions[0].m_Hold = 0.0f;
        blendCamera.m_Instructions[0].m_Blend.m_Time = 1.0f; // 0.5�� ���� ��ȯ

    }


}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxColider : MonoBehaviour
{
    private CineMachine_Set cinemachine;

    private void Start()
    {
        cinemachine = FindObjectOfType<CineMachine_Set>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ScreenManager.instance.ActiveTextBox();
            cinemachine.SetEnterCam();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ScreenManager.instance.DisactiveTextBox();
            cinemachine.SetFollowingCam();
            //Debug.Log("디스액티브 텍스트박스");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningDoor : MonoBehaviour
{
    //엘레베이터 열고 닫기 위한 스크립트
    private Animator doorAni;
    private void Start()
    {
        doorAni = GetComponent<Animator>();
    }


    private void OnTriggerEnter(Collider other)  // 문열기
    {
        if (other.CompareTag("Player"))
        {
            doorAni.SetBool("havetoOpen", true);
        }
    }

    private void OnTriggerExit(Collider other)  // 문닫기
    {
        if (other.CompareTag("Player"))
        {
            doorAni.SetBool("havetoOpen", false);
        }
    }
}

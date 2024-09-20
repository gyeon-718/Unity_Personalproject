using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningDoor : MonoBehaviour
{
    private Animator doorAni;
    private StartTile startTile;

    private void Start()
    {
        doorAni = GetComponent<Animator>();
        startTile = FindObjectOfType<StartTile>();
    }


    private void OnTriggerEnter(Collider other)  // ������
    {
        if (other.CompareTag("Player"))
        {
            doorAni.SetBool("havetoOpen", true);
        }
    }

    private void OnTriggerExit(Collider other)  // ���ݱ�
    {
        if (other.CompareTag("Player"))
        {
            doorAni.SetBool("havetoOpen", false);
        }
    }
}

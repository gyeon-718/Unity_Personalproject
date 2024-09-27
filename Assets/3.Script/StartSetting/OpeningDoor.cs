using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningDoor : MonoBehaviour
{
    //���������� ���� �ݱ� ���� ��ũ��Ʈ
    private Animator doorAni;
    private void Start()
    {
        doorAni = GetComponent<Animator>();
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

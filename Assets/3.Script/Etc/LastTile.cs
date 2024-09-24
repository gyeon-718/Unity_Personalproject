using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastTile : MonoBehaviour
{
    // ���������� ���� �������� �ݶ��̴� ����

    private Animator door_Ani;
    [SerializeField] private GameObject helperUI;
    private bool isOpenedDoor = false;    // UI �ٽ� ���� ����


    private void Start()
    {
        door_Ani = GetComponent<Animator>();
        helperUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")&& !isOpenedDoor)
        {
            helperUI.SetActive(true);
          
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            helperUI.SetActive(false);
            door_Ani.SetBool("havetoOpen", true);
            isOpenedDoor = true;
            ScreenManager.instance.PlayStartingAnimation();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            helperUI.SetActive(false);
        }
    }
}

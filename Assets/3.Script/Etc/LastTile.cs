using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastTile : MonoBehaviour
{
    // 엘레베이터 문을 열기위해 콜라이더 감지

    private Animator door_Ani;
    [SerializeField] private GameObject helperUI;
    private bool isOpenedDoor = false;    // UI 다시 켜짐 방지


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

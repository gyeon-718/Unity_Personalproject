using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc_DetectColider : MonoBehaviour
{
    private PlayerStateMachine player;
    private Collider detectCol;
    private float currentTime = 0;

    private void Start()
    {
        detectCol = GetComponent<Collider>();
        player = FindObjectOfType<PlayerStateMachine>();
        Debug.Log(detectCol);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.isWarning = true;
            ScreenManager.instance.WarningScreen_Active();
            Debug.Log("경고");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentTime += Time.deltaTime;
            if (currentTime > 3.0f)
            {
                Debug.Log("사냥!!!!");
                Debug.Log(player.isWarning);
                ScreenManager.instance.WarningScreen_Disactive();
            ScreenManager.instance.KillingScreen_Active();
               
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentTime = 0;
            player.isWarning = false;
            Debug.Log("나감");

        }
    }
}

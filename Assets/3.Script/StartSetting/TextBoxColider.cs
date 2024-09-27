using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxColider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ScreenManager.instance.ActiveTextBox();
            Debug.Log("액티브 텍스트박스");
        }
    }

   // private void OnTriggerExit(Collider other)
   // {
   //     if(other.CompareTag("Player"))
   //     {
   //         ScreenManager.instance.DisactiveTextBox();
   //         Debug.Log("디스액티브 텍스트박스");
   //     }
   // }
}

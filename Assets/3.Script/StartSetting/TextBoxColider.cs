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
            Debug.Log("��Ƽ�� �ؽ�Ʈ�ڽ�");
        }
    }

   // private void OnTriggerExit(Collider other)
   // {
   //     if(other.CompareTag("Player"))
   //     {
   //         ScreenManager.instance.DisactiveTextBox();
   //         Debug.Log("�𽺾�Ƽ�� �ؽ�Ʈ�ڽ�");
   //     }
   // }
}

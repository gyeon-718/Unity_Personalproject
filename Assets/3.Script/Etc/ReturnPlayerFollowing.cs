using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarReturnPlayerFollowingtTile : MonoBehaviour
{
    public bool isEndTalk = false;


    //private void Start()
    //{
    //    col = GetComponent<Collider>();
    //}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isEndTalk = true;
            //  Debug.Log(isStart);
        }
    }
}

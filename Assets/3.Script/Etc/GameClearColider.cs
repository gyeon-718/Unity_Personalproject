using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearColider : MonoBehaviour
{
    private OpeningDoor door;
    private Animator door_Ani;

    private void Start()
    {
        door = FindObjectOfType<OpeningDoor>();
        door_Ani = door.GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            door_Ani.SetBool("havetoOpen", false);
        }
    }
}

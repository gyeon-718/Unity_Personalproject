using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearColider : MonoBehaviour
{
    private OpeningDoor door;
    private Animator door_Ani;
    private Timer timer;

    public GameObject completeUI;
    public GameObject swipeUI;
    private Animator completeUI_Ani;

    private bool isAnimationEnd = false;

    private void Start()
    {
        door = FindObjectOfType<OpeningDoor>();
        door_Ani = door.GetComponent<Animator>();
        timer = FindObjectOfType<Timer>();
        completeUI_Ani = completeUI.GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            door_Ani.SetBool("havetoOpen", false);
            timer.DisactiveTimer();
            ScreenManager.instance.Invoke("ActiveCompleteUI", 0.6f);
            isAnimationEnd = true;
        }
    }

    private void Update()
    {
        if (isAnimationEnd)
        {
          // ScreenManager.instance.Invoke("ActiveCompleteUI", 0.6f);
           AnimatorStateInfo stateInfo = completeUI_Ani.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.normalizedTime >= 1)
            {
                swipeUI.SetActive(true);
                isAnimationEnd = false;
                SceneManager.LoadScene("LevelSelect");
            }
        }

    }

}

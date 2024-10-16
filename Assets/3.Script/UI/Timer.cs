using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;

    [SerializeField] private float time;
    [SerializeField] private float currentTime;

    [SerializeField] public Transform timer;


    public GameObject completeUI;
    private bool isTimeOut = false;


    private int miniute;
    private int second;

    private void Awake()
    {
        if (timer.gameObject.activeSelf)
        {
            timer.transform.DOLocalMoveY(550, 2f).SetEase(Ease.Unset);
            StartCoroutine(StartTimer());
        }
    }




    private IEnumerator StartTimer()
    {
        currentTime = time;
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            miniute = (int)currentTime / 60;
            second = (int)currentTime % 60;
            timeText.text = miniute.ToString("00") + ":" + second.ToString("00");
            yield return null;

            if (currentTime <= 0)
            {
                currentTime = 0;
                isTimeOut = true;
                yield break;
            }
        }
    }
    private void Update()
    {
        if (isTimeOut)
        {
            DisactiveTimer();
            ScreenManager.instance.Invoke("ActiveTimesUPUI", 0.6f);
            isTimeOut = false;
        }
    }

    public void DisactiveTimer()
    {
        StopCoroutine(StartTimer());

        timer.transform.DOLocalMoveY(700f, 0.5f).SetEase(Ease.Unset);

    }
}

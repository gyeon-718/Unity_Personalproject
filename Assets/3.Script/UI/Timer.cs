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

    [SerializeField] private Transform timer;


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
                yield break;
            }
        }
    }
}

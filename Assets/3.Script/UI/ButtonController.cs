using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ButtonController : MonoBehaviour
{
    public void ActiveScreen(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void DisactiveScreen(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ShowLevelinfo(GameObject levelInfo)
    {
        levelInfo.transform.DOLocalMoveY(-4, 0.5f).SetEase(Ease.Unset);
    }

    public void HideLevelinfo(GameObject levelInfo)
    {
        levelInfo.transform.DOLocalMoveY(2400, 0.5f).SetEase(Ease.Unset);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TextBox : MonoBehaviour
{
    [SerializeField] private GameObject textBox;
    private CanvasGroup canvasGroup;


    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if(textBox.gameObject.activeSelf)
        {
            canvasGroup.DOFade(0, 0.5f).From();
            canvasGroup.transform.DOLocalMove(new Vector2(-2,512), 0.5f).SetEase(Ease.Unset);
        }
    }
    

}

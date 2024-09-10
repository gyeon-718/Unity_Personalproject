using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager instance = null;

    [SerializeField] private GameObject warningScreen;
    [SerializeField] private GameObject killingScreen;

    private Animator killingAni;
    private Image warningImage;
    private Color color;

    public float increaseSpeed = 0.1f;

    private void Start()
    {
        warningScreen.SetActive(false);
        killingScreen.SetActive(false);
        killingAni = killingScreen.GetComponent<Animator>();
        warningImage = warningScreen.GetComponent<Image>();
        color = warningImage.color;
        color.a = 0;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void WarningScreen_Active()
    {
        warningScreen.SetActive(true);
        StartCoroutine(FadeInWarningScreen());
    }

    private IEnumerator FadeInWarningScreen()
    {
        // ��� ȭ���� ���İ��� 0���� 1�� ������ ����
        while (color.a < 1f)
        {
            color.a += increaseSpeed * Time.deltaTime; // ���İ� ����
            color.a = Mathf.Clamp01(color.a); // ���İ��� 0�� 1 ���̷� ����
            warningImage.color = color; // ����� ���İ��� �̹����� ����
            yield return null; // ���� ������ ���
        }
    }


    public void WarningScreen_Disactive()
    {
        color.a = 0;
        warningScreen.SetActive(false);
    }

    public void KillingScreen_Active()
    {
        killingScreen.SetActive(true);

        AnimatorStateInfo state = killingAni.GetCurrentAnimatorStateInfo(0);
        if (state.normalizedTime >= 1.0f)
        {
            killingScreen.SetActive(false);
        }
    }








}

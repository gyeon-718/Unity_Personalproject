using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager instance = null;

    [SerializeField] private GameObject warningScreen;
    [SerializeField] private GameObject killingScreen;
    private PlayerStateMachine player;

    private Animator killingAni;
    private Image warningImage;
    private Color color;

    public float increaseSpeed = 0.1f;

    private void Start()
    {
        player = FindObjectOfType<PlayerStateMachine>();
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
        StartCoroutine(WarningScreen_co());
       

    }

    private IEnumerator WarningScreen_co()
    {
        // ��� ȭ���� ���İ��� 0���� 1�� ������ ����
        while (color.a < 1.0f)
        {
            color.a += increaseSpeed * Time.deltaTime; // ���İ� ����
            color.a = Mathf.Clamp01(color.a); // ���İ��� 0�� 1 ���̷� ����
            warningImage.color = color; // ����� ���İ��� �̹����� ����
            yield return null; // ���� ������ ���
        }
        player.isWarningEnd = true;
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
        if (state.normalizedTime >=0.1f)
        {
            Debug.Log("ų���ִϳ���");
           killingScreen.SetActive(false);
        }
    }

    public void KillingScreen_Disactive()
    {
        killingScreen.SetActive(false);
    }








}

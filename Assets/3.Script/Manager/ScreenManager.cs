using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager instance = null;
    private CanvasGroup canvasGroup;



    [SerializeField] private GameObject warningScreen;
    [SerializeField] private GameObject killingScreen;
    [SerializeField] private GameObject startingScreen;
    [SerializeField] private GameObject timer;
    [SerializeField] private GameObject textBox;
    private PlayerStateMachine player;
    private LastTile lastTile;
    public bool iswarningscreenActive = false;

    public Animator killingAni;
    [SerializeField]private Animator startAni;
    private Image warningImage;
    private Color color;

    public float increaseSpeed = 0.1f;

    private void Start()
    {
        player = FindObjectOfType<PlayerStateMachine>();
        lastTile = FindObjectOfType<LastTile>();
       
        killingAni = killingScreen.GetComponent<Animator>();
        startAni = startingScreen.GetComponent<Animator>();
        warningImage = warningScreen.GetComponent<Image>();
        canvasGroup = textBox.GetComponent<CanvasGroup>();

        //  npcRaycasts = FindObjectsOfType<NPCRaycast>();
        color = warningImage.color;
        timer.SetActive(false);
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

        if (!iswarningscreenActive)
        {
            iswarningscreenActive = true;
            warningScreen.SetActive(true);
            StartCoroutine(WarningScreen_co());
        }

    }

    private IEnumerator WarningScreen_co()
    {
        color.a = 0;
        // ��� ȭ���� ���İ��� 0���� 1�� ������ ����
        while (color.a < 0.7f)
        {
            if (!iswarningscreenActive)
            {
                yield break;  // �ڷ�ƾ ����
            }
            color.a += increaseSpeed * Time.deltaTime; // ���İ� ����
            color.a = Mathf.Clamp01(color.a); // ���İ��� 0�� 1 ���̷� ����
            warningImage.color = color; // ����� ���İ��� �̹����� ����
            yield return null; // ���� ������ ���
        }
        player.isWarningEnd = true;
    }


    public void WarningScreen_Disactive()
    {
        if (iswarningscreenActive)
        {
            iswarningscreenActive = false;
            warningScreen.SetActive(false);
        }
    }

    public void KillingScreen_Active()
    {
        killingScreen.SetActive(true);
    }

    public void KillingScreen_Disactive()
    {
        killingScreen.SetActive(false);
    }

    public void PlayStartingAnimation()
    {
        startingScreen.SetActive(true);
        Debug.Log("PlayStarting ����");

        AnimatorStateInfo stateInfo = startAni.GetCurrentAnimatorStateInfo(0);
        // �ִϸ��̼��� ��� ������ Ȯ��
        if (stateInfo.IsName("Start"))
        {   
            if (stateInfo.normalizedTime >= 1.0f)  // ������Ʈ�� �ȳ����� normalizedTime ���� �ȵ�...
            {
                startingScreen.SetActive(false);
                lastTile.haveActivedStart_ani = true;  // �������� ���� �ִϸ��̼� �ٽ���� ����
                timer.SetActive(true);
            }
        }
    }



    public void ActiveTextBox()
    {
        textBox.SetActive(true);
        canvasGroup.DOFade(0, 0.3f).From();
        canvasGroup.transform.DOLocalMove(new Vector2(-2, 512), 0.3f).SetEase(Ease.Unset);
    }

    public void DisactiveTextBox()
    {
        canvasGroup.DOFade(0, 0.3f);
        canvasGroup.transform.DOLocalMove(new Vector2(10, 200), 0.3f).SetEase(Ease.Unset);
        textBox.SetActive(false);
    }
 







}

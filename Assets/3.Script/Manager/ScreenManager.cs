using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager instance = null;
    private CanvasGroup canvasGroup;

    public List<RangeView> npcList;

    [SerializeField] private GameObject warningScreen;
    [SerializeField] private GameObject killingScreen;
    [SerializeField] private GameObject startingScreen;
    [SerializeField] private GameObject timer;
    [SerializeField] private GameObject textBox;
    [SerializeField] private GameObject gameClear;
    [SerializeField] private GameObject swipe;
    private PlayerStateMachine player;
    private LastTile lastTile;
    public bool iswarningscreenActive = false;
    public bool isTextBoxActive = false;
    public bool havetoReturnFollowingCam = true;

    public Animator killingAni;
    private Animator clearAni;
    private Animator swipeAni;
    [SerializeField] private Animator startAni;
    private Image warningImage;
    private Color color;

    public float increaseSpeed = 0.1f;
    public int npcCount = 0;

    private void Start()
    {
        player = FindObjectOfType<PlayerStateMachine>();
        lastTile = FindObjectOfType<LastTile>();

        killingAni = killingScreen.GetComponent<Animator>();
        startAni = startingScreen.GetComponent<Animator>();
        warningImage = warningScreen.GetComponent<Image>();
        canvasGroup = textBox.GetComponent<CanvasGroup>();
        clearAni = gameClear.GetComponent<Animator>();
        swipeAni = swipe.GetComponent<Animator>();

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
        color = warningImage.color;
        color.a = 0;
        // 경고 화면의 알파값을 0에서 1로 서서히 증가
        while (color.a < 0.7f)
        {
            if (!iswarningscreenActive)
            {
                yield break;  // 코루틴 종료
            }
            color.a += increaseSpeed * Time.deltaTime; // 알파값 증가
            color.a = Mathf.Clamp01(color.a); // 알파값을 0과 1 사이로 제한
            warningImage.color = color; // 변경된 알파값을 이미지에 적용
            yield return null; // 다음 프레임 대기
        }
        player.isWarningEnd = true;
        
    }


    public void WarningScreen_Disactive()
    {
        if (iswarningscreenActive)
        {
            iswarningscreenActive = false;
            warningScreen.SetActive(false);
            Debug.Log(npcCount);
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
       // Debug.Log("PlayStarting 시작");

        AnimatorStateInfo stateInfo = startAni.GetCurrentAnimatorStateInfo(0);
        // 애니메이션이 재생 중인지 확인
        if (stateInfo.IsName("Start"))
        {
            if (stateInfo.normalizedTime >= 1.0f)  // 업데이트에 안넣으면 normalizedTime 갱신 안됨...
            {
                startingScreen.SetActive(false);
                lastTile.haveActivedStart_ani = true;  // 스테이지 시작 애니메이션 다시재생 방지
                timer.SetActive(true);
            }
        }
    }


    public void ActiveTextBox()
    {
        if (!isTextBoxActive)
        {
            textBox.SetActive(true);
            canvasGroup.DOFade(1, 0.3f);
            canvasGroup.transform.DOLocalMove(new Vector2(-2, 512), 0.3f).SetEase(Ease.Unset);
            isTextBoxActive = true;
        }
    }

    public void DisactiveTextBox()
    {
        canvasGroup.DOFade(0, 0.3f);
        canvasGroup.transform.DOLocalMove(new Vector2(-10, 538), 0.3f).SetEase(Ease.Unset);
        //  textBox.SetActive(false);
        isTextBoxActive = false;

        
    }
    public void ActiveCompleteUI()
    {
        gameClear.SetActive(true);
        clearAni.SetTrigger("isCleared");
        AnimatorStateInfo stateinfo = clearAni.GetCurrentAnimatorStateInfo(0);
        if(stateinfo.normalizedTime>=1.0f)
        {
            swipe.SetActive(true);
        }
    }

    public void ActiveTimesUPUI()
    {
        gameClear.SetActive(true);
        clearAni.SetTrigger("isTimesUp");
        AnimatorStateInfo stateinfo = clearAni.GetCurrentAnimatorStateInfo(0);
        if (stateinfo.normalizedTime >= 1.0f)
        {
            swipe.SetActive(true);
        }
    }

}











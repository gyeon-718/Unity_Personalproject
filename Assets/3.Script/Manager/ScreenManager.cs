using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager instance = null;

    [SerializeField] private GameObject warningScreen;
    [SerializeField] private GameObject killingScreen;
    [SerializeField] private GameObject startingScreen;
    private PlayerStateMachine player;
    public bool iswarningscreenActive = false;

    public Animator killingAni;
    private Image warningImage;
    private Color color;

    public float increaseSpeed = 0.1f;

    private void Start()
    {
        player = FindObjectOfType<PlayerStateMachine>();
        killingAni = killingScreen.GetComponent<Animator>();
        warningImage = warningScreen.GetComponent<Image>();
        //  npcRaycasts = FindObjectsOfType<NPCRaycast>();
        color = warningImage.color;
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

        var start_Ani = startingScreen.GetComponent<Animator>();

        AnimatorStateInfo stateInfo = start_Ani.GetCurrentAnimatorStateInfo(0);
       // Debug.Log(stateInfo.IsName("Start"));
        if (stateInfo.normalizedTime >= 0.6f)
        {
            Debug.Log("애니메이션 끝남");
            startingScreen.SetActive(false);
        }

    }







}

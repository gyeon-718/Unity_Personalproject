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
        // 경고 화면의 알파값을 0에서 1로 서서히 증가
        while (color.a < 1.0f)
        {
            color.a += increaseSpeed * Time.deltaTime; // 알파값 증가
            color.a = Mathf.Clamp01(color.a); // 알파값을 0과 1 사이로 제한
            warningImage.color = color; // 변경된 알파값을 이미지에 적용
            yield return null; // 다음 프레임 대기
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
            Debug.Log("킬링애니끝남");
           killingScreen.SetActive(false);
        }
    }

    public void KillingScreen_Disactive()
    {
        killingScreen.SetActive(false);
    }








}

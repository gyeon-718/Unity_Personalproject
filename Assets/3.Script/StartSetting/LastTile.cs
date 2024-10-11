using UnityEngine;

public class LastTile : MonoBehaviour
{
    // 엘레베이터 문을 열기위해 콜라이더 감지

    private Animator door_Ani;
    [SerializeField] private GameObject helperUI;
    private bool isOpenedDoor = false;    // UI 다시 켜짐 방지

    private TextBoxColider textboxColider;

    public bool haveActivedStart_ani = false;


    private void Start()
    {
        door_Ani = GetComponent<Animator>();
        textboxColider = FindObjectOfType<TextBoxColider>();
        helperUI.SetActive(false);
    }

    private void Update()
    {
        if (isOpenedDoor&& !haveActivedStart_ani) ScreenManager.instance.PlayStartingAnimation();  // 청소구역 애니메이션
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")&& !isOpenedDoor)
        {
            helperUI.SetActive(true);      
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            helperUI.SetActive(false);
            door_Ani.SetBool("havetoOpen", true);
            isOpenedDoor = true;
            textboxColider.isStartStage = true;  // 텍스트박스 다시 켜짐 방지
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            helperUI.SetActive(false);
        }
    }
}

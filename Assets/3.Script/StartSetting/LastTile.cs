using UnityEngine;

public class LastTile : MonoBehaviour
{
    // ���������� ���� �������� �ݶ��̴� ����

    private Animator door_Ani;
    [SerializeField] private GameObject helperUI;
    private bool isOpenedDoor = false;    // UI �ٽ� ���� ����

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
        if (isOpenedDoor&& !haveActivedStart_ani) ScreenManager.instance.PlayStartingAnimation();  // û�ұ��� �ִϸ��̼�
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
            textboxColider.isStartStage = true;  // �ؽ�Ʈ�ڽ� �ٽ� ���� ����
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

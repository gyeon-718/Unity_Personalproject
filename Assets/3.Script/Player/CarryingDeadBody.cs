using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryingDeadBody : MonoBehaviour
{
    public Transform carryPosition;

    private PlayerStateMachine playerStateMachine;
    //private GameObject carryDeadBody;

    private bool canCarry = false; // ��ü�� �� �� �ִ� ���� ���� ����
    private GameObject deadBodyInRange; // ���� ���� ��ü ������Ʈ ����

    private Animator deadBody_ani;

    //  private PressEType pType;

    private void Start()
    {
        playerStateMachine = GetComponentInParent<PlayerStateMachine>();
        deadBodyInRange = null;
        //  pType = PressEType.PICKUP;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeadBody"))
        {
            // ��ü�� �� �� �ִ� ���·� ����
            canCarry = true;
            deadBodyInRange = other.gameObject; // ���� �ȿ� �ִ� ��ü ����
            if (deadBodyInRange != null)
            {
                deadBody_ani = deadBodyInRange.GetComponent<Animator>();
               // Debug.Log("�ξƴ�");
            }
         //   Debug.Log("�ű�� ����");
            // ����UI Ȱ��ȭ �޼��� �����
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DeadBody"))
        {
            // ��ü�� �� �� ���� ���·� ����
            canCarry = false;
            deadBodyInRange = null;
          //  Debug.Log("��ü �������� ���");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (canCarry && !playerStateMachine.isCarryingDeadBody)
            {
             //   Debug.Log("ĳ���ٵ�");
                playerStateMachine.isCarryingDeadBody = true;
                CarryBody(deadBodyInRange);
            }
            else if (playerStateMachine.isCarryingDeadBody)
            {
            //    Debug.Log("ǲ�ٿ�ٵ�");
                PutDownBody();
            }
        }

    }

    public void CarryBody(GameObject _deadBody)
    {
        deadBodyInRange = _deadBody;
        Vector3 deadBodyPosition = new Vector3(playerStateMachine.player.forward.x, 0,
           playerStateMachine.player.forward.z);
     //   Debug.Log("�ż��� �ߵ�");
        deadBodyInRange.transform.position = deadBodyPosition;
        deadBodyInRange.transform.position = carryPosition.position;
        deadBodyInRange.transform.SetParent(carryPosition); // �÷��̾��� �ڽ����� ����

        //deadBodyInRange.transform.position = carryPosition.position; // �÷��̾� �տ� ��ġ��Ű��


        //deadBodyInRange.transform.rotation = carryPosition.rotation;
        //  _deadBody.transform.position=new Vector3()
        deadBody_ani.SetBool("isPickedUp", true);
        deadBody_ani.SetBool("isPutDown", false);

    }

    public void PutDownBody()
    {

     //   Debug.Log("ǲ�ٿ�");
        GameObject deadBody = GameObject.Find("DeadBody");
        Animator deadBodyani = deadBody.GetComponent<Animator>();
        // �θ� ���� ����
        deadBodyani.SetBool("isPickedUp", false);
        deadBodyani.SetBool("isPutDown", true);
        deadBody.transform.SetParent(null);

        // ��ü�� �������� ��ġ ���� (���� �÷��̾� �տ� �������� ����)
        Vector3 putDownPosition = playerStateMachine.player.position + playerStateMachine.player.forward; // �÷��̾� �� 2���� ��ġ
        deadBody.transform.position = putDownPosition;

        // �ִϸ��̼� ���� ����


        // �÷��̾� ���� ����
        playerStateMachine.isCarryingDeadBody = false;

       // Debug.Log("��ü ��������");

    }
}
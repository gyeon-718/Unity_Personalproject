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

    private void Start()
    {
        playerStateMachine = GetComponentInParent<PlayerStateMachine>();
        deadBodyInRange = null;
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
                Debug.Log("�ξƴ�");
            }
            Debug.Log("�ű�� ����");
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
            Debug.Log("��ü �������� ���");
        }
    }

    private void Update()
    {
        // ��ü�� �� �� �ִ� ���¿��� E Ű�� ������ ��ü�� ���
        if (canCarry && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E ����");
            playerStateMachine.isCarryingDeadBody = true;
            CarryBody(deadBodyInRange);
        }
    }

    public void CarryBody(GameObject _deadBody)
    {
        deadBodyInRange = _deadBody;
        Vector3 deadBodyPosition = new Vector3(playerStateMachine.player.forward.x-5f , 0,
           playerStateMachine.player.forward.z-3f);
        Debug.Log("�ż��� �ߵ�");
        deadBodyInRange.transform.position = deadBodyPosition;
        deadBodyInRange.transform.position = carryPosition.position;
       // deadBodyInRange.transform.SetParent(carryPosition); // �÷��̾��� �ڽ����� ����

        //deadBodyInRange.transform.position = carryPosition.position; // �÷��̾� �տ� ��ġ��Ű��


        //deadBodyInRange.transform.rotation = carryPosition.rotation;
        //  _deadBody.transform.position=new Vector3()
        deadBody_ani.SetBool("isPickedUp", true);


    }
}
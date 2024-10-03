using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryingDeadBody : MonoBehaviour
{
    public Transform carryPosition;

    private PlayerStateMachine playerStateMachine;
    //private GameObject carryDeadBody;

    private bool canCarry = false; // 시체를 들 수 있는 상태 여부 저장
    private GameObject deadBodyInRange; // 범위 안의 시체 오브젝트 저장

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
            // 시체를 들 수 있는 상태로 변경
            canCarry = true;
            deadBodyInRange = other.gameObject; // 범위 안에 있는 시체 저장
            if (deadBodyInRange != null)
            {
                deadBody_ani = deadBodyInRange.GetComponent<Animator>();
                Debug.Log("널아님");
            }
            Debug.Log("옮기기 가능");
            // 도움말UI 활성화 메서드 만들고
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DeadBody"))
        {
            // 시체를 들 수 없는 상태로 변경
            canCarry = false;
            deadBodyInRange = null;
            Debug.Log("시체 범위에서 벗어남");
        }
    }

    private void Update()
    {
        // 시체를 들 수 있는 상태에서 E 키를 누르면 시체를 들기
        if (canCarry && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E 누름");
            playerStateMachine.isCarryingDeadBody = true;
            CarryBody(deadBodyInRange);
        }
    }

    public void CarryBody(GameObject _deadBody)
    {
        deadBodyInRange = _deadBody;
        Vector3 deadBodyPosition = new Vector3(playerStateMachine.player.forward.x-5f , 0,
           playerStateMachine.player.forward.z-3f);
        Debug.Log("매서드 발동");
        deadBodyInRange.transform.position = deadBodyPosition;
        deadBodyInRange.transform.position = carryPosition.position;
       // deadBodyInRange.transform.SetParent(carryPosition); // 플레이어의 자식으로 설정

        //deadBodyInRange.transform.position = carryPosition.position; // 플레이어 앞에 위치시키기


        //deadBodyInRange.transform.rotation = carryPosition.rotation;
        //  _deadBody.transform.position=new Vector3()
        deadBody_ani.SetBool("isPickedUp", true);


    }
}
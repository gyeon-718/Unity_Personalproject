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
            // 시체를 들 수 있는 상태로 변경
            canCarry = true;
            deadBodyInRange = other.gameObject; // 범위 안에 있는 시체 저장
            if (deadBodyInRange != null)
            {
                deadBody_ani = deadBodyInRange.GetComponent<Animator>();
               // Debug.Log("널아님");
            }
         //   Debug.Log("옮기기 가능");
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
          //  Debug.Log("시체 범위에서 벗어남");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (canCarry && !playerStateMachine.isCarryingDeadBody)
            {
             //   Debug.Log("캐리바디");
                playerStateMachine.isCarryingDeadBody = true;
                CarryBody(deadBodyInRange);
            }
            else if (playerStateMachine.isCarryingDeadBody)
            {
            //    Debug.Log("풋다운바디");
                PutDownBody();
            }
        }

    }

    public void CarryBody(GameObject _deadBody)
    {
        deadBodyInRange = _deadBody;
        Vector3 deadBodyPosition = new Vector3(playerStateMachine.player.forward.x, 0,
           playerStateMachine.player.forward.z);
     //   Debug.Log("매서드 발동");
        deadBodyInRange.transform.position = deadBodyPosition;
        deadBodyInRange.transform.position = carryPosition.position;
        deadBodyInRange.transform.SetParent(carryPosition); // 플레이어의 자식으로 설정

        //deadBodyInRange.transform.position = carryPosition.position; // 플레이어 앞에 위치시키기


        //deadBodyInRange.transform.rotation = carryPosition.rotation;
        //  _deadBody.transform.position=new Vector3()
        deadBody_ani.SetBool("isPickedUp", true);
        deadBody_ani.SetBool("isPutDown", false);

    }

    public void PutDownBody()
    {

     //   Debug.Log("풋다운");
        GameObject deadBody = GameObject.Find("DeadBody");
        Animator deadBodyani = deadBody.GetComponent<Animator>();
        // 부모 관계 해제
        deadBodyani.SetBool("isPickedUp", false);
        deadBodyani.SetBool("isPutDown", true);
        deadBody.transform.SetParent(null);

        // 시체를 내려놓을 위치 설정 (현재 플레이어 앞에 내려놓는 예시)
        Vector3 putDownPosition = playerStateMachine.player.position + playerStateMachine.player.forward; // 플레이어 앞 2미터 위치
        deadBody.transform.position = putDownPosition;

        // 애니메이션 상태 변경


        // 플레이어 상태 변경
        playerStateMachine.isCarryingDeadBody = false;

       // Debug.Log("시체 내려놓음");

    }
}
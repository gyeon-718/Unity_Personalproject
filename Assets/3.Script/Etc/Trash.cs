using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    [SerializeField] private Transform vacuumTransform; // 청소기의 Transform
    private PlayerStateMachine player;
    public float moveSpeed = 3f;

    private bool isBeingVacuumed = false;

    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("CleanRange"))
        {
            isBeingVacuumed = true; // 조건문 들어가
        }
        if (other.CompareTag("Destroy"))
        {
            Destroy(gameObject); // 임시
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CleanRange"))
        {
            isBeingVacuumed = false; // 조건문 나가
        }
    }
    private void Start()
    {
        player = FindObjectOfType<PlayerStateMachine>();
    }
    private void Update()
    {
        if (isBeingVacuumed && player.isCleanRangeActive)
        {
            GotoVacuum();
        }
    }

    private void GotoVacuum()
    {
        moveSpeed += 50f * Time.deltaTime;

        // 오브젝트를 청소기 쪽으로 이동시킴
        transform.position = Vector3.MoveTowards(transform.position, vacuumTransform.position, moveSpeed * Time.deltaTime);
      //  Debug.Log(moveSpeed);
    }
}


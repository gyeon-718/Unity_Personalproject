using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    [SerializeField] private Transform vacuumTransform; // û�ұ��� Transform
    private PlayerStateMachine player;
    public float moveSpeed = 3f;

    private bool isBeingVacuumed = false;

    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("CleanRange"))
        {
            isBeingVacuumed = true; // ���ǹ� ��
        }
        if (other.CompareTag("Destroy"))
        {
            Destroy(gameObject); // �ӽ�
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CleanRange"))
        {
            isBeingVacuumed = false; // ���ǹ� ����
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

        // ������Ʈ�� û�ұ� ������ �̵���Ŵ
        transform.position = Vector3.MoveTowards(transform.position, vacuumTransform.position, moveSpeed * Time.deltaTime);
      //  Debug.Log(moveSpeed);
    }
}


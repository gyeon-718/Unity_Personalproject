using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doodle : MonoBehaviour
{
    //�ν����Ϳ��� �����ϻ�......
    [SerializeField] private float decreaseScale;  // �پ��� ������
    [SerializeField] private float destroyScale;  // �� ũ�⺸�� �۾����� ������Ʈ�� ����


    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Water"))
        {
            transform.localScale -= new Vector3(decreaseScale, 0, decreaseScale) * Time.deltaTime;

            // �������� ���� �� ���Ϸ� �۾����� ������Ʈ�� ����
            if (transform.localScale.x < destroyScale && transform.localScale.z < destroyScale)
            {
                Destroy(gameObject);  // ������Ʈ ����
            }
        }
    }

}

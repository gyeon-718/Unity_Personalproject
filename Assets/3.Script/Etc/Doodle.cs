using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doodle : MonoBehaviour
{
    //인스펙터에서 조절하삼......
    [SerializeField] private float decreaseScale;  // 줄어드는 스케일
    [SerializeField] private float destroyScale;  // 이 크기보다 작아지면 오브젝트를 삭제


    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Water"))
        {
            transform.localScale -= new Vector3(decreaseScale, 0, decreaseScale) * Time.deltaTime;

            // 스케일이 일정 값 이하로 작아지면 오브젝트를 삭제
            if (transform.localScale.x < destroyScale && transform.localScale.z < destroyScale)
            {
                Destroy(gameObject);  // 오브젝트 삭제
            }
        }
    }

}

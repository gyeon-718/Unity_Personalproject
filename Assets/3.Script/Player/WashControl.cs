using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashControl : MonoBehaviour
{
    public ParticleSystem washParticle;
    public ParticleSystem wallHit;
    public ParticleSystem groundHit;
    private bool isWall = false;

    private void Start()
    {
        //washParticle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        // 벽파티클이 재생중인데! 벽이 아니면
        if (wallHit.isPlaying && !isWall)
        {
            wallHit.Stop();
        }

        // 벽에 닿지 않는 상태로 다시 설정 (다음 충돌 이벤트까지)
        isWall = false;
       
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Wall"))
        {
            // 파티클이 충돌할 때 발생하는 여러 정보를 담고 있는 클라스를 담을 리스트(충돌지점, 충돌 속도 등)
            List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
            // washparticle과 other의 충돌 정보를 collision 리스트에 담는 매서드
            ParticlePhysicsExtensions.GetCollisionEvents(washParticle, other, collisionEvents);

            // 충돌한 지점이 존재하느냐?
            if (collisionEvents.Count > 0)
            {
                // 첫 번째 충돌 지점(교차점)
                Vector3 collisionPoint = collisionEvents[0].intersection;


                // 해당 지점으로 wallHit 파티클 시스템 이동 후 재생
                wallHit.transform.position = collisionPoint;
                wallHit.Play();
                isWall = true;
            }
        }


    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWash : BaseState
{
    private Transform player;
    private PlayerStateMachine playerStateMachine;

    private float rotateSpeed = 5f;
    private float moveSpeed = 2.5f;
    private string currentAnimation;

    public PlayerWash(StateMachine stateMachine, Transform _player) : base("PlayerWash", stateMachine)
    {

        this.player = _player;
        this.playerStateMachine = (PlayerStateMachine)stateMachine;
    }

    public override void Enter()
    {
        playerStateMachine.player_ani.SetBool("isWashing", true);
        playerStateMachine.waterParticle.Play();
        playerStateMachine.Water_groundHit.Play();
      //  Debug.Log("물청소");
    }

    public override void Update()
    {
        Move();

        if (playerStateMachine.isWarningEnd)
        {
            playerStateMachine.ChangeState(new PlayerAttack_Start(stateMachine, player));
        }
        else
        {
            // 입력이 있으면 Walk 상태로 전환
            if (!Input.GetMouseButton(0))
            {
                if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
                {
                    playerStateMachine.ChangeState(new PlayerIdle(stateMachine, player));
                }
                else
                {
                    playerStateMachine.ChangeState(new PlayerWalk(stateMachine, player));
                }

            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                playerStateMachine.player_ani.SetTrigger("isSwitching");
                playerStateMachine.playerType = PlayerType.Vacuum;
                playerStateMachine.vacuumNozzle.SetActive(true);
                playerStateMachine.washerNozzle.SetActive(false);
                playerStateMachine.ChangeState(new PlayerVacuum(stateMachine, player));
            }
        }
    }
    public override void Exit()
    {
        playerStateMachine.player_ani.SetBool("isWashing", false);
        playerStateMachine.waterParticle.Stop();
        playerStateMachine.Water_groundHit.Stop();
        playerStateMachine.Water_wallHit.Stop();
    }


    private void Move()
    {
        RotateMouse();
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(horizontal, 0, vertical);
        moveDir = moveDir.normalized;
        moveDir = Quaternion.Euler(0, 45, 0) * moveDir;
        if (moveDir != Vector3.zero)
        {
            if (currentAnimation != "Walk")
            {
                playerStateMachine.PlayAnimation("Walk");
                currentAnimation = "Walk";
            }
            player.forward = Vector3.Slerp(player.forward, moveDir, Time.deltaTime * rotateSpeed);
            player.position += moveDir * moveSpeed * Time.deltaTime;
        }
        else
        {
            if (currentAnimation != "Idle")
            {
                playerStateMachine.PlayAnimation("Idle");
                currentAnimation = "Idle";
            }
        }
    }

    private void RotateMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.up);

        if (plane.Raycast(ray, out float distance))
        {
            Vector3 direction = ray.GetPoint(distance) - player.position;
            player.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        }
    }
}





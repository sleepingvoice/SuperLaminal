using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasing : MonoBehaviour
{
    [SerializeField] private float rad = 10f;
    public bool MoveCheck = false;
    public Vector3 PosCheck = Vector3.zero;

    public float Speed = 1f;
    public float DisLimit = 1.5f;
    public Transform TriggerCheck = null;

    private float AttackCool = 0f;
    private GameObject player = null;
    private Vector3 playerDir_normal = Vector3.zero;
    private Rigidbody myRigigd = null;
    private Vector3 myPos = Vector3.zero;
    private float DisPlayer = 0f;
    private float RandTime = 0f;
    private float MoveTime = 0f;
    private Vector3 RanVec = Vector3.zero;
    private Vector3 nowPos = Vector3.zero;

    enum MonsterState { STATE_IDLE, STATE_WALK, STATE_ATTACK, STATE_RUN }
    private MonsterState nowState = MonsterState.STATE_IDLE;

    private void Start()
    {
        myRigigd = GetComponent<Rigidbody>();
        myPos = transform.position + Vector3.up;
        RoundTest();
    }

    private void RoundTest()
    {
        TriggerCheck.localScale *= rad * 2;
        TriggerCheck.transform.position = myPos;
    }

    public void ChasingPlayer()
    {
        AttackCool += Time.deltaTime;
        RandTime += Time.deltaTime;
        if (player != null)
        {
            MoveCheck = false;
            MoveTime = 0f;

            DisPlayer = Vector3.Distance(transform.position, player.transform.position); // 플레이어와 몬스터의 거리

            myPos = transform.position + Vector3.up; //몬스터의 중심점이 바닥에 있으므로 중심을 보정해준다

            playerDir_normal = player.transform.position - myPos;
            playerDir_normal = playerDir_normal.normalized;//플레이어와 몬스터의 방향벡터
            playerDir_normal.y = 0f;

            transform.rotation = Quaternion.LookRotation(playerDir_normal, Vector3.up);//몬스터가 플레이어가 있는 방향을 바라보게 함

            if (DisPlayer <= DisLimit)//공격거리제한보다 플레이어가 가까이있다면
            {
                myRigigd.velocity = Vector3.zero;
                if (nowState != MonsterState.STATE_ATTACK && AttackCool >= 2f) //쿨타임이 지났으면
                {
                    nowState = MonsterState.STATE_ATTACK;
                    AttackCool = 0f;
                }
                else//쿨타임이 지나지 않았으면
                {
                    nowState = MonsterState.STATE_IDLE;
                }
            }
            else//공격거리제한보다 플레이어가 멀리있다면
            {
                myRigigd.velocity = playerDir_normal * Speed;
                if (nowState != MonsterState.STATE_RUN)
                    nowState = MonsterState.STATE_RUN;
            }
        }
        else if(player == null)//플레이어가 사거리 밖에 있어서 플레이어가 null이라면
        {
            RandomMove();
        }
        
    }



    public void player_Check(GameObject play)
    {
        player = play;
    }

    public int Anicheck()
    {
        return (int)nowState;
    }

    public GameObject Player_Return()
    {
        return player;
    }

    private void RandomMove()
    {
        if (RandTime >= 3.0f)
        {
            if (!MoveCheck)
            {
                RandomVector();
                MoveCheck = true;
            }
            else
            {
                MoveTime += Time.deltaTime / 3;
                firstPos();
                transform.position = Vector3.Lerp(nowPos, PosCheck, MoveTime);
                transform.rotation = Quaternion.LookRotation(RanVec, Vector3.up);
                if (Vector3.Distance(transform.position, PosCheck) <= 0.1f)
                {
                    MoveCheck = false;
                    RandTime = 0;
                    MoveTime = 0;
                }
            }
            if (nowState!=MonsterState.STATE_WALK)
                nowState = MonsterState.STATE_WALK;
        }
        else
        {
            if (nowState != MonsterState.STATE_IDLE)
                nowState = MonsterState.STATE_IDLE;
            nowPos = Vector3.zero;

        }
    }

    private void RandomVector()
    {
        float RanX = Random.Range(-5.0f, 5.0f);
        float RanZ = Random.Range(-5.0f, 5.0f);
        RanVec = new Vector3(RanX, 0, RanZ);
        PosCheck = transform.position + RanVec;
    }

    private void firstPos()
    {
        if(nowPos == Vector3.zero)
        {
            nowPos = transform.position;
        }
    }
}

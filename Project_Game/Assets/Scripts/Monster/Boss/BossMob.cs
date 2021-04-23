using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMob : MonoBehaviour
{
    [SerializeField]private GameObject[] Pattern = new GameObject[2];
    public GameObject Player = null;
    public bool CutFinish = false;
    public float Now_Hp = 20f;

    private bool Pattern_Finish = true;
    private bool Pattern_Check = false;
    private Pattern_ColliderCheck Pattern_Col = null;
    private GameObject nowPattern = null;
    private float Damage = 20f;
    private int randInt = 0;
    private Animator myAni = null;

    enum BossState { STATE_IDLE, STATE_ATTACK_1, STATE_ATTACK_2, STATE_ATTACK_3, STATE_ATTACK_4, STATE_DIE,STATE_WAIT }
    [SerializeField]private BossState nowState = BossState.STATE_IDLE;

    private void Start()
    {
        myAni = GetComponent<Animator>();
    }

    public void BossUpdate()
    {
        if (Pattern_Finish)
        {
            RandPattern();
            StartCoroutine(Boss_Pattern(nowPattern));
        }

        myAni.SetInteger("Boss_State", (int)nowState);
        if(nowState == BossState.STATE_IDLE && CutFinish)
            PlayerChase();
        BossDie();
    }

    private void RandPattern()
    {
        if (!Pattern_Check)
        {
            randInt = Random.Range(0,2);
            nowPattern = Pattern[randInt];
            Pattern_Check = true;
        }
    }

    IEnumerator Boss_Pattern(GameObject Prefeb)
    {
        Pattern_Finish = false;

        yield return new WaitForSeconds(3);//패턴의 쿨타임

        Quaternion myRotate = transform.rotation;
        myRotate.x = 0;
        myRotate.z = 0;
        Vector3 myPos = transform.position;
        myPos.y = 0.001f;
        GameObject ins = Instantiate(Prefeb, myPos, myRotate);
        nowState = BossState.STATE_WAIT;

        yield return new WaitForSeconds(3);//장판피할시간

        nowState = (BossState)(randInt + 1);//랜덤하게 선택된 패턴의 상태를 현재상태로 넣어줌
        Pattern_Col = ins.GetComponent<Pattern_ColliderCheck>();
        Pattern_Col.StartEffect();
        if (Pattern_Col.ReturnAttackCheck())
        {
            Player.GetComponent<Player_con>().Hp -= Damage;
        }
        Destroy(ins,2.0f);

        yield return new WaitForSeconds(2);//애니메이션 끝날시간

        nowState = BossState.STATE_IDLE;
        Pattern_Finish = true;
        Pattern_Check = false;
    }

    private void PlayerChase()
    {
        float DisPlayer = Vector3.Distance(transform.position, Player.transform.position);
        Vector3 myPos = transform.position + Vector3.up;
        Vector3 playerDir_normal = Player.transform.position - myPos;
        playerDir_normal = playerDir_normal.normalized;
        playerDir_normal.y = 0;
        transform.rotation = Quaternion.LookRotation(playerDir_normal, Vector3.up);
    }

    private void BossDie()
    {
        if(Now_Hp <= 0 || Input.GetKeyDown(KeyCode.O))
        {
            Stage_Manager.instance.ReturnChanger().Credit();
        }
    }
}

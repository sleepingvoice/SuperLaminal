using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Con : MonoBehaviour
{
    private Animator myAni = null;
    private Chasing myCahse = null;
    private Player_con player = null;
    private bool ChasingStart = false;
    private GameObject myHpBar = null;
    private Monster_HpBar _hpbar = null;

    public bool StopAction = false;
    public float Now_Hp = 5f;
    public GameObject hpBarPrefab;

    void Start()
    {
        myAni = GetComponent<Animator>();
        myCahse = GetComponent<Chasing>();
        StartCoroutine("FirstStart");
        SetHpBar();
    }
    
    public void MonsterUpdate()
    {
        if(StopAction)
            myAni.SetInteger("AniState",0);

        if (ChasingStart == true && !StopAction)
        {
            myCahse.ChasingPlayer();
                myAni.SetInteger("AniState", myCahse.Anicheck());
                player_Check();
        }
        if(_hpbar != null)
        {
            _hpbar.HPbarPos();
        }
    }

    private void player_Check()
    {
        if (player == null && myCahse.Player_Return() != null)
            player = myCahse.Player_Return().GetComponent<Player_con>();
        else if (player != null && myCahse.Player_Return() == null)
            player = null;
    }

    private void MonsterDie()
    {
        if(Now_Hp <= 0f)
        {
            Destroy(this.gameObject);
        }
    }
    IEnumerator FirstStart()
    {
        yield return new WaitForSeconds(5);
        ChasingStart = true;
    }

    public void StopCor()
    {
        StopAllCoroutines();
    }

    private void SetHpBar()//HpBar를 생성하는 함수
    {
        Canvas HpBarCanvas = GameObject.Find("HealthBarCanvas").GetComponent<Canvas>();
        myHpBar = Instantiate<GameObject>(hpBarPrefab, HpBarCanvas.transform);

        _hpbar = myHpBar.GetComponent<Monster_HpBar>();
        _hpbar.SetmyMob(this.transform);
    }

    public void DestroyHpBar()
    {
        Destroy(myHpBar);
    }

    public void ActiveHpBar(bool b)
    {
        myHpBar.SetActive(b);
    }

}

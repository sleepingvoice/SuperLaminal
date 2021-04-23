using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonster : MonoBehaviour
{
    public bool Attacked = false;
    public GameObject ExplosionPrefeb = null;
    public BossCutScene myBoss = null;

    private int stage = 1;
    private Vector3 First_ExplosionPos = new Vector3(0.75f,2f,-1.5f);
    private Vector3 Second_ExplosionPos = new Vector3(-1f, 4f, -1.5f);
    private Vector3 Third_ExplosionPos = new Vector3(0f,5f,-2f);
    private Spowner mySpowner = null;

    private void Awake()
    {
        mySpowner = GetComponentInChildren<Spowner>();
    }

    public void  SpawnMonsterUpdate()
    {
        mySpowner.SpownerUpdate();
    }

    public void DamageSpawnMob()
    {
        if (Attacked || Input.GetKeyDown(KeyCode.P))
        {
            mySpowner.AllMobDie();
            StartCoroutine(DamageMob());
            Attacked = false;
        }
    }

    IEnumerator DamageMob()
    {
        GameObject Explosion =Instantiate(ExplosionPrefeb, transform.position + ExplosionPosition(), Quaternion.identity);
        yield return new WaitForSeconds(2);
        Destroy(Explosion);
        if(stage == 3)
        {
            myBoss.SpawnBoss();
            StopAllCoroutines();
            Destroy(gameObject);
        }
        stage++;
        mySpowner.SpawnNum += 5;
        mySpowner.stage = stage;
    }

    private Vector3 ExplosionPosition()
    {
        Vector3 ExplosionPos = Vector3.zero;
        if (stage == 1)
            ExplosionPos = First_ExplosionPos;
        else if (stage == 2)
            ExplosionPos = Second_ExplosionPos;
        else if(stage == 3)
            ExplosionPos = Third_ExplosionPos;

        return ExplosionPos;
    }

    public void HpBarActive(bool b)
    {
        mySpowner.MonsterHpBarSet(b);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spowner : MonoBehaviour
{
    public GameObject prefabs;
    public int SpawnNum = 10;
    public int stage = 1;

    public List<GameObject> Mob = new List<GameObject>();
    private BoxCollider mybox = null;

    void Start()
    {
        mybox = GetComponent<BoxCollider>();
    }

    public void SpownerUpdate()
    {
        if (stage != 0)
        {
            Spawn(SpawnNum);
            stage = 0;
        }
        MonsterUpdate();
        MobDie();
    }

    private void Spawn(int Count)
    {
        for (int i = 0; i < Count; ++i)
        {
            Vector3 spawnPos = GetRandomPosition();

            GameObject instance = Instantiate(prefabs, spawnPos, Quaternion.identity);
            Mob.Add(instance);
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 SpawnPos = transform.position;

        float posX = SpawnPos.x + Random.Range(-mybox.size.x / 2f, mybox.size.x / 2f);
        float posY = transform.position.y;
        float posZ = SpawnPos.z + Random.Range(-mybox.size.z / 2f, mybox.size.z / 2f);

        Vector3 spawnPos = new Vector3(posX, posY, posZ);

        return spawnPos;
    }

    private void MobDie()
    {
        if (Mob == null)
            return;
        foreach (GameObject obj in Mob)
        {
            if (obj != null)
            {
                if (obj.GetComponent<Monster_Con>().Now_Hp <= 0)
                {
                    obj.GetComponent<Monster_Con>().StopCor();
                    obj.GetComponent<Monster_Con>().DestroyHpBar();
                    Destroy(obj);
                }
            }
        }
    }

    public void AllMobDie()
    {
        if (Mob != null)
        {
            foreach (GameObject obj in Mob)
            {
                if (obj != null)
                {
                    obj.GetComponent<Monster_Con>().StopCor();
                    obj.GetComponent<Monster_Con>().DestroyHpBar();
                    Destroy(obj);
                }
            }
            Mob.Clear();
        }
    }

    public void MonsterUpdate()
    {
        foreach (GameObject obj in Mob)
        {
            if (obj == null)
                return;
            obj.GetComponent<Monster_Con>().MonsterUpdate();
        }
    }

    public void MonsterHpBarSet(bool b)
    {
        foreach (GameObject obj in Mob)
        {
            if (obj == null)
                return;
            obj.GetComponent<Monster_Con>().ActiveHpBar(b);
        }
    }
}

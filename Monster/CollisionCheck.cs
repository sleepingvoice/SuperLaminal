using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    public bool AttackArm = false;
    public float Limit = 0f;
    private float attackTime = 0f;
    private Monster_Con Mob = null;
   

    private void Start()
    {
        if (AttackArm)
        {
            Mob = GetComponentInParent<Monster_Con>();
        }
    }

    private void Update()
    {
        attackTime += Time.deltaTime;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 11 && !AttackArm)
        {
            this.transform.parent.GetComponent<Chasing>().player_Check(other.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11 && AttackArm && attackTime >= Limit)
        {
            other.gameObject.GetComponent<Player_con>().Hp -= 10;
            Debug.Log("성공");
            attackTime = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 11 && !AttackArm)
        {
            this.transform.parent.GetComponent<Chasing>().player_Check(null);
        }
    }
}

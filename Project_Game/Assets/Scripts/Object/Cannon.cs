using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Player_con Player = null;
    [SerializeField] private SpawnMonster Spawner = null;

    public bool BulletInBox = false;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Bullet")
        {
            //if(collision.collider.gameObject.transform.localScale.x >= 2.5f)
            //{
                BulletInBox = true;
                Destroy(collision.gameObject);
            //}            
        }
    }
}

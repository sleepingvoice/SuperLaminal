using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet info")]
    public bool isShoot = false;
    public int Bullet_Level = 1;
    public int BulletDmg = 1;
    
    private Vector3 Direction = Vector3.zero;
    private float BulletSpeed = 20f;    

    // Scale에 따라 Bullet의 레벨을 정함
    public void CountBulletLevel()
    {
        if(this.transform.localScale.x <= 3.0f && this.transform.localScale.x >= 2.0f)
        {
            Bullet_Level = 3;
        }
        else if (this.transform.localScale.x < 2.0f && this.transform.localScale.x >= 1.0f)
        {
            Bullet_Level = 2;
        }
        else if (this.transform.localScale.x < 1.0f && this.transform.localScale.x >= 0.5f)
        {
            Bullet_Level = 1;
        }
    }    

    public void BulletMove()
    {
        if (isShoot)
        {            
            Vector3 newPos = transform.position;
            newPos += Direction * BulletSpeed * Time.deltaTime;
            transform.position = newPos;

            Destroy(this.gameObject, 3f);
        }
    }

    // Bullet의 나가는 방향과 정보들을 설정해줌
    public void SetBullet(Vector3 _Dir)
    {        
        switch (Bullet_Level)
        {
            case 1:
                this.transform.localScale = Vector3.one * 0.5f;
                BulletDmg = 1;
                break;
            case 2:
                this.transform.localScale = Vector3.one * 1.5f;
                BulletDmg = 3;
                break;
            case 3:
                this.transform.localScale = Vector3.one * Bullet_Level;
                BulletDmg = 5;
                break;
        }        
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Collider>().isTrigger = true;
        Direction = _Dir;

        isShoot = true;
    }   


}

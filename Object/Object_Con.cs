using UnityEngine;

public class Object_Con : MonoBehaviour
{
    public bool isContact = false;

    public float Obj_Min_Scale;
    public float Obj_Max_Scale;
    public bool SpawnerContact = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
            isContact = true;
        if(other.gameObject.layer == 14)
        {
            SpawnerContact = true;
        }
        if(other.gameObject.tag == "Monster")
        {
            if (this.GetComponent<Bullet>().isShoot)
            {
                other.GetComponent<Monster_Con>().Now_Hp -= transform.GetComponent<Bullet>().BulletDmg;
                Destroy(this.gameObject);
            }            
        }
        if(other.gameObject.layer == 15)
        {
            if(this.GetComponent<Bullet>().isShoot)
            {
                other.GetComponent<BossMob>().Now_Hp -= transform.GetComponent<Bullet>().BulletDmg;
                Destroy(this.gameObject);
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
            isContact = false;
    }


}

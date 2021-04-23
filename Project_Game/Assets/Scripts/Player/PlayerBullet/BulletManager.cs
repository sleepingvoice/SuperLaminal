using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletManager : MonoBehaviour
{
    [Header("Script Setting")]
    [SerializeField] private Player_con Player = null;
    [SerializeField] private GameObject BulletPrefab = null;

    [Header("Text Setting")]
    [SerializeField] private Text Lv1_text = null;
    [SerializeField] private Text Lv2_text = null;
    [SerializeField] private Text Lv3_text = null;

    public bool isObtain = false;

    private Bullet bullet = null;
    private List<int> Bullet_list = new List<int>();
    private List<GameObject> ShotBullet_list = new List<GameObject>();
    private int Max_SpawnBullet = 40;
    private int Lv1_bullet = 0;
    private int Lv2_bullet = 0;
    private int Lv3_bullet = 0;

    private void Start()
    {
        int random = Random.Range(10, Max_SpawnBullet);
        for (int i = 0; i < random; ++i)
        {
            GameObject bullet = Instantiate(BulletPrefab, new Vector3(Random.Range(-50, 50), 1, Random.Range(-50, 50)), Quaternion.identity);
            bullet.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    // bullet을 가져올 때 level을 세팅해주고 저장해줌
    public void GetBullet()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Player.GetComponent<ClickEvent>().obj != null)
            {
                if (Player.GetComponent<ClickEvent>().obj.tag == "Bullet")
                {
                    bullet = Player.GetComponent<ClickEvent>().obj.GetComponent<Bullet>();
                }
                else
                {
                    return;
                }
            }
            isObtain = true;
        }

        if (isObtain)
        {
            float Obj_Dis = Vector3.Distance(bullet.gameObject.transform.position, Player.gameObject.transform.position);
            if (Obj_Dis <= 1)
            {
                bullet.CountBulletLevel();
                isObtain = false;
                Bullet_list.Add(bullet.Bullet_Level);
                Bullet_list.Sort();
                CountBullet();
                Player.GetComponent<ClickEvent>().obj_Catch = false;
                Destroy(bullet.gameObject);
            }
            else
            {
                bullet.gameObject.transform.position = Vector3.MoveTowards(bullet.gameObject.transform.position, Player.gameObject.transform.position, 1f);
            }
        }
    }

    // ClickEvent에서 돌릴 것
    public void ShootBullet()
    {
        if (Bullet_list.Count != 0)
        {
            if (Input.GetMouseButtonDown(1))
            {
                GameObject bullet = Instantiate(BulletPrefab, Player.transform.position, Quaternion.identity);
                bullet.GetComponent<Bullet>().Bullet_Level = Bullet_list[0];
                bullet.GetComponent<Bullet>().SetBullet(Player.myCam.transform.forward);
                Bullet_list.RemoveAt(0);
                CountBullet();
                ShotBullet_list.Add(bullet);
            }
        }
    }

    // update에서 돌려야 함
    private void BulletLevel_UI()
    {
        Lv1_text.text = Lv1_bullet.ToString();
        Lv2_text.text = Lv2_bullet.ToString();
        Lv3_text.text = Lv3_bullet.ToString();
    }

    // Bullet의 레벨을 정리
    private void CountBullet()
    {
        Lv1_bullet = 0;
        Lv2_bullet = 0;
        Lv3_bullet = 0;
        if (Bullet_list.Count != 0)
        {
            foreach (int i in Bullet_list)
            {
                if (i == 1)
                {
                    ++Lv1_bullet;
                }
                if (i == 2)
                {
                    ++Lv2_bullet;
                }
                if (i == 3)
                {
                    ++Lv3_bullet;
                }
            }
        }
    }

    public void BulletUpdate()
    {
        BulletLevel_UI();

        if (ShotBullet_list == null)
            return;
        foreach (GameObject obj in ShotBullet_list)
        {
            if (obj != null)
            {
                obj.GetComponent<Bullet>().BulletMove();                
            }
        }
    }
}

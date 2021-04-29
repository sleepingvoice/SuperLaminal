using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonCutScene : MonoBehaviour
{
    [SerializeField] private GameObject Bullet = null;
    [SerializeField] private GameObject GunBody = null;
    [SerializeField] private GameObject Spawner = null;
    [SerializeField] private GameObject MainCam = null;
    [SerializeField] private GameObject SubCam = null;
    [SerializeField] private Cannon myCannon = null;

    private GameObject CutBullet = null;
    private float MoveTime = 0f;
    private Vector3 StarterPos = Vector3.zero;
    private bool BulletAttack = false;

    public bool StartCut = false;
    public float Speed;

    public void CannonUpdate()
    {
        if(myCannon.BulletInBox)
        {
            CutSceneMove();
        }
    }

    private void CutSceneMove()//업데이트에 돌려야함
    {
        if (CutBullet == null && !BulletAttack)//탄환이 없고 데미지를 입히지 못하였을경우
        {
            CutBullet = Instantiate(Bullet, GunBody.transform.position, Quaternion.identity);
            CutBullet.transform.localScale = Vector3.one * 3f;
            CutBullet.GetComponent<SphereCollider>().isTrigger = true;
            StarterPos = CutBullet.transform.position;
            CutSceneStart();
        }
        else if(CutBullet == null && BulletAttack)//탄환이 없지만 데미지를 입혔을 경우
            return;

        MoveTime += Time.deltaTime / 3;//3초가 됐을때 MoveTime이 1이 된다.
        Stage_Manager.instance.mySapwnMob().HpBarActive(false);
        Stage_Manager.instance.AimActive(false);

        if (MoveTime < 1)//게임시간상 3초가 지나지 않았을 경우
        {
            CutBullet.transform.position = Vector3.Lerp(StarterPos, Spawner.transform.position, MoveTime);
            SubCam.transform.position = Vector3.Lerp(new Vector3(70, 5, 0), new Vector3(12, 25, 0), MoveTime);
        }

        if (CutBullet.GetComponent<Object_Con>().SpawnerContact)//탄환이 spawner와 충돌하였을 경우
        {
            StartCoroutine(EnterSpawner());
            BulletAttack = true;
        }
    }
    
    IEnumerator EnterSpawner()
    {
        Destroy(CutBullet);
        CutBullet = null;
        Spawner.GetComponent<SpawnMonster>().Attacked = true;
        yield return new WaitForSeconds(1f);
        MainCam.SetActive(true);
        SubCam.SetActive(false);
        StartCut = false;
        myCannon.BulletInBox = false;
        BulletAttack = false;
        MoveTime = 0f;
        Stage_Manager.instance.mySapwnMob().HpBarActive(true);
        Stage_Manager.instance.AimActive(true);
    }


    private void CutSceneStart()
    {
        MainCam.SetActive(false);
        SubCam.SetActive(true);
        StartCut = true;
    }
}

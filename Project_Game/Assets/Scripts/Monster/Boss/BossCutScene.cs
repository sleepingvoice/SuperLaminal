using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCutScene : MonoBehaviour
{
    private Vector3 firstPos = Vector3.zero;
    private Vector3 nowPos = Vector3.zero;
    private float CutTime = 0f;
    private int MoveTime = 0;
    private GameObject BossMob = null;
    [SerializeField] private GameObject MainCam = null;
    [SerializeField] private GameObject SubCam = null;

    public GameObject BossPrefeb = null;
    public bool StartCut = true;
    private void firstMove()
    {
        CutTime += Time.deltaTime / 3f;
        firstPos = new Vector3(60f, 60f, 60f);
        nowPos = new Vector3(60f, 60f, -30f);
        BossMob.transform.position = Vector3.Lerp(firstPos, nowPos, CutTime);

    }

    private void SecondMove()
    {
        CutTime += Time.deltaTime / 3f;
        firstPos = new Vector3(60f, 60f, -30f);
        nowPos = new Vector3(-30f, 60f, -30f);
        BossMob.transform.position = Vector3.Lerp(firstPos, nowPos, CutTime);
    }

    private void ThirdMove()
    {
        CutTime += Time.deltaTime / 3f;
        firstPos = new Vector3(-30f, 60f, -30f);
        nowPos = new Vector3(-30f, 60f, 45f);
        BossMob.transform.position = Vector3.Lerp(firstPos, nowPos, CutTime);

    }

    private void FourthMove()
    {
        CutTime += Time.deltaTime / 3f;
        firstPos = new Vector3(-30f, 60f, 45f);
        nowPos = new Vector3(0f, 60f, 45f);
        BossMob.transform.position = Vector3.Lerp(firstPos, nowPos, CutTime);
    }

    private void LastMove()
    {
        CutTime += Time.deltaTime / 2f;
        firstPos = new Vector3(0f, 60f, 45f);
        nowPos = new Vector3(0f, 0f, 0f);
        BossMob.transform.position = Vector3.Lerp(firstPos, nowPos, CutTime);
    }

    public void CutSceneMove()
    {
        if (!BossMob)
            return;
        if (MoveTime >= 5)
        {
            BossMob.GetComponent<Animator>().SetTrigger("BossBattle");
            BossMob.GetComponent<BossMob>().CutFinish = true;
            BossMob.transform.position = Vector3.zero;
            StartCut = true;
            MainCam.SetActive(true);
            SubCam.SetActive(false);
            return;
        }

        switch (MoveTime)
        {
            case 0:
                firstMove();
                break;
            case 1:
                SecondMove();
                break;
            case 2:
                ThirdMove();
                break;
            case 3:
                FourthMove();
                break;
            case 4:
                LastMove();
                break;
            default:
                break;
        }

        if (transform.position == nowPos || CutTime > 1f)
        {
            MoveTime++;
            CutTime = 0f;
            if (MoveTime >= 5f)
                return;
            BossMob.transform.Rotate(new Vector3(0f, 90f, 0f));
        }

        if(MoveTime == 4f)
        {
            BossMob.GetComponent<Animator>().SetTrigger("Arrive");
        }
    }

    public void SpawnBoss()
    {
        BossMob = Instantiate(BossPrefeb, new Vector3(60f, 60f, 60f), Quaternion.identity);
        BossMob.GetComponent<Animator>().SetTrigger("BossAppear");
        BossMob.GetComponent<BossMob>().Player = Stage_Manager.instance.myPlayerReturn();
        BossMob.transform.Rotate(new Vector3(0f, 180f, 0f));
        MainCam.SetActive(false);
        SubCam.SetActive(true);
        StartCut = false;
    }

    public GameObject BossReturn()
    {
        return BossMob;
    }
}

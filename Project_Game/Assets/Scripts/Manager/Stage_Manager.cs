using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Stage_Manager : MonoBehaviour
{
    public static Stage_Manager instance;
    private void Awake()
    {
        instance = this;
    }
    //싱글톤 패턴

    [SerializeField] private GameObject myPlayer = null;
    [SerializeField] private UIManager MyUI = null;
    [SerializeField] private ScaleManager Scalemanager = null;
    [SerializeField] private SpawnMonster mySpawner = null;
    [SerializeField] private BulletManager myBulletManager = null;
    [SerializeField] private CannonCutScene myCutScene = null;
    [SerializeField] private BossCutScene BossCut = null;
    [SerializeField] private SceneChange SceneChanger = null;
    private GameObject myBoss = null;
    private Player_con myPlayer_con = null;



    public bool Right_mouse = false;
    
    void Start()
    {
        myPlayer_con = myPlayer.GetComponent<Player_con>();
        //마우스를 게임화면의 중심에 가두고 커서가 안보이게 하는것
        //esc를 누르면 마우스가 나타나 게임화면을 빠져나갈수있음
    }

    // Update is called once per frame
    void Update()
    {
        if(mySpawner)
        mySpawner.DamageSpawnMob();
        if (!MyUI.StopOptionTime)
        {
            if (myCutScene)
            {
                myCutScene.CannonUpdate();
                if (myCutScene.StartCut)
                    return;
            }
            if (BossCut)
            {
                myBoss = BossCut.BossReturn();
                if (BossCut.StartCut == false)
                {
                    BossCut.CutSceneMove();
                    return;
                }
            }
            myPlayer_con.PlayerUpdate();
            MyUI.CheckImage();
            Scalemanager.ScaleManagerUpdate();
            if(mySpawner)
                mySpawner.SpawnMonsterUpdate();
            if(myBulletManager)
                myBulletManager.BulletUpdate();
            if (myBoss)
                myBoss.GetComponent<BossMob>().BossUpdate();
            }
        MyUI.myOption();
    }

    private void FixedUpdate()
    {
        if (myCutScene)
        {
            if (myCutScene.StartCut)
                return;
        }
        if (!MyUI.GetComponent<UIManager>().StopOptionTime)
            myPlayer_con.Run();
    }

    public GameObject myPlayerReturn()
    {
        return myPlayer;
    }

    public ScaleManager myScaleReturn()
    {
        return Scalemanager;
    }

    public void AimActive(bool b)
    {
        MyUI.Aim.gameObject.SetActive(b);
    }

    public SpawnMonster mySapwnMob()
    {
        return mySpawner.GetComponent<SpawnMonster>();
    }

    public UIManager ReturnUI()
    {
        return MyUI;
    }

    public SceneChange ReturnChanger()
    {
        return SceneChanger;
    }


}

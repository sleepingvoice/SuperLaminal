using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_con : MonoBehaviour
{
    private Rigidbody playerRigid;
    private Rotate myRotate;

    private float InputRunX;
    private float InputRunZ;
    private bool JumpCheck = false;

    public bool Mouse_Click_Check = false;

    [Header("Script Setting")]
    public ClickEvent myMouse;
    public PlayerHealthBar myHpBar = null;
    public GameObject myCam;//메인카메라


    [Header("Speed Setting")]

    public float runSpeed = 5f;
    //플레이어의 이동속도
    public float RoataeSpeed = 10f;
    //플레이어의 회전속도
    public float jumpSpeed = 10f;

    [Header("Player info")]
    public float Hp = 100;
    public float Attack;


    

    void Awake()
    {
        playerRigid = GetComponent<Rigidbody>();
        myRotate = GetComponent<Rotate>();
        myMouse = GetComponent<ClickEvent>();
        myCam = GetComponentInChildren<Camera>().gameObject;
    }

    public void Run()
    {
        if (InputRunZ != 0|| InputRunX!=0)
        {
            Vector3 moveDistanceZ = InputRunZ * transform.forward * runSpeed * Time.deltaTime;
            Vector3 moveDistanceX = InputRunX * transform.right * runSpeed * Time.deltaTime;
            Vector3 moveDistance = moveDistanceX + moveDistanceZ;
            playerRigid.MovePosition(playerRigid.position + moveDistance);
        }
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space)&& JumpCheck == true)
        {
            playerRigid.AddForce(Vector3.up * jumpSpeed,ForceMode.Impulse);
            JumpCheck = false;
        }
    }

    private void player_Move()
    {
        InputRunZ = Input.GetAxis("Vertical");
        InputRunX = Input.GetAxis("Horizontal");
        myRotate.xRotation(RoataeSpeed);
    }

    private void mouseCheck()
    {
        myMouse.MouseClick(myCam, RoataeSpeed);
        Mouse_Click_Check = myMouse.ClickReturn();
    }

    private void CameraUpdate()
    {
        //카메라가 가지고있는것중 업데이트 할것
        myCam.GetComponent<Camera_con>().Camera_rotate();
        myCam.GetComponent<Camera_Ray>().CameraRay();
        RoataeSpeed = myCam.GetComponent<Camera_con>().RoatateSpeed;
    }

    private void HpUpdate()
    {
        if (myHpBar != null) {
            myHpBar.BarValue = Hp;
            if (Hp <= 0)
            {
                Stage_Manager.instance.ReturnChanger().NextScene();
            }
        }
    }

    public void PlayerUpdate()
    {
        //게임매니저 업데이트에서 돌릴것
        player_Move();
        mouseCheck();
        CameraUpdate();
        Jump();
        HpUpdate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Flat"||collision.gameObject.layer == 9)
        {
            JumpCheck = true;
        }
    }
}

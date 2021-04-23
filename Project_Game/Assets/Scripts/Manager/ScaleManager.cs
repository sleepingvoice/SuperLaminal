using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScaleManager : MonoBehaviour
{
    private GameObject Object = null;
    private Player_con Player = null;

    // 플레이어로부터 오브젝트와 벽의 잡았을때 거리와 현재 거리 
    public float Dis_Obj_now = 0f; // 갱신되는 거리
    private float Dis_Obj_first = 0f; // 잡았을 때 거리
    public float Dis_Wall_now = 0f;
    private float Dis_Wall_first = 0f;

    private float Obj_Min_Scale = 0f;
    private float Obj_Max_Scale = 0f;

    // player 와 obj 사이 최소 거리
    private float Min_PO_Distance = 1.0f;
    // obj 와 wall 사이 최소 거리
    public float Min_OW_Distance = 0f;

    // 처음값의 몇배인지를 저장하는 변수
    public float Double_before = 0f;
    public float Double_after = 0f;

    public Vector3 Scale_First = Vector3.zero;
    public float Distance_First = 0;

    public float Distance = 0f;

    private RaycastHit Obj_ray = new RaycastHit();
    private RaycastHit Wall_ray = new RaycastHit();

    private Camera_Ray Camera_ray = null;

    // 인스펙터에서 스케일증가의 비율을 보정할 값
    [SerializeField] private float Ratio_Weight = 1.0f;
    [SerializeField] private float Ratio_Distance = 1.0f;




    private void Start()
    {
        Player = Stage_Manager.instance.myPlayerReturn().GetComponent<Player_con>();
        Camera_ray = Player.GetComponentInChildren<Camera_Ray>();        
    }


    // 잡았을 때 실행
    public void CheckObjInfo(GameObject Obj)
    {
        Object = Obj;
        Obj_Min_Scale = Object.GetComponent<Object_Con>().Obj_Min_Scale;
        Obj_Max_Scale = Object.GetComponent<Object_Con>().Obj_Max_Scale;        
        Dis_Obj_first = Vector3.Distance(Object.transform.position, Player.gameObject.transform.position);
        Dis_Wall_first = Vector3.Distance(Wall_ray.point, Player.gameObject.transform.position);
    }

    private void ManagerSet()
    {
        Camera_ray.RayGet_Wall(out Wall_ray);
        Camera_ray.RayGet_Obj(out Obj_ray);
        if (Object != null)
        {
            Dis_Obj_now = Vector3.Distance(Object.transform.position, Player.gameObject.transform.position);
            //오브젝트의 현재 거리
            Dis_Wall_now = Vector3.Distance(Wall_ray.point, Player.gameObject.transform.position);
            //벽의 현재 거리

            Min_OW_Distance = Dis_Wall_now * 0.3f;

            ContactCheck();

            if (Camera_ray.ray_ObjCheck)
            {
                Dis_Obj_now = Vector3.Distance(Obj_ray.point, Player.gameObject.transform.position);
                //잡고있는 오브젝트를 제외한 오브젝트의 거리
                Double_after = CalcRatio(Dis_Obj_now, Dis_Wall_first);
            }
            else
                Double_after = CalcRatio(Dis_Wall_now, Dis_Wall_first);
            //obj_double_Weight의 값을 정해줌
        }
    }

    private void ChangeScale()
    {
        if (!Camera_ray.ray_WallCheck)
        {
            return;
        }
        if (Double_before == Double_after)
        {
            return;
        }
        
        
        if (Player.Mouse_Click_Check)
        {            
            if (Scale_First == Vector3.zero)
                Scale_First = Object.transform.localScale;
            if (Double_after == 1)
                return;
            Object.transform.localScale = Scale_First * Double_after * Ratio_Weight;
            //변해야할 크기 = 원본크기 * 거리 당 커져야할 비율 * 보정비율

            // Obj의 Scale이 0.5 와 4.0 사이값만 가지게 함
            if (Obj_Min_Scale != 0 && Obj_Max_Scale != 0)
            {
                if (Object.transform.localScale.x <= Obj_Min_Scale)
                {
                    Object.transform.localScale = Vector3.one * Obj_Min_Scale;
                    return;
                }
                if(Object.transform.localScale.x >= Obj_Max_Scale)
                {
                    Object.transform.localScale = Vector3.one * Obj_Max_Scale;
                    return; 
                }
            }
        }
    }

    private void ChangeDistance()
    {
        if (!Camera_ray.ray_WallCheck)
        {
            return;
        }
        if (Double_before == Double_after)
        {
            return;
        }

        if (Player.Mouse_Click_Check)
        {
            if (Distance_First == 0)
                Distance_First = Vector3.Distance(Player.transform.position, Object.transform.position);
            if (Double_after == 1)
                return;
            if (isWithin_MinOWDisRange())
                Distance_First -= Min_OW_Distance * Time.deltaTime * 0.5f;
            Distance = Distance_First * Double_after * Ratio_Distance;           
            if (Distance <= Min_PO_Distance)
                Distance = Min_PO_Distance;
            if (Distance >= Dis_Wall_now)
            {
                Distance = Dis_Wall_now - Min_OW_Distance;
            }
            
            Double_before = Double_after;
        }
    }

    // _now는 계속해서 변하는 Obj의 distance값, _first는 Obj를 잡았을 때의 distance값
    // _double은 now dis값이 first dis값의 몇배인지를 구하여 반환해주는 값
    private float CalcRatio(float _now, float _first)
    {
        if (_now != 0f && _first != 0f)
        {
            float tmp = _now / _first;
            return tmp;
        }
        return 0f;
    }

    // obj의 point(표면)을 가져와 player와의 거리 계산 후 너무 가까우면 못잡게 함
    public bool CatchDis()
    {
        float DisObjPointWithPlayer = Vector3.Distance(Player.transform.position, Obj_ray.point);
        if(DisObjPointWithPlayer >= 1.5f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // DisWallnow - Distance의 값(Obj와 Wall 사이의 거리) 이/가  Min_OW_Distance값보다 작으면 true를 반환해준다
    public bool isWithin_MinOWDisRange()
    {
        if (Dis_Wall_now - Distance <= Min_OW_Distance && Distance != Min_PO_Distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ContactCheck()
    {
        // obj가 벽에 박혀들어가는 경우 방지
        if(Distance <= Min_PO_Distance + 0.1f && Min_OW_Distance <= 0.6f)
        {
            if (Object != null)
            {
                if (Object.GetComponent<Object_Con>().isContact)
                {
                    Object.GetComponent<Collider>().isTrigger = false;
                }
                else
                {
                    Object.GetComponent<Collider>().isTrigger = true;
                }
            }
        }
        if(!Player.Mouse_Click_Check)
            Object.GetComponent<Collider>().isTrigger = false;
    }

    public void SetDistance(float _Obj, float _Wall)
    {
        Dis_Obj_first = _Obj;
        Dis_Wall_now = _Wall;
    }

    // GameManager에서 업데이트 시킬 것
    public void ScaleManagerUpdate()
    {
        ManagerSet();
        ChangeScale();
        ChangeDistance();
    }
}

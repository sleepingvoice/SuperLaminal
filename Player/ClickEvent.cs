using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEvent : MonoBehaviour
{
    public bool obj_Catch = false;

    private RaycastHit obj_hit = new RaycastHit();
    private Vector3 Obj_Pos_first = new Vector3();

    [SerializeField] private BulletManager bullet = null;

    public GameObject obj = null;
    public float Obj_Dis = 0;
    public float speed = 0;
    public bool Trans_Door = false;
    public float Rotate_Limit = 0f;

    private void Awake()
    {
        bullet = FindObjectOfType<BulletManager>();
    }

    public void MouseClick(GameObject cam, float RotateSpeed)
    {
        RayCatch(cam);//cam의 raycase_object를 반환시켜 obj_hit에 넣어주는 함수

        if (Input.GetMouseButtonDown(0) || Trans_Door == true)
        {
            if (!obj_Catch && Trans_Door == true)
            {
                Trans_Door = false;
                return;
            }
            if (!obj_Catch)
            {
                if (Stage_Manager.instance.myScaleReturn().CatchDis())
                {
                    //클릭을 처음할경우
                    if (obj_hit.transform != null)
                    {
                        if (obj_hit.transform.gameObject.GetComponent<Collider>().isTrigger == true)
                        {
                            return;
                        }
                        obj = obj_hit.transform.gameObject;
                    }
                    //잡을 물체를 obj에 저장함
                    if (cam.GetComponent<Camera_Ray>().ray_ObjCheck)
                    {
                        Obj_Dis = Vector3.Distance(transform.position, obj.transform.position);
                        Obj_Pos_first = transform.position + cam.transform.forward * Obj_Dis + cam.transform.localPosition;
                        obj_Catch = true;
                        obj.layer = 10;
                        Stage_Manager.instance.myScaleReturn().CheckObjInfo(obj);
                        obj.GetComponent<Rigidbody>().useGravity = false;
                        obj.GetComponent<Rigidbody>().freezeRotation = true;
                        obj.GetComponent<Collider>().isTrigger = true;
                        obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    }
                    //오브젝트를 처음 클릭했을때 오브젝트 설정
                }
            }
            else
            {
                //클릭한뒤 한번더 클릭을 했을 경우
                obj_Catch = false;
                Stage_Manager.instance.myScaleReturn().Scale_First = Vector3.zero;
                Stage_Manager.instance.myScaleReturn().Distance_First = 0;
                Stage_Manager.instance.myScaleReturn().Distance = 0;
                obj.GetComponent<Collider>().isTrigger = false;
                if (obj != null)
                {
                    obj.layer = 9;
                    obj.GetComponent<Rigidbody>().useGravity = true;
                    obj.GetComponent<Rigidbody>().freezeRotation = false;
                }
            }
        }
        if (obj_Catch)
        {
            if (bullet != null)
            {
                bullet.GetBullet();
                if (bullet.isObtain)
                    return;
                else
                {
                    obj.transform.position = Vector3.Lerp(obj.transform.position, Obj_Pos_first, Time.deltaTime * speed);
                    //처음 클릭했을경우 오브젝트의 위치를 고정시킴
                    if (Stage_Manager.instance.myScaleReturn().Distance != 0)
                    {
                        Obj_Dis = Stage_Manager.instance.myScaleReturn().Distance;
                    }
                    Obj_Pos_first = transform.position + cam.transform.forward * Obj_Dis + cam.transform.localPosition;

                    obj.GetComponent<Rotate>().Obj_Traget_Look(this.gameObject, Rotate_Limit);
                    //오브젝트의 회전                                
                }
            }
            else
            {
                obj.transform.position = Vector3.Lerp(obj.transform.position, Obj_Pos_first, Time.deltaTime * speed);
                //처음 클릭했을경우 오브젝트의 위치를 고정시킴
                if (Stage_Manager.instance.myScaleReturn().Distance != 0)
                {
                    Obj_Dis = Stage_Manager.instance.myScaleReturn().Distance;
                }
                Obj_Pos_first = transform.position + cam.transform.forward * Obj_Dis + cam.transform.localPosition;

                obj.GetComponent<Rotate>().Obj_Traget_Look(this.gameObject, Rotate_Limit);
            }
        }
        if (bullet != null)
            bullet.ShootBullet();
    }

    public bool ClickReturn()
    {
        return obj_Catch;
    }

    public float DistanceReturn()
    {
        return Obj_Dis;
    }

    private void RayCatch(GameObject cam)
    {
        cam.GetComponent<Camera_Ray>().RayGet_Obj(out obj_hit);
    }


}

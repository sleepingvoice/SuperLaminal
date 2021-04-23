using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Ray : MonoBehaviour
{
    private RaycastHit hit_Wall = new RaycastHit();
    private RaycastHit hit_Obj = new RaycastHit();
    private Ray ray_Wall;
    private Ray ray_Obj;

    private Camera myCam;
    private float max_ray_distance = 500.0f;

    private int layerMaskWall;
    private int layerMaskObject;

    public bool ray_WallCheck = false;//ray_Wall이 있는지 없는지 체크
    public bool ray_ObjCheck = false;// ray_Obj가 있는지 없는지 체크

    private void Start()
    {
        myCam = GetComponent<Camera>();
        layerMaskWall = (1 << LayerMask.NameToLayer("Wall")+(1<<LayerMask.NameToLayer("Door")));//Wall 레이어만 충돌체크함
        layerMaskObject = 1 << LayerMask.NameToLayer("Object");
    }



    public void CameraRay()
    {
        ray_Wall = myCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
        ray_Obj = myCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));

        if (Physics.Raycast(ray_Wall, out hit_Wall, max_ray_distance, layerMaskWall))
        {
            Debug.DrawLine(ray_Wall.origin, hit_Wall.point, Color.green);
            //Debug.Log("캐릭터가 바라보고 있는 벽 이름 ->" + hit_Wall.transform.name);
            //Debug.Log("벽위치 " + hit_Wall.point);
            //캐릭터가 바라보고 있는 벽
            ray_WallCheck = true;
        }
        else
        {
            ray_WallCheck = false;
        }

        if (Physics.Raycast(ray_Obj, out hit_Obj, max_ray_distance, layerMaskObject))
        {
            Debug.DrawLine(ray_Obj.origin, hit_Obj.point, Color.red);
            //Debug.Log("캐릭터가 바라보고 있는 오브젝트 이름 ->" + hit_Obj.transform.name);
            //Debug.Log("오브젝트위치 " + hit_Obj.point);
            //캐릭터가 바라보고 있는 오브젝트
            
            ray_ObjCheck = true;
        }
        else
        {
            ray_ObjCheck = false;
        }

    }

    public void RayGet_Obj(out RaycastHit Obj)
    {
        Obj = hit_Obj;
    }

    public void RayGet_Wall(out RaycastHit Wall)
    {
        Wall = hit_Wall;
    }
}

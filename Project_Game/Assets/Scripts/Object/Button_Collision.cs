using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Collision : MonoBehaviour
{
    public float speed = 0f;
    public bool noCollision = true;

    private Vector3 Up_position = Vector3.zero;
    private Vector3 Down_position = Vector3.zero;
    private float dis_first = 0f;
    private float dis = 0f;
    private GameObject Obj_Touch=null;

    private void Awake()
    {
        Up_position = transform.position;
        Down_position = transform.position + new Vector3(0, -0.1f, 0);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            noCollision = false;
            dis_first = Vector3.Distance(transform.position, collision.transform.position);
            Obj_Touch = collision.gameObject;
        }
    }

    private void Update()
    {
        Trans_Pos();

        if (Obj_Touch != null)
        {
            dis = Vector3.Distance(transform.position, Obj_Touch.transform.position);
            if (dis > dis_first+2f)
            {
                noCollision = true;
                Obj_Touch = null;
                dis_first = 0f;
                dis = 0f;
            }
        }
    }

    private void Trans_Pos()
    {
        if (noCollision)
        {
            if (transform.position.y >= Up_position.y)
                return;
            transform.position = Vector3.Lerp(transform.position, Up_position, Time.deltaTime * speed);
        }
        else
        {
            if (transform.position.y <= Down_position.y)
                return;
            transform.position = Vector3.Lerp(transform.position, Down_position, Time.deltaTime * speed);
        }
    }

}

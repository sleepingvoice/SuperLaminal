using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_con : MonoBehaviour
{
    private Rotate myRotate;
    public float RoatateSpeed = 10f;
    public float xRotate = 0;
    // Start is called before the first frame update
    void Start()
    {
        myRotate = GetComponent<Rotate>();
    }

    // Update is called once per frame


    public void Camera_rotate()
    {
        //카메라 상하 회전처리

        xRotate = transform.eulerAngles.x;

        if (xRotate <= 70f)
        {
            myRotate.yRotation(RoatateSpeed);
        }
        else if (xRotate >= 280f)
        {
            myRotate.yRotation(RoatateSpeed);
        }
        else
        {
            if (xRotate > 180)
            {
                Quaternion rotation = Quaternion.Euler(290f, 0, 0);
                Quaternion Middle = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
                Middle.z = 0;
                Middle.y = 0;
                transform.localRotation = Middle;
            }
            else
            {
                Quaternion rotation = Quaternion.Euler(60f, 0, 0);
                Quaternion Middle = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
                Middle.z = 0;
                Middle.y = 0;
                transform.localRotation = Middle;
            }
        }
    }

    public void MouseSensitiveChange(float f)
    {
        RoatateSpeed = f*20;
    }

}

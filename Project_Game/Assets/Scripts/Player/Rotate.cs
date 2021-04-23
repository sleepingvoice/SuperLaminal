using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float yRotate;
    public float rotespeed = 0;

    private Quaternion obj_rotate = new Quaternion();
    private float mouseX = 0f;
    private float mouseY = 0f;
    private float mouseMove = 0f;

    public void yRotation(float Speed)
    {
        transform.Rotate(Vector3.right, -Input.GetAxis("Mouse Y") * Speed);
    }
    public void xRotation(float Speed)
    {
        float xRotate = Input.GetAxis("Mouse X") * Speed;
        transform.Rotate(Vector3.up, xRotate);
    }

    public void Obj_Traget_Look(GameObject target, float rotateLimit)
    {
        Vector3 relativePos = target.transform.position - transform.position;
        obj_rotate = Quaternion.LookRotation(relativePos);

        mouseX += Input.GetAxis("Mouse X");
        mouseY += Input.GetAxis("Mouse Y");

        mouseMove = Mathf.Sqrt(Mathf.Pow(mouseX, 2) + Mathf.Pow(mouseY, 2));

        if (mouseMove >= rotateLimit)
        {
            transform.rotation = obj_rotate;
            mouseX = 0f;
            mouseY = 0f;
        }
    }
}

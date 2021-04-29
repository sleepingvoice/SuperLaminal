using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Con : MonoBehaviour
{
    public GameObject Door = null;
    public GameObject Button = null;
    public float speed = 0;
    public bool Btn = true;

    private Vector3 move_pos = new Vector3(5f, 0, 0);
    public Vector3 Pos_first = Vector3.zero;

    private void Awake()
    {
        if (Door != null)
        {
            Pos_first = Door.transform.localPosition;
        }
    }

    // Update is called once per frame
    // Update is called once per frame
    void Update()
    {
        if (Button == null)
        {
            return;
        }
        Btn = Button.GetComponent<Button_Collision>().noCollision;
        if (!Btn)
        {
            if (Door.transform.position == Pos_first + move_pos)
                return;
            Door.transform.localPosition = Vector3.Lerp(Door.transform.localPosition, Pos_first + move_pos, Time.deltaTime * speed);
        }
        else
        {
            if (Door.transform.position == Pos_first)
                return;
            Door.transform.localPosition = Vector3.Lerp(Door.transform.localPosition, Pos_first, Time.deltaTime * speed);
        }
    }
}

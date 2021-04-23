using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            if (other.GetComponent<Player_con>())
                other.GetComponent<Player_con>().myMouse.Trans_Door = true;
        }
    }

}

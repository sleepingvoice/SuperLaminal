using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCam : MonoBehaviour
{
    public BossCutScene BossCut = null;
    private GameObject myMob = null;

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            myMob = BossCut.BossReturn();
        }

        if (myMob != null)
        {
            transform.rotation = Quaternion.LookRotation(myMob.transform.position);
        }
    }
}

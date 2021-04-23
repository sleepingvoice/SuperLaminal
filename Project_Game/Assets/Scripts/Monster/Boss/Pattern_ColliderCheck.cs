using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern_ColliderCheck : MonoBehaviour
{
    private bool AttackCheck = false;
    public ParticleSystem myEffect = null;

    private void Start()
    {
        if (myEffect == null)
            return;
        if (myEffect.isPlaying == true)
        {
            myEffect.Stop();
            myEffect.Clear();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == 11)
        {
            AttackCheck = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 11)
        {
            AttackCheck = false;
        }
    }

    public bool ReturnAttackCheck()
    {
        return AttackCheck;
    }

    public void StartEffect()
    {
        if (myEffect == null)
            return;
        myEffect.gameObject.SetActive(true);
        myEffect.Play();
    }
}

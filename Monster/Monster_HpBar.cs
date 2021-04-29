using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster_HpBar : MonoBehaviour
{
    public Transform myMob = null;
    private Canvas canvas = null;
    private GameObject myHpBar = null;
    private float max_Hp = 0f;
    private Vector3 First_Scale = Vector3.zero;
    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        myHpBar = GetComponentInChildren<Slider>().gameObject;
        First_Scale = myHpBar.transform.localScale;
    }

    private void Update()
    {
        HPbarPos();
        if(myMob!=null)
        {
            myHpBar.GetComponent<Slider>().value = (myMob.gameObject.GetComponent<Monster_Con>().Now_Hp / max_Hp);
        }
    }
    

    public void HPbarPos()
    {
        if (!Camera.main)
            return;
        Vector3 ScreenPos = Camera.main.WorldToScreenPoint(myMob.position + new Vector3(0f, 3f, 0f));

        myHpBar.GetComponent<RectTransform>().position = ScreenPos;
        if (ScreenPos.z < 0)
            myHpBar.SetActive(false);
        else
            myHpBar.SetActive(true);

        HpBarSclae(ScreenPos);
    }

    public void SetmyMob(Transform mob)
    {
        myMob = mob;
        max_Hp = mob.GetComponent<Monster_Con>().Now_Hp;
    }

    private void HpBarSclae(Vector3 ScreenPos)
    {
        if (ScreenPos.z <= 10)
            myHpBar.transform.localScale = First_Scale * (10 - ScreenPos.z) * (5 / 3);
        else
            myHpBar.transform.localScale = First_Scale;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option_UI : MonoBehaviour
{
    public Slider mySlider = null;
    private Text myText = null;

    public bool sound = false;
   
    private void Awake()
    {
        myText = GetComponent<Text>();
        if (sound)
            SoundCheck();
        else
            sensitivity();
    }

    private void Update()
    {
        myText.text = "" + Mathf.FloorToInt(mySlider.value * 100);
    }

    private void SoundCheck()
    {
        if(Stage_Manager.instance.ReturnChanger().mySound != null)
        {
            mySlider.value = Stage_Manager.instance.ReturnChanger().mySound.returnVolume();
        }
    }

    private void sensitivity()
    {
        if(Stage_Manager.instance.myPlayerReturn()!=null)
        {
            mySlider.value = Stage_Manager.instance.myPlayerReturn().GetComponent<Player_con>().RoataeSpeed/20;
        }
    }
}

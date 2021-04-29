using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager SoundIns;

    private float Main_Sound_first = 1.0f;

    private AudioSource MySound = null;

    private void Awake()
    {
        if (SoundIns != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            SoundIns = this;
            DontDestroyOnLoad(this);
        }
        if (MySound)
            return;
        MySound = GetComponent<AudioSource>();
        MySound.volume = Main_Sound_first;
        if (PlayerPrefs.HasKey("Main"))
        {
            MySound.volume = PlayerPrefs.GetFloat("Main");
        }
    }


    public void Change_Main_Sound(float Main)
    {
        MySound.volume = Main_Sound_first * Main;
        PlayerPrefs.SetFloat("MainSound", MySound.volume);
    }


    public void Change_AudioSource(AudioClip SoundClip)
    {
            MySound.clip = SoundClip;
        if (MySound.clip != null)
            MySound.Play();
    }

    public float returnVolume()
    {
        return MySound.volume;
    }
}

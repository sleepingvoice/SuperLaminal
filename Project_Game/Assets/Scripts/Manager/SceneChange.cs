using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField] private bool Credit_Yes = false;
    [SerializeField] private GameObject Credit_First = null;
    [SerializeField] private GameObject Credit_Scene = null;
    [SerializeField] private AudioClip SceneAudio = null;

    private bool cursorLock = false;

    public SoundManager mySound=null;

    private void Awake()
    {
        mySound = SoundManager.SoundIns;
        if(SceneAudio != null && mySound != null)
        {
            mySound.Change_AudioSource(SceneAudio);
        }
    }

    private void Update()
    {
        if (Credit_Yes)
        {
            CreditScene();
            Credit_Yes = false;
        }

        if (Stage_Manager.instance != null&&Stage_Manager.instance.ReturnUI().StopOptionTime)
            return;

        if(SceneManager.GetActiveScene().name != "Stage1" && SceneManager.GetActiveScene().name != "Stage2")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            cursorLock = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            cursorLock = true;
        }
    }

    public void newGame()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void Credit()
    {
        SceneManager.LoadScene("Credit");
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void Option()
    {
        SceneManager.LoadScene("Option");
    }

    public void ButtonMouseEnter(Button myBtk)
    {
        myBtk.GetComponentInChildren<Text>().color = new Color(144 / 255f, 186 / 255f, 178 / 255f);
    }

    public void ButtonMouseExit(Button myBtk)
    {
        myBtk.GetComponentInChildren<Text>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
    }

    public void NextScene()
    {
        SceneManager.LoadScene("Stage2");
    }

    public void TitleScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void CreditScene()
    {
        StartCoroutine("CreditTime");
    }

    IEnumerator CreditTime()
    {
        Credit_First.SetActive(true);
        yield return new WaitForSeconds(5);
        Credit_First.SetActive(false);
        Credit_Scene.SetActive(true);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("StartScene");
    }

    public void Main_Sound_Change(float sound)
    {
        if (mySound == null)
            return;
        mySound.Change_Main_Sound(sound);
    }


}

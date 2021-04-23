using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TitleManager : MonoBehaviour
{

    public void newGame()
    {
        SceneManager.LoadScene("SampleScene");
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

}

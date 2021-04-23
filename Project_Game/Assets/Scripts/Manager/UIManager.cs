using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public RawImage Aim = null;
    [SerializeField] private Texture dot = null;
    [SerializeField] private Texture handOpen = null;
    [SerializeField] private Texture handClose = null;
    [SerializeField] private GameObject OptionMenu = null;
    private GameObject player = null;
    private GameObject Cam = null;

    
    private bool ObjCheck = false;
    private bool CatchCheck = false;
    private RaycastHit hit = new RaycastHit();

    public bool StopOptionTime = false;

    private void Start()
    {
        StartCoroutine("lastStart");

    }

    private IEnumerator lastStart()
    {
        yield return new WaitForFixedUpdate();
        player = Stage_Manager.instance.myPlayerReturn();
        Cam = player.GetComponent<Player_con>().myCam;
    }
    //스타트에 cam을 받는 것을 넣게 된다면 player의 myCam이 들어오기전에 start가 먼저 돌아 null값이 나오는 경우가 있다
    //그래서 코루틴을 통해 FixedUpdate이후 Cam을 받아와서 player의 myCam이 들어온후 값을 받아 올수 있도록 한다.

    public void CheckImage()
    {
        if(Cam == null)
        {
            return;
        }
        ObjCheck = Cam.GetComponent<Camera_Ray>().ray_ObjCheck;
        CatchCheck = player.GetComponent<Player_con>().Mouse_Click_Check;
        Cam.GetComponent<Camera_Ray>().RayGet_Obj(out hit);
        if(hit.transform != null)
        {
            if(hit.transform.gameObject.GetComponent<Collider>().isTrigger)
            {
                Aim.texture = dot;
                return;
            }
        }
        if (ObjCheck == true && Stage_Manager.instance.myScaleReturn().CatchDis())
        {
            Aim.texture = handOpen;
            return;
        }
        if (CatchCheck == true)
        {
            Aim.texture = handClose;
            return;
        }
        Aim.texture = dot;
    }

    public void myOption()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopOptionTime = !StopOptionTime;
            OptionMenu.SetActive(StopOptionTime);
            Cursor.visible = StopOptionTime;
            if (StopOptionTime)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        if(StopOptionTime)
        {
            TitleBack();
        }
    }

    public void TitleBack()
    {
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}

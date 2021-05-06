using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMainMenu : MonoBehaviour
{

    public GameObject CanvasObject;

    public CanvasGroup startMenuGroup;
    public CanvasGroup settingsMenuGroup;

    // Start is called before the first frame update
    void Start()
    {
        CanvasGroupChanger(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CanvasGroupChanger(int num)
    {
        CanvasGroup[] groups = {startMenuGroup, settingsMenuGroup};

        for (int i = 0; i < groups.Length; i++) 
        {
            if (i != num)
            {
                groups[i].alpha = 0;
                groups[i].blocksRaycasts = false;
            }
            else if (i == num)
            {
                groups[i].alpha = 1;
                groups[i].blocksRaycasts = true;
            }
        }
    }

    public void Buttons(string buttons)
    {
        switch(buttons)
        {
            case "STARTMENU":
                CanvasGroupChanger(0);
                break;

            case "SETTINGSMENU":
                CanvasGroupChanger(1);
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUIManager : MonoBehaviour
{
    public TabManagerUI tabManager;

    public void Start()
    {
        
    }
    public void OnStartClick ()
    {
        tabManager.SwitchTab(TAB.Level);
    }

    public void OnExitClick()
    {
        Application.Quit();
    }
}

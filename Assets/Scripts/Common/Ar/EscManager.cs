using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscManager : MonoBehaviour
{
    [SerializeField] GameObject EscPanel;

    private void Update()
    {
        ToUpdate();    
    }
    public void ToUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            EscPanel.SetActive(!EscPanel.activeSelf);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

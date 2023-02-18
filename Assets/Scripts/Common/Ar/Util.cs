using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util : MonoSingleton<Util>
{
    public Camera mainCam { get; set; }
    public Vector3 mousePosition { get { return mainCam.ScreenToWorldPoint(Input.mousePosition); } }
    private void Awake()
    {
        mainCam = Camera.main;
    }
}

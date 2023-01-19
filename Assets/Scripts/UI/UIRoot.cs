using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class UIRoot : MonoBehaviour
{
    public Image blackPannel;

    private CanvasScaler rootCvs;

    private float nowRatio;

    public bool isPaused = false;

    private void Awake()
    {
        GameSceneClass.gUiRoot = this;

        SetCvsResolution();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 안드로이드 기기에서 백버튼처리
            // 팝업 매니져 처리
        }
    }

    private void OnApplicationFocus(bool focusStatus)
    {
        isPaused = focusStatus;

        if (!isPaused)
        {
            // 세이브 데이터 저장 처리 등
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            // 세이브 데이터 저장 처리 등
        }
        else
        {
            Screen.fullScreen = true;
        }
    }

    private void OnApplicationQuit()
    {
        // 세이브 데이터 저장 처리 등
    }

    void SetCvsResolution()
    {
        rootCvs = this.gameObject.GetComponent<CanvasScaler>();
        if (rootCvs == null)
        {
            Debug.LogWarning("rootCvs is null");
            return;
        }

        Global.refrenceResolution = new Vector2();
        Global.refrenceResolution = rootCvs.referenceResolution;
        Global.blackPannel = blackPannel;

        nowRatio = Convert.ToSingle((double)Screen.height / (double)Screen.width);
        Debug.LogFormat("해상도{0}x{1} 비율 : {2:F6}, dpi:{3}", Screen.height, Screen.width, nowRatio, Screen.dpi);

        rootCvs.referenceResolution = Global.refrenceResolution;
    }
}

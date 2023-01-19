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
            // �ȵ���̵� ��⿡�� ���ưó��
            // �˾� �Ŵ��� ó��
        }
    }

    private void OnApplicationFocus(bool focusStatus)
    {
        isPaused = focusStatus;

        if (!isPaused)
        {
            // ���̺� ������ ���� ó�� ��
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            // ���̺� ������ ���� ó�� ��
        }
        else
        {
            Screen.fullScreen = true;
        }
    }

    private void OnApplicationQuit()
    {
        // ���̺� ������ ���� ó�� ��
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
        Debug.LogFormat("�ػ�{0}x{1} ���� : {2:F6}, dpi:{3}", Screen.height, Screen.width, nowRatio, Screen.dpi);

        rootCvs.referenceResolution = Global.refrenceResolution;
    }
}

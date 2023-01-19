using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // public MGTeam gTeamManager;
    // public MGStage gStageManager;

    void Awake()
    {
        GameSceneClass._gameManager = this;

        InitCamera();

         //gTeamManager = new MGTeam();
         //gStageManager = new MGStage();

        Global.gameState = eGameState.Playing;
    }

    private void Update()
    {
        if (Global.gameState == eGameState.Playing)
        {
            // �� ��� Ŭ�������� ������Ʈ�� ���� ���߰�������
        }
    }

    void InitCamera()
    {
        Debug.Log("���������");
        if (Global.gMainCamTrm == null)
        {
            Global.gMainCamTrm = ((GameObject.Instantiate(Global.prefabsDic[ePrefabs.MainCamera])) as GameObject).transform;

            if (Global.gMainCamTrm != null)
            {
                Global.mainCam = Global.gMainCamTrm.GetComponent<Camera>();
                if (Global.mainCam == null)
                {
                    Debug.LogWarning("Global.mainCam in null");
                    return;
                }
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // public MGTeam gTeamManager;
    // public MGStage gStageManager;
    FrameWork framWork;
    void Awake()
    {
        GameSceneClass._gameManager = this;

        InitCamera();
        Global.gameState = eGameState.Playing;

        framWork = GetComponent<FrameWork>();

        framWork.FirstAwkae();
    }

    private void Start()
    {
        framWork.OnAwake();
        framWork.OnStart();
    }

    private void Update()
    {
        if (Global.gameState == eGameState.Playing)
        {
            framWork.OnUpdate();
        }
    }

    void InitCamera()
    {
        Global.gMainCamTrm = FindObjectOfType<Camera>().transform;

        if (Global.gMainCamTrm == null)
        {
            Global.gMainCamTrm = ((GameObject.Instantiate(Global.prefabsDic[ePrefabs.MainCamera])) as GameObject).transform;
        }

        if (Global.gMainCamTrm != null) //카메라 2개면 오류생길듯?
        {
            Global.mainCam = Global.gMainCamTrm.GetComponent<Camera>();
            if (Global.mainCam == null)
            {
                Debug.LogWarning("Global.mainCam in null");
                return;
            }
        }

        //if (Global.gMainCamTrm == null)
        //{
        //    Global.gMainCamTrm = ((GameObject.Instantiate(Global.prefabsDic[ePrefabs.MainCamera])) as GameObject).transform;

        //    if (Global.gMainCamTrm != null)
        //    {
        //        Global.mainCam = Global.gMainCamTrm.GetComponent<Camera>();
        //        if (Global.mainCam == null)
        //        {
        //            Debug.LogWarning("Global.mainCam in null");
        //            return;
        //        }
        //    }
        //}
    }
}


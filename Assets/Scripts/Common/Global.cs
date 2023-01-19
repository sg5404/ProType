using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Global
{
    public static Dictionary<ePrefabs, GameObject> prefabsDic;
    public static Dictionary<string, Sprite> spritesDic;

    public static Vector2 refrenceResolution;
    public static Image blackPannel;

    // ���� ���ӻ���
    public static eGameState gameState = eGameState.None;

    // ����ī�޶�
    public static Transform gMainCamTrm;
    public static Camera mainCam;

}

public enum ePrefabs
{
    None = -1,
    MainCamera,
    HEROS = 1000,
    HeroMan,
    HeroGirl,
    MANAGERS = 2000,
    GameManager,
    PoolManager,
    UI = 3000,
    UIRoot,
    UIRootLoading,
    UIRootTitle,
    UIRootGame,
    Object = 4000,
    EmptyObj,
    Cube,
}

public enum eSceneName
{
    None = -1,
    Loading,
    Title,
    Game,
}

public class GameSceneClass
{
    public static UIRoot gUiRoot;

    public static GameManager _gameManager;
    public static PoolManager _poolManager;
}

public enum eGameState
{
    None,
    Playing,
    Paused,
}
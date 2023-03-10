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

    // 현재 게임상태
    public static eGameState gameState = eGameState.None;

    // 메인카메라
    public static Transform gMainCamTrm;
    public static Camera mainCam;

    //다음으로 바뀔 씬들
    public eSceneName nextScene = eSceneName.None;
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
    Managers,

    UI = 3000,
    UIRoot,
    UIRootLoading,
    UIRootTitle,
    UIRootGame,

    Object = 4000,
    EmptyObj,
    Cube,
    InteractionObj,

    ROOM = 5000,
    BattleRoom,
    EventRoom,
    ShopRoom,
    StartRoom,
    HorizontalPass,
    VerticalPass,
}

public enum eSceneName
{
    None = -1,
    Loading,
    Title,
    Game,
    Test,
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

public enum eRoom
{
    Battle,
    Event,
    Shop,
    Start,
}

public enum ePass
{
    Horizontal,
    Vertical,
}

public enum Pos
{
    left = 0,
    right,
    top,
    bottom
}

public enum Item
{
    NONE = 0,
    A,
    B,
    C,
    D
}

public enum Rarity
{
    NORMAL = 0,
    RARE,
    EPIC,
    UNIQUE,
    LEGENDARY,
}
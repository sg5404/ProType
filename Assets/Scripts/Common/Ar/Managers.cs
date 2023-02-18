using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    public static Managers Instance
    {
        get
        {
            Init();
            return s_instance;
        }
    }
    #region CORE

    // 변수
    private UIManager _uiManager;
    private InputManager _inputManager;
    private SoundManager _soundManager;
    private ButtonManager _buttonManager;

    // 프로퍼티 
    public static UIManager UIManager { get { return Instance._uiManager; } }
    public static InputManager InputManager { get { return Instance._inputManager; } }
    public static SoundManager SoundManager { get { return Instance._soundManager; } }
    public static ButtonManager ButtonManager { get { return Instance._buttonManager; } }

    #endregion

    private void Start()
    {
        Init();
        Setinst();
    }
    void Setinst()
    {
        _uiManager = GetComponent<UIManager>();
        _inputManager = GetComponent<InputManager>();
        _soundManager = GetComponent<SoundManager>();
        _buttonManager = GetComponent<ButtonManager>();
    }
    private void Update()
    {
        //_inputManager.OnUpdate();
    }
    static void Init()
    {
        DOTween.Init(false, false, LogBehaviour.Default).SetCapacity(100, 20);
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("Managers");
            if (go == null)
            {
                go = new GameObject("Managers");
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            //s_instance._soundManager.Init();
        }
    }
}

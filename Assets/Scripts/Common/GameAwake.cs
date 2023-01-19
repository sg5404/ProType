using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

public class GameAwake : MonoBehaviour
{
    public static GameAwake Instance { get; private set; }

    private bool bFirstInit = false;    // 최초 한번만 로딩해주는 함수 처리를 위함

    private string tempStr;

    private StringBuilder _sb;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        _sb = new StringBuilder();

        //this.gameObject.hideFlags = HideFlags.HideAndDontSave;
    }

    private void Start()
    {
        // 필요한 데이터 세팅

        if (!bFirstInit)
        {
            InitOnce();

            bFirstInit = true;
        }
    }

    void InitOnce()
    {
        LoadPrefabDic();

        LoadSpritesDic();

        // 씬매니져를 생성
        MGScene.Instance.Generate();
    }

    void LoadPrefabDic()
    {
        Global.prefabsDic = new Dictionary<ePrefabs, GameObject>();

        object[] files;

        _sb.Remove(0, _sb.Length);
        _sb.Append("Prefabs/");

        files = Resources.LoadAll(_sb.ToString());
        SetPrefabDic(files);
    }

    void SetPrefabDic(object[] files)
    {
        for (int i = 0; i < files.Length; i++)
        {
            GameObject outObj;
            tempStr = GetFileName(files[i].ToString());

            if (!Global.prefabsDic.TryGetValue((ePrefabs)Enum.Parse(typeof(ePrefabs), tempStr), out outObj))
            {
                Global.prefabsDic.Add((ePrefabs)Enum.Parse(typeof(ePrefabs), tempStr), (GameObject)files[i]);
            }

        }
    }

    void LoadSpritesDic()
    {
        Global.spritesDic = new Dictionary<string, Sprite>();

        Sprite[] files;

        _sb.Remove(0, _sb.Length);
        _sb.Append("Sprites/");

        files = Resources.LoadAll<Sprite>(_sb.ToString());
        setSpriteDic(files);
    }

    void setSpriteDic(Sprite[] files)
    {
        for (int i = 0; i < files.Length; i++)
        {
            Sprite outObj;

            tempStr = GetFileName(files[i].ToString());

            if (!Global.spritesDic.TryGetValue(tempStr, out outObj))
                Global.spritesDic.Add(tempStr, (Sprite)files[i]);
        }
    }

    string GetFileName(string objName)
    {
        string rtn = null;

        rtn = objName.Substring(0, objName.IndexOf(' '));

        return rtn;
    }
}

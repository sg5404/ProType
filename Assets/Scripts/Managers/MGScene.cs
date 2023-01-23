using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;
using UnityEngine.SceneManagement;

public class MGScene : MonoBehaviour
{
    private static MGScene _instance;
    public static MGScene Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(MGScene)) as MGScene;
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.hideFlags = HideFlags.HideAndDontSave; //�Ⱥ��̰� �ϱ�
                    _instance = obj.AddComponent<MGScene>();
                }
            }

            return _instance;
        }
    }

    public void Generate() { }

    public Canvas rootCvs;
    public Transform rootTrm;
    private Transform addUiTrm;

    public eSceneName curScene, prevScene;

    private StringBuilder _sb;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        _sb = new StringBuilder();

        this.gameObject.hideFlags = HideFlags.HideAndDontSave;    //�Ⱥ��̰� �ϱ�

        // �� �Ŵ��� ȣ��� UIRoot ����
        GameObject obj = GameObject.Instantiate(Global.prefabsDic[ePrefabs.UIRoot]) as GameObject;
        if (obj != null)
        {
            print("UIRoot ����!");
            rootCvs = obj.GetComponent<Canvas>();
            rootTrm = obj.transform;
        }

        addUiTrm = null;

        InitFirstScene();
    }

    // �� �Ŵ��� ���� �� ���ʷ� �ε��� ��
    void InitFirstScene()
    {
        prevScene = eSceneName.None;
        ChangeScene(eSceneName.Title);
    }

    public void ChangeScene(eSceneName inScene)
    {
        curScene = inScene;

        _sb.Remove(0, _sb.Length);
        _sb.AppendFormat("{0}Scene", eSceneName.Loading);

        SceneManager.LoadScene(_sb.ToString());

        // �ε�ȭ���� �ʿ��Ѱ��� �ƴ� ��츦 ����

        eSceneName useScene = curScene switch
        {
            eSceneName.Title => eSceneName.Title,
            _ => eSceneName.Loading,
        };

        ChangeUi(useScene);

        //if (curScene == eSceneName.Title)
        //    ChangeUi(eSceneName.Title);
        //else
        //    ChangeUi(eSceneName.Loading);
    }

    void ChangeUi(eSceneName inScene)
    {
        // �ٲܾ��� UI �������� ������
        _sb.Remove(0, _sb.Length);
        _sb.AppendFormat("UIRoot{0}", inScene.ToString());
        ePrefabs addUiPrefab = (ePrefabs)(Enum.Parse(typeof(ePrefabs), _sb.ToString()));
        //Debug.Log(inScene.ToString());

        // ������ ������ UI�� �ִٸ� �ʱ�ȭ
        if (addUiTrm != null)
        {
            addUiTrm.SetParent(null);
            GameObject.Destroy(addUiTrm.gameObject);
        }

        // ���ο���� UI������ ���� //UIoot �ȿ� UIGame�̳� UITitle����� �뵵
        GameObject obj = GameObject.Instantiate(Global.prefabsDic[addUiPrefab]) as GameObject;
        addUiTrm = obj.transform;
        addUiTrm.SetParent(rootTrm, false);
        addUiTrm.localPosition = Vector3.zero;
        addUiTrm.localScale = new Vector3(1, 1, 1);

        if (inScene >= eSceneName.Loading)
        {
            RectTransform rts = obj.GetComponent<RectTransform>();
            rts.offsetMax = new Vector2(0, 0);
            rts.offsetMin = new Vector2(0, 0);
        }
    }

    public void LoadingDone()
    {
        prevScene = curScene;
        ChangeUi(curScene);

        if (curScene == eSceneName.Game)
        {
            GameObject.Instantiate(Global.prefabsDic[ePrefabs.PoolManager]);
            GameObject.Instantiate(Global.prefabsDic[ePrefabs.GameManager]);
            GameObject.Instantiate(Global.prefabsDic[ePrefabs.Managers]);
        }
    }
}

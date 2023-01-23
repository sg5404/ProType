using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolManager : MonoSingleton<PoolManager>
{
    // ������ ������Ʈ�� MGPool������Ʈ�� �ڽ����� �ֱ�����
    public Transform myTrm;

    // Ǯ�Ŵ����� ��ü ������Ʈ���� ePrefabsŰ��, List<CONEntity>�� ������ ����
    public Dictionary<ePrefabs, List<CONEntity>> poolListDic;

    // ��� ������ ���ӿ�����Ʈ�� ��Ƶ�
    public List<GameObject> poolAllObjList;

    // ���ӿ�����Ʈ���� �ִ밹���� ��Ƶ�
    public List<int> poolAllObjCountList;

    // ������ Ŭ������ �����Ǵ� ������
    // �� ������ ����Ʈ�� ���� �����Ѵ�
    public List<GameObject> poolCharList, poolEffectList;// , _poolEtcList , _poolProjectileList ,_poolMinionList;
    // �� ����Ʈ���� ����������
    public int poolCharCount, poolEffectCount;//, _poolEtcCnt, _poolProjectileCnt, _poolMinionCnt;
    // �� ������ ������Ʈ�� ������ ��Ƶ�
    public List<int> poolCharCountList, poolEffectCountList;// _poolEtcCntList, _poolProjectileCntList, _poolMinionCntList;

    void Awake()
    {
        GameSceneClass._poolManager = this;

        myTrm = this.transform;

        poolAllObjList = new List<GameObject>();
        poolAllObjCountList = new List<int>();

        poolCharCount = poolCharList.Count;
        poolEffectCount = poolEffectList.Count;

        // _poolEtcCnt = _poolEtcList.Count;
        // _poolProjectileCnt = _poolProjectileList.Count;
        // _poolMinionCnt = _poolMinionList.Count;

        CreatePool();
    }

    private void CreatePool()
    {
        // ĳ���� ������ŭ ��縮��Ʈ�� �߰�
        for (int i = 0; i < poolCharList.Count; i++)
        {
            poolAllObjList.Add(poolCharList[i]);
            poolAllObjCountList.Add(poolCharCountList[i]);
        }
        // ����Ʈ ������ŭ ��縮��Ʈ�� �߰�
        for (int i = 0; i < poolEffectList.Count; i++)
        {
            poolAllObjList.Add(poolEffectList[i]);
            poolAllObjCountList.Add(poolEffectCountList[i]);
        }

        // for (int i = 0; i < _poolEtcList.Count; i++)
        // {
        //     poolAllObjList.Add(_poolEtcList[i]);
        //     poolAllObjCountList.Add(_poolEtcCntList[i]);
        // }

        // for (int i = 0; i < _poolProjectileList.Count; i++)
        // {
        //     poolAllObjList.Add(_poolProjectileList[i]);
        //     poolAllObjCountList.Add(_poolProjectileCntList[i]);
        // }

        // for (int i = 0; i < _poolMinionList.Count; i++)
        // {
        //     poolAllObjList.Add(_poolMinionList[i]);
        //     poolAllObjCountList.Add(_poolMinionCntList[i]);
        // }


        poolListDic = new Dictionary<ePrefabs, List<CONEntity>>();

        for (int i = 0; i < poolAllObjList.Count; i++)
        {
            CONEntity myEn = null;
            ePrefabs myKind = ePrefabs.None;

            myEn = poolAllObjList[i].GetComponent<CONEntity>();
            if (myEn == null)
            {
                Debug.LogWarning(" **** Entity Controller Needed **** ");
                continue;
            }

            myKind = myEn.myKind;
            poolListDic[myKind] = new List<CONEntity>();

            //Ǯ�Ŵ��� �ؿ� ���Ľ�Ű��
            GameObject emptyObj = InstantiateObj(ePrefabs.EmptyObj);
            emptyObj.transform.SetParent(this.transform);
            emptyObj.name = myKind.ToString() + 's';

            for (int j = 0; j < poolAllObjCountList[i]; j++)
            {
                myEn = (InstantiateObj(myKind)).GetComponent<CONEntity>();
                myEn.transform.SetParent(emptyObj.transform);
                myEn.SetActive(false);
                myEn.SetPosition(Vector3.zero);
                poolListDic[myKind].Add(myEn);
            }
        }
    }

    /// <summary>
    /// �۷ι��� �ִ� ������ Dictionary ���� �������� ����
    /// </summary>
    /// <param name="inObj"></param>
    /// <returns></returns>
    private GameObject InstantiateObj(ePrefabs inObj)
    {
        GameObject myGo = GameObject.Instantiate(Global.prefabsDic[inObj]) as GameObject;

        return myGo;
    }

    public CONEntity CreateObj(ePrefabs inObj, Vector3 inPos)
    {
        CONEntity createdEn = null;
        bool bNotEnough = true;

        for (int i = 0; i < poolListDic[inObj].Count; i++)
        {
            if (!poolListDic[inObj][i].IsActive())
            {
                createdEn = poolListDic[inObj][i];
                createdEn.SetActive(true);
                bNotEnough = false;
                break;
            }
        }

        if (bNotEnough)
        {
            createdEn = InstantiateObj(inObj).GetComponent<CONEntity>();
            poolListDic[inObj].Add(createdEn);
        }

        if (createdEn != null)
            createdEn.SetPosition(inPos);

        createdEn.myTrm.parent = null;

        return createdEn;
    }

    public void RemoveObj(CONEntity inEn)
    {
        inEn.SetActive(false);
    }
}

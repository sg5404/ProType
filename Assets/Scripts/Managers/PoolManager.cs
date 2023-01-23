using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolManager : MonoSingleton<PoolManager>
{
    // 생성한 오브젝트를 MGPool오브젝트의 자식으로 넣기위해
    public Transform myTrm;

    // 풀매니져의 전체 오브젝트들을 ePrefabs키로, List<CONEntity>를 값으로 저장
    public Dictionary<ePrefabs, List<CONEntity>> poolListDic;

    // 모든 종류의 게임오브젝트를 담아둠
    public List<GameObject> poolAllObjList;

    // 게임오브젝트들의 최대갯수를 담아둠
    public List<int> poolAllObjCountList;

    // 에디터 클래스와 연동되는 변수들
    // 각 종류별 리스트를 따로 관리한다
    public List<GameObject> poolCharList, poolEffectList;// , _poolEtcList , _poolProjectileList ,_poolMinionList;
    // 각 리스트들의 갯수보관용
    public int poolCharCount, poolEffectCount;//, _poolEtcCnt, _poolProjectileCnt, _poolMinionCnt;
    // 각 종류별 오브젝트의 갯수를 담아둠
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
        // 캐릭터 갯수만큼 모든리스트에 추가
        for (int i = 0; i < poolCharList.Count; i++)
        {
            poolAllObjList.Add(poolCharList[i]);
            poolAllObjCountList.Add(poolCharCountList[i]);
        }
        // 이펙트 갯수만큼 모든리스트에 추가
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

            //풀매니저 밑에 정렬시키기
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
    /// 글로벌에 있는 프리팹 Dictionary 에서 프리팹을 들고옴
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

using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;


[CustomEditor(typeof(PoolManager))]
public class EDMGPool : Editor
{
    private PoolManager myMGPool;
    private int prevPoolCharCount, prevPoolEffectCount;//, _prevPoolEtcCnt, _prevPoolProjectileCnt, _prevPoolMinionCnt;
    private List<GameObject> delObjList;
    private List<int> delCountList;

    void Awake()
    {
        delObjList = new List<GameObject>();
        delCountList = new List<int>();
    }

    void OnEnable()
    {
        myMGPool = (PoolManager)target;

        prevPoolCharCount = myMGPool.poolCharCount;
        prevPoolEffectCount = myMGPool.poolEffectCount;

        // _prevPoolEtcCnt = myMGPool._poolEtcCnt;
        // _prevPoolProjectileCnt = myMGPool._poolProjectileCnt;
        // _prevPoolMinionCnt = myMGPool._poolMinionCnt;

        EditorUtility.SetDirty((PoolManager)target);
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        if (myMGPool.poolCharList == null)
            myMGPool.poolCharList = new List<GameObject>();

        if (myMGPool.poolCharCountList == null)
            myMGPool.poolCharCountList = new List<int>();


        if (myMGPool.poolEffectList == null)
            myMGPool.poolEffectList = new List<GameObject>();

        if (myMGPool.poolEffectCountList == null)
            myMGPool.poolEffectCountList = new List<int>();


        // if (myMGPool._poolEtcList == null)
        //     myMGPool._poolEtcList = new List<GameObject>();

        // if (myMGPool._poolEtcCntList == null)
        //     myMGPool._poolEtcCntList = new List<int>();


        // if (myMGPool._poolProjectileList == null)
        //     myMGPool._poolProjectileList = new List<GameObject>();

        // if (myMGPool._poolProjectileCntList == null)
        //     myMGPool._poolProjectileCntList = new List<int>();

        // if (myMGPool._poolMinionList == null)
        //     myMGPool._poolMinionList = new List<GameObject>();

        // if (myMGPool._poolMinionCntList == null)
        //     myMGPool._poolMinionCntList = new List<int>();

        GenerateList();
    }

    public void GenerateList()
    {
        ///////////////////////////////////////////////////////
        GUILayout.Space(15);
        GUILayout.Label(" Char Pool");
        CreateInspectorUI(ref prevPoolCharCount, ref myMGPool.poolCharCount, myMGPool.poolCharList, myMGPool.poolCharCountList);

        ///////////////////////////////////////////////////////
        GUILayout.Space(15);
        GUILayout.Label(" Effect Pool");
        CreateInspectorUI(ref prevPoolEffectCount, ref myMGPool.poolEffectCount, myMGPool.poolEffectList, myMGPool.poolEffectCountList);

        // ///////////////////////////////////////////////////////
        // GUILayout.Space(15);
        // GUILayout.Label(" Minion Pool");
        // CreateInspectorUI(ref _prevPoolMinionCnt, ref myMGPool._poolMinionCnt, myMGPool._poolMinionList, myMGPool._poolMinionCntList);

        // ///////////////////////////////////////////////////////
        // GUILayout.Space(15);
        // GUILayout.Label(" ETC Pool");
        // CreateInspectorUI(ref _prevPoolEtcCnt, ref myMGPool._poolEtcCnt, myMGPool._poolEtcList, myMGPool._poolEtcCntList);

        // ///////////////////////////////////////////////////////
        // GUILayout.Space(15);
        // GUILayout.Label(" Projectile Pool");
        // CreateInspectorUI(ref _prevPoolProjectileCnt, ref myMGPool._poolProjectileCnt, myMGPool._poolProjectileList, myMGPool._poolProjectileCntList);

        ///////////////////////////////////////////////////////
        GUILayout.Space(15);
        GUILayout.Label(" Save PoolManager");
        if (GUILayout.Button("Save All", GUILayout.Width(100), GUILayout.Height(50)))
        {
            EditorUtility.SetDirty((PoolManager)target);
        }
    }

    private void CreateInspectorUI(ref int inPrev , ref int inNow , List<GameObject> inObjList , List<int> inCountList)
    {
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Add Pool Object", GUILayout.Width(150)))
            inNow++;
        else if (GUILayout.Button("Remove Pool Object", GUILayout.Width(150)) && inNow > 0)
            inNow--;

        EditorGUILayout.EndHorizontal();

        if (inPrev != inNow)
        {
            int gap = 0;

            if (inPrev > inNow)     // 기존에 있던 오브젝트가 삭제됐으므로 제거해줌
            {
                gap = inPrev - inNow;

                for (int i = 0; i < gap; i++)
                {
                    inObjList.Remove(inObjList[inObjList.Count - 1]);
                    inCountList.Remove(inCountList[inCountList.Count - 1]);
                }
            }
            else if (inPrev < inNow)    // 풀에 새로운 오브젝트가 추가됐으므로 초기화해서 추가
            {
                gap = inNow - inPrev;

                for (int i = 0; i < gap; i++)
                {
                    inObjList.Add(null);
                    inCountList.Add(0);
                }
            }

            // 바뀐 현재값을 다시 이전값에 저장
            inPrev = inNow;
        }

        // 개별 오브젝트를 인스펙터에 보여줌
        for (int i = 0; i < inNow; i++)
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Prefab", GUILayout.Width(50));
            inObjList[i] = EditorGUILayout.ObjectField(inObjList[i], typeof(GameObject), true) as GameObject;
            inCountList[i] = EditorGUILayout.IntField(inCountList[i], GUILayout.Width(30));
            
            if (GUILayout.Button("Remove", GUILayout.Width(60)) && inNow > 0)
            {
                delObjList.Add(inObjList[i]);
                delCountList.Add(inCountList[i]);
            }

            EditorGUILayout.EndHorizontal();
        }

        // 만약 삭제를 눌렀다면 제거해줌
        for (int i = 0; i < delObjList.Count; i++)
        {
            inObjList.Remove(delObjList[i]);
            inCountList.Remove(delCountList[i]);
            inNow--;
        }
        // 클리어까지
        if (delObjList.Count > 0)
        {
            delObjList.Clear();
            delCountList.Clear();
            inPrev = inNow;
        }
    }
}
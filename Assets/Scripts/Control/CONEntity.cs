using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 풀메니저를 사용할 오브젝트에 넣어줘야하는 스크립트
/// </summary>
public class CONEntity : MonoBehaviour
{
    public ePrefabs myKind;

    protected GameObject myObj;

    public Transform myTrm;

    public Vector3 myVelocity;

    protected bool bFirstUpdate;

    public virtual void Awake()
    {
        myTrm = this.transform;
        myObj = this.gameObject;
    }

    public virtual void OnEnable()
    {

    }

    public virtual void OnDisable()
    {

    }

    public virtual void Start()
    {

    }

    public void SetActive(bool bValue)
    {
        // 초기화 함수 호출
        myObj.SetActive(bValue);

        if(!bValue)
        {
            myTrm.position = Vector3.zero;
            //myTrm.SetParent(GameSceneClass.gMGPool.myTrm, false);

            if(GameSceneClass._gameManager == null)
                return;
        }
    }

    public void SetPosition(Vector3 inVec)
    {
        myTrm.position = inVec;
    }

    public bool IsActive()
    {
        return myObj.activeInHierarchy;
    }
}

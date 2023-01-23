using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameWork : MonoBehaviour
{
    //GameManager에서 돌릴 함수들을 모아주는 역할
    RoomManager roomManager;
    public void FirstAwkae()
    {
        roomManager = GetComponent<RoomManager>();
    }
    
    public void OnAwake()
    {
        roomManager.OnAwake();
    }

    public void OnStart()
    {
        roomManager.OnStart();
    }

    public void OnUpdate()
    {
        roomManager.OnUpdate();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameWork : MonoBehaviour
{
    //GameManager���� ���� �Լ����� ����ִ� ����
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] List<GameObject> Walls;

    bool[] isPos;
    public bool isEnemy;

    void Awake()
    {
        isPos = new bool[4];
        boolInit();
    }

    public void OnUpdate()
    {

    }

    void boolInit()
    {
        for(int i = 0; i < 4; i++)
        {
            isPos[i] = false;
        }
    }

    public void ActiveDoor(int num)
    {
        isPos[num] = true;
    }

    /// <summary>
    /// ��� ���� Ȱ��ȭ ��Ű�� �Լ�
    /// </summary>
    /// <param name="isTrue"></param>
    void UnActiveAllDoors()
    {
        for(int i = 0; i < 4; i++)
        {
            if (!isPos[i]) continue;
            Walls[i].SetActive(true);
        }
    }

    /// <summary>
    /// ���͸� ��� ������� �ǵ����ִ� �Լ�
    /// </summary>
    void ReturnRoom()
    {
        for (int i = 0; i < 4; i++)
        {
            if (!isPos[i]) continue;
            Walls[i].SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isEnemy)
                UnActiveAllDoors();
            else
                ReturnRoom();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            if (!isEnemy) ReturnRoom();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ReturnRoom();
    }
}

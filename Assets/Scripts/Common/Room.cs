using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] List<GameObject> Walls;
    [SerializeField] List<GameObject> DoorWalls;
    [SerializeField] List<GameObject> Doors;

    bool[] isPos;
    public bool isEnemy;

    void Awake()
    {
        isPos = new bool[4];
        boolInit();
    }

    public void OnUpdate()
    {
        //�ʿ� ���Ͱ� ����, �÷��̾ �ȿ� �ִٸ� ��� �� ���ֱ�
        //�÷��̾ �ȿ� ������, �� �ȴ���
    }

    void boolInit()
    {
        for(int i = 0; i < 4; i++)
        {
            isPos[i] = false;
        }
    }

    public void ActiveWalls()
    {
        foreach(var wall in Walls)
        {
            wall.SetActive(true);
        }
    }

    public void ActiveDoor(int num)
    {
        Walls[num].SetActive(false);
        DoorWalls[num].SetActive(true);
        Doors[num].SetActive(true);

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
            Walls[i].SetActive(true);
            DoorWalls[i].SetActive(false);
            Doors[i].SetActive(false);
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
            DoorWalls[i].SetActive(true);
            Doors[i].SetActive(true);
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

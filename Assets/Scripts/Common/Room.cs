using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] List<GameObject> Walls;
    [SerializeField] List<GameObject> DoorWalls;
    [SerializeField] List<GameObject> Doors;

    public void OnStart()
    {

    }

    public void OnUpdate()
    {
        //�ʿ� ���Ͱ� ����, �÷��̾ �ȿ� �ִٸ� ��� �� ���ֱ�
        //�÷��̾ �ȿ� ������, �� �ȴ���
    }

    public void ActiveWalls()
    {
        foreach(var wall in Walls)
        {
            wall.SetActive(true);
        }
    }

    public void ActiveDoor(Pos pos)
    {
        Debug.Log("�����");
        int num = (int)pos;

        Walls[num].SetActive(false);
        DoorWalls[num].SetActive(true);
        Doors[num].SetActive(true);
    }
}

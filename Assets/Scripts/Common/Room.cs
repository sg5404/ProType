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
        //맵에 몬스터가 없고, 플레이어가 안에 있다면 모든 문 없애기
        //플레이어가 안에 없으면, 문 안닫음
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
        Debug.Log("실행됨");
        int num = (int)pos;

        Walls[num].SetActive(false);
        DoorWalls[num].SetActive(true);
        Doors[num].SetActive(true);
    }
}

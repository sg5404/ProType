using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class RoomManager : MonoBehaviour
{
    //private Dictionary<Vector3, Transform> RoomDic;
    //private List<Transform> roomList;

    private int RoomCount = 9;

    private StringBuilder _sb;

    private bool[,] isRoom;

    private int roomX;
    private int roomY;

    public void OnAwake()
    {
        _sb = new StringBuilder();
        //RoomDic = new Dictionary<Vector3, Transform>();
        isRoom = new bool[17, 17];
        roomX = roomY = (RoomCount * 2 - 1) / 2;
    }

    public void OnStart()
    {
        RoomSpawner();

    }

    public void OnUpdate()
    {

    }

    /// <summary>
    /// Room들을 스폰해주는 함수
    /// </summary>
    void RoomSpawner()
    {
        isRoom[roomX, roomY] = true;
        for (int i = 0; i < RoomCount - 1; i++)
        {
            InputRoom();
        }

        for (int y = 0; y < RoomCount * 2 - 1; y++)
        {
            for (int x = 0; x < RoomCount * 2 - 1; x++)
            {
                if (isRoom[y, x])
                {
                    SpawnRoom(x, y);
                }

                if (x + 1 < RoomCount * 2 - 1 && x > 0)
                {
                    if (isRoom[y, x - 1] && isRoom[y, x + 1])
                    {
                        SpawnPass(x, y, ePass.Horizontal);
                    }
                }

                if (y + 1 < RoomCount * 2 - 1 && y > 0)
                {
                    if (isRoom[y + 1, x] && isRoom[y - 1, x])
                    {
                        SpawnPass(x, y, ePass.Vertical);
                    }
                }
            }
        }
    }

    void SpawnPass(int x, int y, ePass _passShape)
    {
        CONEntity Pass = PassShape(x/2, y/2, _passShape);
        Pass.SetActive(true);
        Pass.SetPosition(new Vector3(x - (RoomCount * 2 - 1) / 2, y - (RoomCount * 2 - 1) / 2, 1) * 100);
    }

    CONEntity PassShape(int x, int y, ePass _passShape)
    {
        //이름들고오기
        _sb.Remove(0, _sb.Length);
        _sb.Append(System.Enum.GetName(typeof(ePass), (int)_passShape));
        _sb.Append("Pass");

        //다른형식으로 만들기
        ePrefabs rPass = (ePrefabs)System.Enum.Parse(typeof(ePrefabs), _sb.ToString());
        //들고오기
        PoolManager.Instance.poolListDic.TryGetValue(rPass, out List<CONEntity> PassS);

        foreach (CONEntity pass in PassS)
        {
            if (!pass.IsActive())
            {
                return pass;
            }
        }
        return null;
    }

    void SpawnRoom(int x, int y)
    {
        CONEntity Room = RandomRoon();
        Room.SetActive(true);
        Room.SetPosition(new Vector3(x - (RoomCount * 2 - 1) / 2, y - (RoomCount * 2 - 1) / 2, 1) * 100);

        //여기서 모양을 정해줄까?
        RoomShape(x, y, Room.gameObject);
    }

    /// <summary>
    /// 배열에 룸을 입력
    /// </summary>
    void InputRoom()
    {
        if(Random.Range(0, 2) == 1)
            roomX += Random.Range(-1, 2) * 2;
        else
            roomY += Random.Range(-1, 2) * 2;


        RangeCheck();

        if (isRoom[roomX, roomY])
            InputRoom();
        else
            isRoom[roomX, roomY] = true;

    }

    /// <summary>
    /// 지정된 범위를 넘었는지 알아내는 함수
    /// </summary>
    void RangeCheck()
    {
        if (roomX >= RoomCount * 2 - 1)
            roomX -= 2;

        if (roomX < 0)
            roomX += 2;

        if (roomY >= RoomCount * 2 - 1)
            roomY -= 2;

        if (roomY < 0)
            roomY += 2;
    }

    /// <summary>
    /// 만들 Room을 가져와주는 함수
    /// </summary>
    /// <returns></returns>
    CONEntity RandomRoon()
    {
        while(true)
        {
            int rNum = Random.Range(0, System.Enum.GetValues(typeof(eRoom)).Length);

            //이름들고오기
            _sb.Remove(0, _sb.Length);
            _sb.Append(System.Enum.GetName(typeof(eRoom), rNum));
            _sb.Append("Room");

            //다른형식으로 만들기
            ePrefabs rRoom = (ePrefabs)System.Enum.Parse(typeof(ePrefabs), _sb.ToString());

            //PoolManager에서 들고오기
            PoolManager.Instance.poolListDic.TryGetValue(rRoom, out List<CONEntity> RoomS);

            //있으면 리턴
            foreach (CONEntity room in RoomS)
            {
                if (!room.IsActive())
                {
                    return room;
                }
            }
        }
    }

    /// <summary>
    /// 방 모양을 정해주는 함수
    /// </summary>
    void RoomShape(int x, int y, GameObject obj)
    {
        Room room = obj.GetComponent<Room>();

        if(room == null)
        {
            Debug.LogWarning("room 비었음");
        }

        room.ActiveWalls();



        if (x - 2 >= 0)
            if (isRoom[y, x - 2]) room.ActiveDoor((int)Pos.left);

        if (x + 2 < RoomCount * 2)
            if (isRoom[y, x + 2]) room.ActiveDoor((int)Pos.right);

        if (y - 2 >= 0)
            if (isRoom[y - 2, x]) room.ActiveDoor((int)Pos.bottom);

        if (y + 2 < RoomCount * 2)
            if (isRoom[y + 2, x]) room.ActiveDoor((int)Pos.top);

    }
}

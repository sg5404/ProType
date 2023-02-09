using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Ar
{
    [SerializeField] PlayerSO playerSO;

    private bool isMoved = false;

    public UnityEvent MouseUp;
    public UnityEvent AfterMove;

    protected override void Start()
    {
        base.Start();
        MouseUp.AddListener(() => { isMoved = true; });
        AfterMove.AddListener(() => { isMoved = false; });
        ClassSet();
    }

    private void FixedUpdate()
    {
        ToFixedUpdate();
    }

    public void ToFixedUpdate()
    {
        lastVelocity = rigid.velocity;
        MoveFinish();
    }

    public void Dash(Vector2 drag)
    {
        if (isMoved) AfterMove.Invoke();
        rigid.velocity = (-drag * pushPower);
        MouseUp?.Invoke(); // 발사 직후 발동하는 트리거
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.transform.CompareTag("Enemy"))
        {
            BeforeCrash?.Invoke(); //충돌 직전 발동하는 트리거
            BattleManager.Instance.PlayerCrashSet(collision.contacts[0].normal);
        }
    }

    private void MoveFinish()
    {
        if (lastVelocity.magnitude <= 0.5f && isMoved)
            AfterMove.Invoke();
    }

    public override void AttackFinish()
    {
        base.AttackFinish();
        HPManager.Instance.ChangeHP();
    }

    private void ClassSet()
    {
        switch (playerSO.weapon)
        {
            case Weapon.None:
                break;
            case Weapon.Sword:
                AfterMove.AddListener(SwordSpin);
                break;
            case Weapon.Bow:
                AfterMove.AddListener(ShootArrow);
                break;
        }
        MaxHP = playerSO.HP;
        HP = MaxHP;
        ATK = playerSO.ATK;
        pushPower = playerSO.pushPower;
    }

    public void SwordSpin()
    {
        Debug.Log("원형 공격");
        Ar _area = Instantiate(playerSO.bullet, null);
        _area.transform.position = transform.position;
        _area.transform.localScale = new Vector2(3, 3);
        Destroy(_area.gameObject, 1f);
    }

    public void ShootArrow()
    {
        Ar[] ars = FindObjectsOfType<Ar>();
        Ar shortAr = null;
        float shortDis = 0;
        foreach (Ar trs in ars)
        {
            float a = Vector2.Distance(transform.position, trs.transform.position);
            if (shortDis == 0 || a < shortDis)
            {
                if (a == 0) continue;
                shortAr = trs;
                shortDis = a;
            }
        }
        Ar _arrow = Instantiate(playerSO.bullet, null);
        _arrow.transform.position = transform.position;
        _arrow.SetRigidPower((shortAr.transform.position - transform.position).normalized * 6);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Out"))
        {
            Debug.Log("나감");

            float temp = 0;
            float _distance = 100000;
            Room closeRoom = new Room();             //가장 가까운 룸

            //가까운거 찾아서 돌아오는거
            Room[] rooms = FindObjectsOfType<Room>();

            //가장 가까운 룸을 찾기 위한 과정
            foreach (Room room in rooms)
            {
                temp = Vector3.Distance(transform.position, room.transform.position);
                if (temp < _distance)
                {
                    _distance = temp;
                    closeRoom = room;
                }
            }

            //플레이어 피 줄이는거 만들어 주셈


            //플레이어가 낙사한 후 다시 돌아오게하기
            transform.position = closeRoom.gameObject.transform.position;
        }
    }
}

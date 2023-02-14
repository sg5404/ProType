using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Ar
{
    [SerializeField] PlayerSO playerSO;
    [SerializeField] Transform resetPosition;

    private bool isMoved = false;
    public bool isInvinsible = false;
    private SpriteRenderer sprite;

    public UnityEvent MouseUp;
    public UnityEvent AfterMove;

    protected override void Start()
    {
        base.Start();
        sprite = GetComponent<SpriteRenderer>();
        MouseUp.AddListener(() => { 
            isMoved = true;
            isCharge = true;
            StartCoroutine(DashInvinsible());
        });
        AfterMove.AddListener(() => { isMoved = false; });
        OnHit.AddListener(() => { 
            StartCoroutine(InvinsibleTime());
            HPManager.Instance.ChangeHP();
        });
        ClassSet();
        OnOutDie.AddListener(() => {
            HP -= MaxHP / 10;
            HPManager.Instance.ChangeHP();
            transform.position = resetPosition.position;
        });
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
        rigid.velocity = (-drag * pushPower)/70;
        MouseUp?.Invoke(); // �߻� ���� �ߵ��ϴ� Ʈ����
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.transform.CompareTag("Enemy"))
        {
            BeforeCrash?.Invoke(); //�浹 ���� �ߵ��ϴ� Ʈ����
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
        Debug.Log("���� ����");
        Ar _area = Instantiate(playerSO.bullet, null);
        _area.transform.position = transform.position;
        _area.transform.localScale = new Vector2(3, 3);
        Destroy(_area.gameObject, 1f);
    }

    public void ShootArrow()
    {
        Enemy[] ars = FindObjectsOfType<Enemy>();
        Enemy shortAr = null;
        float shortDis = 0;
        foreach (Enemy trs in ars)
        {
            float a = Vector2.Distance(transform.position, trs.transform.position);
            if (shortDis == 0 || a < shortDis)
            {
                if (a == 0) continue;
                shortAr = trs;
                shortDis = a;
            }
        }
        var _arrow = Instantiate(playerSO.bullet, null);
        _arrow.transform.position = transform.position;
        _arrow.SetRigidPower((shortAr.transform.position - transform.position).normalized * 6);
    }

    private IEnumerator InvinsibleTime()
    {
        isInvinsible = true;

        sprite.color = Color.black;
        yield return new WaitForSeconds(0.3f);
        sprite.color = Color.green;

        isInvinsible = false;
    }

    private IEnumerator DashInvinsible()
    {
        isCharge = true;

        yield return new WaitForSeconds(0.25f);

        isCharge = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Out"))
        {
            Debug.Log("����");

            float temp = 0;
            float _distance = 100000;
            Room closeRoom = new Room();             //���� ����� ��

            //������ ã�Ƽ� ���ƿ��°�
            Room[] rooms = FindObjectsOfType<Room>();

            //���� ����� ���� ã�� ���� ����
            foreach (Room room in rooms)
            {
                temp = Vector3.Distance(transform.position, room.transform.position);
                if (temp < _distance)
                {
                    _distance = temp;
                    closeRoom = room;
                }
            }

            OnOutDie.Invoke();

            transform.position = closeRoom.gameObject.transform.position;
        }
        else if(collision.CompareTag("Bullet"))
        {
            var bullet = collision.GetComponent<Bullet>();
            if (!bullet.bulletSO.isEnemyBullet) return;

            HP -= bullet.bulletSO.bulletDamage;
            OnHit?.Invoke();
            if (!bullet.bulletSO.isPenetrate)
                bullet.gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ar : MonoBehaviour
{
    [SerializeField] ArSO arSO;
    public float MaxHP { get; set; }
    public float HP { get; set; }
    public float ATK { get; set; }

    protected float pushPower = 10;

    protected Vector2 defaultScale = new Vector2(0.5f, 0.5f);
    protected GameObject line;
    protected Transform hpBar;
    protected SpriteRenderer hpImage;

    public Rigidbody2D rigid { get; protected set; }
    public Vector2 lastVelocity { get; protected set; }

    public bool isFirstAttack { get; protected set; }
    public bool isDoubleAttack { get; protected set; }

    protected bool isMoved = false;

    public UnityEvent MouseUp;
    public UnityEvent BeforeCrash;
    public UnityEvent AfterCrash;
    public UnityEvent BeforeBattle;
    public UnityEvent AfterBattle;
    public UnityEvent BeforeAttack;
    public UnityEvent AfterAttack;
    public UnityEvent BeforeDefence;
    public UnityEvent AfterDefence;
    public UnityEvent AfterMove;
    public UnityEvent OnOutDie;
    public UnityEvent OnBattleDie;

    protected virtual void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        MaxHP = 100; //
        HP = MaxHP; //
        ATK = 0; // 나중에 변경
        MouseUp.AddListener(() => { isMoved = true; });
        ClassSet();
    }

    protected void StatReset() // 수치 초기화
    {
        HP = MaxHP;
        hpBar.localScale = new Vector3(Mathf.Clamp(HP / MaxHP, 0, 1), 1, 1);
    }

    protected void ClassSet()
    {
        switch(arSO.weapon)
        {
            case Weapon.Sword:
                AfterMove.AddListener(SwordSpin);
                break;
            case Weapon.Bow:
                AfterMove.AddListener(ShootArrow);
                break;
        }    
    }

    public void Dash(Vector2 drag)
    {
        rigid.velocity = (-drag * pushPower);
        MouseUp?.Invoke(); // 발사 직후 발동하는 트리거
    }

    protected void FixedUpdate()
    {
        lastVelocity = rigid.velocity;
        MoveFinish();
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ar"))
        {
            BeforeCrash?.Invoke(); //충돌 직전 발동하는 트리거
            BattleManager.Instance.CrashSet(this, collision.contacts[0].normal);
        }
        else if (collision.transform.CompareTag("Out"))
        {
            OnOutDie.Invoke();
        }
        else if (collision.transform.CompareTag("Object"))
        {
            rigid.velocity = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal) * pushPower / 2;
            AfterCrash?.Invoke();
        }
    }

    public void AttackFinish()
    {
        Debug.Log(name + HP);
        if (!DeadCheck())
            AfterCrash?.Invoke(); //충돌 직후 발동하는 트리거
    }

    public void Hit(Vector2 velo)
    {
        rigid.velocity = velo;
    }

    protected bool DeadCheck()
    {
        if (HP <= 0)
        {
            OnBattleDie.Invoke();
            if (HP <= 0)
            {
                return true;
            }
        }
        //hpBar.localScale = new Vector3(Mathf.Clamp(HP / MaxHP, 0, 1), 1, 1);
        return false;
    }

    protected void MoveFinish()
    {
        if(lastVelocity.magnitude<=0.5f && isMoved)
        {
            isMoved = false;
            AfterMove?.Invoke();
        }
    }

    public void SwordSpin()
    {
        Debug.Log("원형 공격");
        Ar _area = Instantiate(arSO.bullet, null);
        _area.transform.position = transform.position;
        _area.transform.localScale = new Vector2(3, 3);
        Destroy(_area.gameObject, 1f);
    }

    public void ShootArrow()
    {
        Ar[] ars = FindObjectsOfType<Ar>();
        Ar shortAr = null;
        float shortDis = 0;
        foreach(Ar trs in ars)
        {
            float a = Vector2.Distance(transform.position, trs.transform.position);
            if(shortDis == 0 || a<shortDis)
            {
                if (a == 0) continue;
                shortAr = trs;
                shortDis = a;
            }
        }
        Ar _arrow = Instantiate(arSO.bullet, null);
        _arrow.transform.position = transform.position;
        _arrow.SetRigidPower((shortAr.transform.position - transform.position).normalized * 6);
    }

    public void SetRigidPower(Vector2 power)
    {
        rigid = GetComponent<Rigidbody2D>();

        rigid.velocity = power;
    }
}

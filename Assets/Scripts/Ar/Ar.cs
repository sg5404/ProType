using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ar : MonoBehaviour
{
    public float MaxHP { get; set; }
    public float HP { get; set; }
    public float ATK { get; set; }

    protected float pushPower = 5;

    protected Vector2 defaultScale = new Vector2(0.5f, 0.5f);
    protected GameObject line;
    protected Transform hpBar;
    protected SpriteRenderer hpImage;

    public Rigidbody2D rigid { get; protected set; }
    public Vector2 lastVelocity { get; protected set; }

    public bool isFirstAttack { get; protected set; }
    public bool isDoubleAttack { get; protected set; }

    public UnityEvent MouseUp;
    public UnityEvent BeforeCrash;
    public UnityEvent AfterCrash;
    public UnityEvent BeforeBattle;
    public UnityEvent AfterBattle;
    public UnityEvent BeforeAttack;
    public UnityEvent AfterAttack;
    public UnityEvent BeforeDefence;
    public UnityEvent AfterDefence;
    public UnityEvent OnOutDie;
    public UnityEvent OnBattleDie;

    protected virtual void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        line = transform.GetChild(0).gameObject;
        hpBar = transform.GetChild(1).GetChild(0);
        hpImage = hpBar.GetComponentInChildren<SpriteRenderer>();
        MaxHP = 100; //
        HP = MaxHP; //
        ATK = 35; // 나중에 변경
    }

    protected void StatReset() // 수치 초기화
    {
        HP = MaxHP;
        hpBar.localScale = new Vector3(Mathf.Clamp(HP / MaxHP, 0, 1), 1, 1);
    }

    public void Dash(Vector2 drag)
    {
        rigid.velocity = (-drag * pushPower);
        MouseUp?.Invoke(); // 발사 직후 발동하는 트리거
    }

    protected void FixedUpdate()
    {
        lastVelocity = rigid.velocity;
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
            rigid.velocity = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal) * pushPower;
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
        hpBar.localScale = new Vector3(Mathf.Clamp(HP / MaxHP, 0, 1), 1, 1);
        return false;
    }
}

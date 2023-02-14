using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum BossMoveState
{
    IDLE,
    MOVE,
    ATTACK,
    RIDER_KICK
}

public enum Phase
{
    ONE,
    TWO,
    THREE,
    FOUR,
    LAST = 99
}

public class Boss : Enemy
{
    protected BossMoveState currentState;
    protected Phase currentPhase;
    protected Animator animator;
    protected Player player;

    public UnityEvent attackEvent;

    protected override void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        AfterBattle.AddListener(HPChange);
        OnHit.AddListener(HPChange);
        OnHit.AddListener(() => { DeadCheck(); });
        OnBattleDie.AddListener(()=> { gameObject.SetActive(false); });
        OnOutDie.AddListener(() => { gameObject.SetActive(false); });
    }

    protected virtual void Idle()
    {
        animator.SetBool("Idle", true);
        animator.SetBool("Move", false);
        animator.SetBool("Attack", false);
    }

    protected virtual void Move()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Move", true);
        animator.SetBool("Attack", false);
    }

    protected virtual void Attack()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Move", false);
        animator.SetBool("Attack", true);


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Out"))
        {
            //�׳� ���̸� ��
            OnOutDie.Invoke();
            //Destroy ���� SetActive �� ���ְԸ� ���ָ� ������
        }
        else if (collision.CompareTag("Bullet"))
        {
            var bullet = collision.GetComponent<Bullet>();
            if (bullet.bulletSO.isEnemyBullet) return;

            HP -= bullet.bulletSO.bulletDamage;
            OnHit?.Invoke();
            if (!bullet.bulletSO.isPenetrate)
                bullet.gameObject.SetActive(false);
        }
    }

    //------------------------------------------------------------------

    public virtual IEnumerator Pattern1()
    {
        Debug.Log("���� 1 ����");
        yield return null;
    }
    public virtual IEnumerator Pattern2()
    {
        Debug.Log("���� 2 ����");
        yield return null;
    }
    public virtual IEnumerator Pattern3()
    {
        Debug.Log("���� 3 ����");
        yield return null;
    }
    public virtual IEnumerator Pattern4()
    {
        Debug.Log("���� 4 ����");
        yield return null;
    }
    public virtual IEnumerator Pattern5()
    {
        Debug.Log("���� 5 ����");
        yield return null;
    }
}

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

public class Boss : Ar
{
    protected BossMoveState currentState;
    protected Phase currentPhase;
    protected Animator animator;

    public UnityEvent attackEvent;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
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

        int pattern = Random.Range(1, 6);
        Pattern(pattern);

    }

    protected virtual void Pattern(int num)
    {
        UnityAction a = num switch
        {
            1=>Pattern1,
            2=>Pattern2,
            3=>Pattern3,
            4=>Pattern4,
            5=>Pattern5,
            _ => () => { Debug.LogWarning("Pattern 함수에서 발생한 오류 ^v^"); }
        };
        a.Invoke();
    }

//------------------------------------------------------------------

    protected virtual void Pattern1()
    {
        Debug.Log("패턴 1 실행");
    }
    protected virtual void Pattern2()
    {
        Debug.Log("패턴 2 실행");
    }
    protected virtual void Pattern3()
    {
        Debug.Log("패턴 3 실행");
    }
    protected virtual void Pattern4()
    {
        Debug.Log("패턴 4 실행");
    }
    protected virtual void Pattern5()
    {
        Debug.Log("패턴 5 실행");
    }
}

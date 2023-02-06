using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

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
    }

//------------------------------------------------------------------

    protected virtual void Pattern1()
    {

    }
    protected virtual void Pattern2()
    {

    }
    protected virtual void Pattern3()
    {

    }
    protected virtual void Pattern4()
    {

    }
    protected virtual void Pattern5()
    {

    }
}

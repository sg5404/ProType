using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Ar
{
    [SerializeField] EnemySO enemySO;

    protected override void Start()
    {
        base.Start();
        ClassSet();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.transform.CompareTag("Player") || collision.transform.CompareTag("Enemy"))
        {
            BeforeCrash?.Invoke(); //충돌 직전 발동하는 트리거
            BattleManager.Instance.EnemyCrashSet(this, collision.contacts[0].normal);
        }
    }

    private void ClassSet()
    {
        MaxHP = enemySO.HP;
        HP = MaxHP;
        ATK = enemySO.ATK;
    }
}

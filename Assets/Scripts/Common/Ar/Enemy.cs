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

    private void ClassSet()
    {
        MaxHP = enemySO.HP;
        HP = MaxHP;
        ATK = enemySO.ATK;
    }
}

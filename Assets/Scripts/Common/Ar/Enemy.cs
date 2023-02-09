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
        OnOutDie.AddListener(() => { gameObject.SetActive(false); });
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.transform.CompareTag("Player") || collision.transform.CompareTag("Enemy"))
        {
            BeforeCrash?.Invoke(); //�浹 ���� �ߵ��ϴ� Ʈ����
            BattleManager.Instance.EnemyCrashSet(this, collision.contacts[0].normal);
        }
    }

    private void ClassSet()
    {
        MaxHP = enemySO.HP;
        HP = MaxHP;
        ATK = enemySO.ATK;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.CompareTag("Out"))
       {
            //�׳� ���̸� ��
            OnOutDie.Invoke();
            //Destroy ���� SetActive �� ���ְԸ� ���ָ� ������
       }
    }
}

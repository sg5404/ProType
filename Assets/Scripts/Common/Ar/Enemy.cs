using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Ar
{
    [SerializeField] EnemySO enemySO;
    [SerializeField] protected GameObject hpBar;

    protected override void Start()
    {
        base.Start();
        ClassSet();
        OnHit.AddListener(HPChange);
        OnHit.AddListener(() => { DeadCheck(); });
        OnOutDie.AddListener(() => { gameObject.SetActive(false); });
        OnBattleDie.AddListener(() => { gameObject.SetActive(false); });
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

    private void MoveFinish()
    {
        if (lastVelocity.magnitude <= 0.5f && isCharge)
            isCharge = false;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.transform.CompareTag("Player") || collision.transform.CompareTag("Enemy"))
        {
            BeforeCrash?.Invoke(); //충돌 직전 발동하는 트리거
            BattleManager.Instance.EnemyCrashSet(this, collision.contacts[0].normal, isCharge);
        }
    }

    protected virtual void HPChange()
    {
        if (HP >= 0)
            hpBar.transform.localScale = new Vector2(HP / MaxHP, 1);
        Debug.Log(HP / MaxHP);
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
            //그냥 죽이면 됨
            OnOutDie.Invoke();
            //Destroy 말고 SetActive 로 꺼주게만 해주면 좋을듯
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
}

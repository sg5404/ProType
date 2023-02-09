using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoSingleton<BattleManager>
{
    private Player player = null;
    private Enemy a = null, b = null;
    private Vector2 aNomal, bNomal, pNomal;
    public Vector3 mousePosition { get { return Camera.main.ScreenToWorldPoint(Input.mousePosition); } }

    public void PlayerCrashSet(Vector2 nomal)
    {
        if (player == null) player = FindObjectOfType<Player>();
        pNomal = nomal;
        if (a != null) PlayerCrashResult(); 
    }

    public void EnemyCrashSet(Enemy bullet, Vector2 nomal)
    {
        if (a == null)
        {
            a = bullet;
            aNomal = nomal;

            if(player!=null) PlayerCrashResult();
        }
        else if (b == null)
        {
            b = bullet;
            bNomal = nomal;

            EnemyCrashResult();
        }
    }

    private void PlayerCrashResult()
    {
        player.BeforeBattle?.Invoke(); //공격 직전 발동하는 트리거
        a.BeforeBattle?.Invoke(); //
        Debug.Log("플레이어 충돌");

        player.BeforeDefence?.Invoke();
        a.BeforeAttack?.Invoke();

        if (DamageCalculate())
        {
            var reflect = Vector2.Reflect(player.lastVelocity.normalized, pNomal);
            player.rigid.velocity = (reflect);
            a.rigid.velocity = (-reflect + a.lastVelocity / 2);
            Debug.Log("데미지 계산");
        }

        player.AfterAttack?.Invoke();
        a.AfterDefence?.Invoke();

        player.AfterBattle?.Invoke(); // 공격 직후 발동하는 트리거
        a.AfterBattle?.Invoke(); //

        player.AttackFinish();
        a.AttackFinish();
        a = null;
    }
    private void EnemyCrashResult()
    {
        a.BeforeBattle?.Invoke(); //
        b.BeforeBattle?.Invoke(); //공격 직전 발동하는 트리거
        Debug.Log("적 끼리 충돌");
        if (a.lastVelocity.magnitude > b.lastVelocity.magnitude)
        {
            a.BeforeAttack?.Invoke();
            b.BeforeDefence?.Invoke();

            if (EnemyDamageCalculate())
            {
                var reflect = Vector2.Reflect(a.lastVelocity.normalized, aNomal);
                a.rigid.velocity = (reflect);
                b.rigid.velocity = (-reflect + a.lastVelocity);
                Debug.Log("데미지 계산");
            }

            a.AfterAttack?.Invoke();
            b.AfterDefence?.Invoke();
        }
        else if (a.lastVelocity.magnitude < b.lastVelocity.magnitude)
        {
            b.BeforeAttack?.Invoke();
            a.BeforeDefence?.Invoke();

            if (DamageCalculate())
            {
                var reflect = Vector2.Reflect(b.lastVelocity.normalized, bNomal);
                b.rigid.velocity = (reflect);
                a.rigid.velocity = (-reflect + b.lastVelocity);
                Debug.Log("데미지 계산");
            }

            b.AfterAttack?.Invoke();
            a.AfterDefence?.Invoke();
        }
        a.AfterBattle?.Invoke(); //
        b.AfterBattle?.Invoke(); // 공격 직후 발동하는 트리거

        a.AttackFinish();
        b.AttackFinish();
        a = null;
        b = null;
    }

    private bool DamageCalculate()
    {
        float FinalDamage = player.ATK; // 여기에 패시브스킬 등으로 변하는 데미지 계산
        if (player.isDoubleAttack)
        {
            a.HP -= FinalDamage; // 
            if (a.HP <= 0)
            {
                return false;
            }
            a.HP -= FinalDamage; // 여기에 속도비례 보정 들어가야함
        }
        else
        {
            if (a.HP <= 0)
            {
                return false;
            }
            a.HP -= FinalDamage; //여기에 속도비례 보정 들어가야함
        }
        if (a.HP <= 0)
        {
            return false;
        }
        return true;
    }

    private bool EnemyDamageCalculate()
    {
        float FinalDamage = player.ATK; // 여기에 패시브스킬 등으로 변하는 데미지 계산

        a.HP -= FinalDamage;
        b.HP -= FinalDamage;

        return true;
    }
}

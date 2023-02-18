using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoSingleton<BattleManager>
{
    private Ar a = null, b = null;
    private Vector2 aNomal, bNomal;

    public void CrashSet(Ar bullet, Vector2 nomal)
    {
        if (a == null)
        {
            a = bullet;
            aNomal = nomal;

        }
        else if (b == null)
        {
            b = bullet;
            bNomal = nomal;

            CrashResult();
        }
    }

    private void CrashResult()
    {
        if(a.isBlueTeam!=b.isBlueTeam)
        {
            a.BeforeBattle?.Invoke(); //
            b.BeforeBattle?.Invoke(); //���� ���� �ߵ��ϴ� Ʈ����
        }

        if (a.lastVelocity.magnitude > b.lastVelocity.magnitude)
        {
            if (a.isBlueTeam != b.isBlueTeam)
            {
                a.BeforeAttack?.Invoke();
                b.BeforeDefence?.Invoke();
            }

            if (DamageCalculate())
            {
                var reflect = Vector2.Reflect(a.lastVelocity.normalized, aNomal);
                a.rigid.velocity = (reflect);
                b.rigid.velocity = (-reflect + a.lastVelocity / 2);
            }
            if (a.isBlueTeam != b.isBlueTeam)
            {
                a.AfterAttack?.Invoke();
                b.AfterDefence?.Invoke();
            }
                
        }
        else if (a.lastVelocity.magnitude < b.lastVelocity.magnitude)
        {
            if (a.isBlueTeam != b.isBlueTeam)
            {
                b.BeforeAttack?.Invoke();
                a.BeforeDefence?.Invoke();
            }

            if (DamageCalculate())
            {
                var reflect = Vector2.Reflect(b.lastVelocity.normalized, bNomal);
                b.rigid.velocity = (reflect);
                a.rigid.velocity = (-reflect + b.lastVelocity / 2);
            }
            if (a.isBlueTeam != b.isBlueTeam)
            {
                b.AfterAttack?.Invoke();
                a.AfterDefence?.Invoke();
            }

        }
        if (a.isBlueTeam != b.isBlueTeam)
        {
            a.AfterBattle?.Invoke(); //
            b.AfterBattle?.Invoke(); // ���� ���� �ߵ��ϴ� Ʈ����
        }

        a.AttackFinish();
        b.AttackFinish();
        a = null;
        b = null;
    }

    private bool DamageCalculate()
    {
        if (a.isBlueTeam == b.isBlueTeam) return true;
        float aFinalDamage = a.ATK; //
        float bFinalDamage = b.ATK; //���⿡ �нú꽺ų ������ ���ϴ� ������ ���
        if (a.isDoubleAttack)
        {
            if (b.isDoubleAttack)
            {
                a.HP -= bFinalDamage; // 
                b.HP -= aFinalDamage; //���⿡ �ӵ���� ���� ������
                if (a.HP <= 0 || b.HP <= 0)
                {
                    return false;
                }
                a.HP -= bFinalDamage; // 
                b.HP -= aFinalDamage; //���⿡ �ӵ���� ���� ������
            }
            else if (b.isFirstAttack)
            {
                b.HP -= aFinalDamage; //���⿡ �ӵ���� ���� ������
                a.HP -= bFinalDamage; // 
                if (a.HP <= 0 || b.HP <= 0)
                {
                    return false;
                }
                a.HP -= bFinalDamage; //���⿡ �ӵ���� ���� ������
            }
            else
            {
                b.HP -= aFinalDamage; // 
                if (a.HP <= 0 || b.HP <= 0)
                {
                    return false;
                }
                a.HP -= bFinalDamage; // 
                b.HP -= aFinalDamage; //���⿡ �ӵ���� ���� ������
            }
        }
        else if (a.isFirstAttack)
        {
            if (b.isDoubleAttack)
            {
                a.HP -= bFinalDamage; // 
                b.HP -= aFinalDamage; //���⿡ �ӵ���� ���� ������
                if (a.HP <= 0 || b.HP <= 0)
                {
                    return false;
                }
                a.HP -= bFinalDamage; //���⿡ �ӵ���� ���� ������
            }
            else if (b.isFirstAttack)
            {
                a.HP -= bFinalDamage; // 
                b.HP -= aFinalDamage; //���⿡ �ӵ���� ���� ������
            }
            else
            {
                b.HP -= aFinalDamage; // 
                if (a.HP <= 0 || b.HP <= 0)
                {
                    return false;
                }
                a.HP -= bFinalDamage; //���⿡ �ӵ���� ���� ������
            }
        }
        else
        {
            if (b.isDoubleAttack)
            {
                a.HP -= bFinalDamage; //���⿡ �ӵ���� ���� ������
                if (a.HP <= 0 || b.HP <= 0)
                {
                    return false;
                }
                a.HP -= bFinalDamage; // 
                b.HP -= aFinalDamage; //���⿡ �ӵ���� ���� ������
            }
            else if (b.isFirstAttack)
            {
                a.HP -= bFinalDamage; // ���⿡ �ӵ���� ���� ������
                if (a.HP <= 0 || b.HP <= 0)
                {
                    return false;
                }
                b.HP -= aFinalDamage; // 
            }
            else
            {
                a.HP -= bFinalDamage; // 
                b.HP -= aFinalDamage; //���⿡ �ӵ���� ���� ������
            }
        }
        if (a.HP <= 0 || b.HP <= 0)
        {
            return false;
        }
        return true;
    }
}

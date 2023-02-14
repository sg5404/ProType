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

    public void EnemyCrashSet(Enemy bullet, Vector2 nomal, bool isCharge)
    {
        if (a == null)
        {
            a = bullet;
            aNomal = nomal;

            if (player != null && isCharge && !player.isCharge) EnemyAttackCrashResult();
            else PlayerCrashResult();
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
        player.BeforeBattle?.Invoke(); //���� ���� �ߵ��ϴ� Ʈ����
        a.BeforeBattle?.Invoke(); //
        Debug.Log("�÷��̾� ����");

        if (DamageCalculate())
        {
            var reflect = Vector2.Reflect(player.lastVelocity.normalized, pNomal);
            player.rigid.velocity = (reflect);
            a.rigid.velocity = (-reflect + a.lastVelocity / 2);
            Debug.Log("������ ���");
        }

        player.AfterBattle?.Invoke(); // ���� ���� �ߵ��ϴ� Ʈ����
        a.AfterBattle?.Invoke(); //

        player.AttackFinish();
        a.AttackFinish();
        a = null;
    }

    private void EnemyAttackCrashResult()
    {
        player.BeforeBattle?.Invoke(); //���� ���� �ߵ��ϴ� Ʈ����
        a.BeforeBattle?.Invoke(); //
        Debug.Log("�÷��̾� �ǰ�");

        if (PlayerDamageCalculate())
        {
            var reflect = Vector2.Reflect(player.lastVelocity.normalized, pNomal);
            player.rigid.velocity = (reflect);
            a.rigid.velocity = (-reflect + a.lastVelocity / 2);
            Debug.Log("������ ���");
        }

        player.AfterBattle?.Invoke(); // ���� ���� �ߵ��ϴ� Ʈ����
        a.AfterBattle?.Invoke(); //

        player.AttackFinish();
        a.AttackFinish();
        a = null;
    }

    private void EnemyCrashResult()
    {
        a.BeforeBattle?.Invoke(); //
        b.BeforeBattle?.Invoke(); //���� ���� �ߵ��ϴ� Ʈ����
        Debug.Log("�� ���� �浹");
        if (a.lastVelocity.magnitude > b.lastVelocity.magnitude)
        {
            if (EnemyDamageCalculate())
            {
                var reflect = Vector2.Reflect(a.lastVelocity.normalized, aNomal);
                a.rigid.velocity = (reflect);
                b.rigid.velocity = (-reflect + a.lastVelocity);
                Debug.Log("������ ���");
            }
        }
        else if (a.lastVelocity.magnitude < b.lastVelocity.magnitude)
        {
            if (EnemyDamageCalculate())
            {
                var reflect = Vector2.Reflect(b.lastVelocity.normalized, bNomal);
                b.rigid.velocity = (reflect);
                a.rigid.velocity = (-reflect + b.lastVelocity);
                Debug.Log("������ ���");
            }
        }
        a.AfterBattle?.Invoke(); //
        b.AfterBattle?.Invoke(); // ���� ���� �ߵ��ϴ� Ʈ����

        a.AttackFinish();
        b.AttackFinish();
        a = null;
        b = null;
    }

    private bool DamageCalculate()
    {
        float FinalDamage = player.ATK; // ���⿡ �нú꽺ų ������ ���ϴ� ������ ���
        if (player.isDoubleAttack)
        {
            a.HP -= FinalDamage; // 
            if (a.HP <= 0)
            {
                return false;
            }
            a.HP -= FinalDamage; // ���⿡ �ӵ���� ���� ������
        }
        else
        {
            if (a.HP <= 0)
            {
                return false;
            }
            a.HP -= FinalDamage; //���⿡ �ӵ���� ���� ������
        }
        if (a.HP <= 0)
        {
            return false;
        }
        return true;
    }

    private bool EnemyDamageCalculate()
    {
        float FinalDamage = player.ATK; // ���⿡ �нú꽺ų ������ ���ϴ� ������ ���

        a.HP -= FinalDamage;
        b.HP -= FinalDamage;

        return true;
    }

    private bool PlayerDamageCalculate()
    {
        float FinalDamage = a.ATK;

        if (player.isInvinsible || player.isCharge) return true;
        player.HP -= FinalDamage;

        return true;
    }
}

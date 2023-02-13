using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamenRider : Boss
{
    [SerializeField] GameObject itemBox;
    [SerializeField] Bullet breath;

    private int stunCounter = 0;

    protected override void Start()
    {
        base.Start();
        MaxHP = 10;
        HP = MaxHP;
        ATK = 50;
        itemBox.SetActive(false);
        AfterBattle.AddListener(() => { stunCounter++; });
        OnBattleDie.AddListener(SummonItemBox);
        OnOutDie.AddListener(SummonItemBox);
    }

    private void SummonItemBox()
    {
        itemBox.SetActive(true);
    }

    public override IEnumerator Pattern1()
    {
        for(int i=0; i<15; i++)
        {
            var _breath = Instantiate(breath, null);
            _breath.transform.position = transform.position;
            _breath.SetRigidPower((player.transform.position - transform.position).normalized * 4);
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }

    public override IEnumerator Pattern2()
    {
        yield return new WaitForSeconds(2f);
        if (stunCounter >= 2)
        {
            yield return new WaitForSeconds(4f);
        }
        else
        {
            isCharge = true;
            SetRigidPower((player.transform.position - transform.position).normalized * 50);
        }
        stunCounter = 0;
    }
}

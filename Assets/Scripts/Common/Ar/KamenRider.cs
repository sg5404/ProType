using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamenRider : Boss
{
    [SerializeField] GameObject itemBox;
    protected override void Start()
    {
        base.Start();
        MaxHP = 1500;
        HP = MaxHP;
        ATK = 50;
        itemBox.SetActive(false);
        OnBattleDie.AddListener(SummonItemBox);
        OnOutDie.AddListener(SummonItemBox);
    }

    private void SummonItemBox()
    {
        itemBox.SetActive(true);
    }
}

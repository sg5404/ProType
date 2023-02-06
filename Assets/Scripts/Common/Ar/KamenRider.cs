using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamenRider : Boss
{
    protected override void Start()
    {
        base.Start();
        MaxHP = 1500;
        HP = MaxHP;
        ATK = 50;
    }
}

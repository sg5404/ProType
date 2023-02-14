using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Ar
{
    public BulletSO bulletSO;

    private void Start()
    {
        base.Start();
        Destroy(this.gameObject, 10f);
    }


}

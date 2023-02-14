using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Ar
{
    public BulletSO bulletSO;

    private void Start()
    {
        base.Start();
        Destroy(gameObject, 10f);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Object"))
            Destroy(gameObject);
    }
}

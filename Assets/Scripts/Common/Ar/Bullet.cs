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

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Object"))
            Destroy(gameObject, 0.2f);
    }
}

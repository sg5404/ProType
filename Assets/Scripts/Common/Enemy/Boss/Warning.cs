using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour
{
    Collider2D collider;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
        collider.enabled = false;
    }

    public void Blow()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color32(160, 0, 0, 255);
        collider.enabled = true;
        Destroy(gameObject, 1f);
    }
}

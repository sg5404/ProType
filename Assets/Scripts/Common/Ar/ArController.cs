using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArController : MonoBehaviour
{
    [SerializeField] Ar ar;
    [SerializeField] CircleCollider2D stickBase;
    private Vector2 pos;

    private void OnMouseDrag()
    {
        pos = BattleManager.Instance.mousePosition - stickBase.transform.position;

        pos = Vector2.ClampMagnitude(pos, stickBase.radius);

        transform.localPosition = pos;
    }

    private void OnMouseUp()
    {
        ar.Dash(transform.localPosition);

        transform.localPosition = Vector3.zero;
    }
}

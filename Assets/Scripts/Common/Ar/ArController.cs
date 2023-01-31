using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArController : MonoBehaviour
{
    [SerializeField] Ar ar;
    private Vector2 pos;

    private void OnMouseDrag()
    {
        pos = BattleManager.Instance.mousePosition - transform.parent.position;

        pos = Vector2.ClampMagnitude(pos, 1.5f);

        transform.localPosition = pos;
    }

    private void OnMouseUp()
    {
        ar.Dash(transform.localPosition);

        transform.localPosition = Vector3.zero;
    }
}

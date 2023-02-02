using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player player;
    private Vector2 pos;
    private float power;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnMouseDrag()
    {
        pos = BattleManager.Instance.mousePosition - transform.parent.position;
        
        pos = Vector2.ClampMagnitude(pos, 1.5f);

        transform.localPosition = pos;
    }

    private void OnMouseUp()
    {
        power = Vector2.Distance(transform.localPosition, transform.parent.position);
        if(StaminaManager.Instance.UseStamina(power))
            player.Dash(transform.localPosition);
        transform.localPosition = Vector3.zero;
    }
}

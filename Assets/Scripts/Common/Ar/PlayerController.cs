using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Player player;
    private float power;
    private RectTransform rect;
    [SerializeField] RectTransform stick;
    [SerializeField] float stickRange;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        rect = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        var inputDir = eventData.position - rect.anchoredPosition;
        var clampedDir = inputDir.magnitude < stickRange ? inputDir : inputDir.normalized * stickRange;

        stick.anchoredPosition = clampedDir;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        power = Vector2.Distance(stick.localPosition, transform.position)/300;
        if (StaminaManager.Instance.UseStamina(power))
            player.Dash(stick.localPosition);
        stick.anchoredPosition = Vector2.zero;
    }
}

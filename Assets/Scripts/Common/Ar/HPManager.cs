using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoSingleton<HPManager>
{
    private Player player;
    private Image HP;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        HP = transform.GetChild(1).GetComponent<Image>();
    }

    public void ChangeHP()
    {
        HP.fillAmount = player.HP / player.MaxHP;
    }
}

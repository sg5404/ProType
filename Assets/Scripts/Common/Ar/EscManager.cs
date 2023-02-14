using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscManager : MonoSingleton<EscManager>
{
    [SerializeField] GameObject EscPanel;
    [SerializeField] GameObject weaponPanel;
    [SerializeField] PlayerSO player;
    Player _player;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        ToUpdate();    
    }
    public void ToUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePanel();
        }
    }

    public void TogglePanel()
    {
        EscPanel.SetActive(!EscPanel.activeSelf);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SellectSword()
    {
        player.weapon = Weapon.Sword;
        _player.ClassSet();
        weaponPanel.SetActive(false);
    }
    public void SellectBow()
    {
        player.weapon = Weapon.Bow;
        _player.ClassSet();
        weaponPanel.SetActive(false);
    }
}

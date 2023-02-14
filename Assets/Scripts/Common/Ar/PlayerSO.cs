using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weapon
{
    None,
    Sword,
    Bow,
}

[CreateAssetMenu(menuName = "SO/Ar/Player"), System.Serializable]
public class PlayerSO : ScriptableObject
{
    public Weapon weapon;
    public Bullet bullet;
    public float HP;
    public float ATK;
    public float pushPower;
}

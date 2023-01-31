using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weapon
{
    None,
    Sword,
    Bow,
}

[CreateAssetMenu(menuName = "SO/Ar"), System.Serializable]
public class ArSO : ScriptableObject
{
    public Weapon weapon;
    public Ar bullet;
}

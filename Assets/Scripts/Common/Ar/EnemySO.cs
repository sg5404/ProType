using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Melee,
    Range,
    Both
}

[CreateAssetMenu(menuName = "SO/Ar/Enemy"), System.Serializable]
public class EnemySO : ScriptableObject
{
    public EnemyType type;
    public float HP;
    public float ATK;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Inspector"), System.Serializable]
public class PlayerInspector : ScriptableObject
{
    public string PlayerName = "";

    public int Atk;
    public int Def;
    public int Hp;

    public int ItemAtk;
    public int ItemDef;
    public int ItemHp;

    public List<ItemSO> Items;
}

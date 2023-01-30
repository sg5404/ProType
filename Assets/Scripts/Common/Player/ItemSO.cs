using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Item"), System.Serializable]
public class ItemSO : ScriptableObject
{
    public string name = "";
    public Item item;
    public Rarity rarity;
}

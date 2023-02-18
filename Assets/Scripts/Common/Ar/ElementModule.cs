using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Module/Element"), System.Serializable]
public class ElementModule : ScriptableObject
{
    public string Name => name;
    [SerializeField] private string name;
    public float HP => hp;
    [SerializeField] private float hp;

    public float Atk => atk;
    [SerializeField] private float atk;

    public Property Type => type;
    [SerializeField] private Property type;

    public Rarity Grade => grade;
    [SerializeField] private Rarity grade;

    public int Quantity;

    public ElementTreeSO JohabTree;
}

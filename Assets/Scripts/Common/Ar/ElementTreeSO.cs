using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ElementTree
{
    public int Amount;
    public ElementModule Element;
}

[CreateAssetMenu(menuName = "SO/Module/ElementTree"), System.Serializable]
public class ElementTreeSO : ScriptableObject
{
    public List<ElementTree> ElementTrees;
}

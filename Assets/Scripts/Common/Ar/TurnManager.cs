using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoSingleton<TurnManager>
{
    [SerializeField]
    private Ar[] ars;
    private List<Ar> redArs = new List<Ar>();
    private List<Ar> blueArs = new List<Ar>();
    public static int Turn { get; private set; }

    private void Awake()
    {
        ars = FindObjectsOfType<Ar>();
        DevideTeam();
    }

    public void AddTrun(int add = 1)
    {
        Turn += add;
        foreach (Ar ar in ars)
            ar.CoolDown();
    }

    private void DevideTeam()
    {
        foreach (Ar ar in ars)
        {
            if (ar.isBlueTeam) blueArs.Add(ar);
            else redArs.Add(ar);
        }
    }

    public void ArDie(Ar ar)
    {
        blueArs.Remove(ar);
        redArs.Remove(ar);
        FinishCheck();
    }

    private void FinishCheck()
    {
        if (blueArs.Count <= 0)
        {
            Debug.Log("·¹µåÆÀ ½Â¸®!");
        }
        else
        {
            Debug.Log("ºí·çÆÀ ½Â¸®!");
        }
    }
}

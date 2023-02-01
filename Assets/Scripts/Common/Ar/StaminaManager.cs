using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaManager : MonoSingleton<StaminaManager>
{
    private ArStamina[] staminas;

    private void Start()
    {
        staminas = GetComponentsInChildren<ArStamina>();
    }

    public bool UseStamina(float power)
    {
        foreach(ArStamina stamina in staminas)
        {
            if (stamina.Use(power))
            {
                return true;
            }
        }
        return false;
    }
}

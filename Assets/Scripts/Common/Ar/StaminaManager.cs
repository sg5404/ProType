using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaManager : MonoSingleton<StaminaManager>
{
    private PlayerStamina[] staminas;

    private void Start()
    {
        staminas = GetComponentsInChildren<PlayerStamina>();
    }

    public bool UseStamina(float power)
    {
        foreach(PlayerStamina stamina in staminas)
        {
            if (stamina.Use(power))
            {
                return true;
            }
        }
        return false;
    }
}

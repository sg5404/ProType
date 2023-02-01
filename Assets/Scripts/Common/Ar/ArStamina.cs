using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArStamina : MonoBehaviour
{
    public bool isUsed { get; private set; }
    private float useCooltime;
    private Image staminaImage;

    private void Start()
    {
        staminaImage = GetComponent<Image>();
    }

    private void Update()
    {
        UpdateCooltime();
    }

    public bool Use(float power)
    {
        if (isUsed) return false;

        isUsed = true;
        StartCoroutine(StaminaCharge(power));
        return true;
    }

    public void UpdateCooltime()
    {
        if(useCooltime>0)
        useCooltime -= Time.deltaTime;
    }

    private IEnumerator StaminaCharge(float power)
    {
        useCooltime = power;
        staminaImage.fillAmount = 0;
        while (true)
        {
            staminaImage.fillAmount = (power - useCooltime)/power;
            if (staminaImage.fillAmount == 1)
                break;
            yield return null;
        }
        isUsed = false;
    }
}

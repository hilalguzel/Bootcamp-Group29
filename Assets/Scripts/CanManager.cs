using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanManager : MonoBehaviour
{
    public CharacterController karakter;
    public Image fillImage;
    private Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }


    void Update()
    {
        if (slider.value <= slider.minValue)
        {
            fillImage.enabled = false;
        }

        if (slider.value > slider.minValue && !fillImage.enabled)
        {
            fillImage.enabled = true;
        }

        float fillValue = karakter.currentHealth / karakter.maxHealth;

        slider.value = fillValue;
    }
}

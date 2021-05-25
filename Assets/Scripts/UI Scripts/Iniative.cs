using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Iniative : MonoBehaviour
{

    public Slider slider;
    /// <summary>
    /// Get value from one turn in Turn Calculator
    /// </summary>
    void Start()
    {
        slider.maxValue = 1000;
    }

    void Update()
    {
        if (TurnCalculator.iniativeArray[0] == 0)
        {
            slider.value = 1000;
        }
        else
        {
            slider.value = TurnCalculator.iniativeArray[0];
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    //Script for moving the HP bar slider
    public Slider slider;
     
    void Start()
    {
        slider.maxValue = 100;
        slider.value = 100;
    }

    void Update()
    {
        slider.value = PlayerInfo.HP;
        slider.maxValue = PlayerInfo.maxHP;
    }

    public void UpdateHP()
    {
        slider.value = PlayerInfo.HP;
    }

    public void UpdateMaxHP()
    {
        slider.maxValue = PlayerInfo.maxHP;
    }
}

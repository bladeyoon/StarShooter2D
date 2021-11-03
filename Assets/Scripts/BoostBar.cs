using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostBar : MonoBehaviour
{
    public Slider slider;

    public float sliderValue;

    public Player player;

    public void Start()
    {
        sliderValue = slider.value;
    }

    public void IncreaseBoostValue(float boostValue)
    {
        slider.value += boostValue;
    }

    public void ReduceBoostValue()
    {
        if (slider.value > 0)
        {
            slider.value -= 1 * Time.deltaTime;
        }
        else
        {
            player.SpeedUpDisabled();
        }
    }
}

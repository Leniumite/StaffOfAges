using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUiManager : MySingleton<LevelUiManager>
{
    public override bool DoDestroyOnLoad
    {
        get { return true; }
    }

    public Slider playerHealthSlider;

    private void Start()
    {
        UpdateSlider(1);
    }

    public void UpdateSlider(float value)
    {
        playerHealthSlider.value = value;
    }
}

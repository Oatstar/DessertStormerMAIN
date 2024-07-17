using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] Slider healthSlider;
    [SerializeField] TMP_Text floorCounter;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void RefreshHealthSlider(int healthValue)
    {
        healthSlider.value = healthValue;
    }

    public void RefreshFloorCounter(int floorCount)
    {
        floorCounter.text = "FLOOR: " + floorCount.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private Image fill;
    [SerializeField] private Slider bar;

    public void UpdateBar(float value)
    {
        bar.value = value;
    }

    public void ChangeState(string name, Color color)
    {
        statusText.text = name;
        statusText.color = color;
        fill.color = color;
    }
}

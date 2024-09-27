using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextController : MonoBehaviour
{
    [SerializeField] private Transform cashparent;
    [SerializeField] private Transform bottleparent;
    [SerializeField] private FloatingText cashPrefab;
    [SerializeField] private FloatingText bottlePrefab;
    [SerializeField] private float time;
    private FloatingText cashCurrentFloatText;
    private FloatingText bottleCurrentFloatText;

    public void CreateEffect(float value)
    {
        Debug.Log($"{value}");
        if(value >= 0f)
        {
            if(!cashCurrentFloatText)
            {
                cashCurrentFloatText = Instantiate(cashPrefab, cashparent);
                cashCurrentFloatText.Init(time);
                cashCurrentFloatText.AddNum(value);
            }
            else
            {
                cashCurrentFloatText.AddNum(value);
            }
        }
        else
        {
            if(!bottleCurrentFloatText)
            {
                bottleCurrentFloatText = Instantiate(bottlePrefab, bottleparent);
                bottleCurrentFloatText.transform.localPosition = Vector2.zero;
                bottleCurrentFloatText.Init(time);
                bottleCurrentFloatText.AddNum(value);
            }
            else
            {
                bottleCurrentFloatText.AddNum(value);
            }     
        }
    }
}

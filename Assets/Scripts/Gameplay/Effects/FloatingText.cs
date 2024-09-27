using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Animator animator;
    [SerializeField] private float offset = 5f;
    private float timer;
    private float currentNum;

    public void Init(float time)
    {
        timer = time;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            Destroy(gameObject);
        }

        text.color = new Color(text.color.r, text.color.g, text.color.b, timer);
        transform.DOMoveY(transform.position.y + offset, speed);
    }

    public void AddNum(float value)
    {
        if(timer > 0f)
        {
            currentNum += -value;

            if(value >= 0f)
            {
               text.text = $"+{currentNum}$";
            }
            else
            {
               text.text = $"-{currentNum}$";
            }

            animator.SetTrigger("isAdded");
        }
    }
}

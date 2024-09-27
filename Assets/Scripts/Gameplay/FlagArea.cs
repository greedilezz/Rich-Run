using UnityEngine;
using DG.Tweening;

public class FlagArea : MonoBehaviour
{
    [SerializeField] private Transform flag1;
    [SerializeField] private Transform flag2;
    [SerializeField] private float durationOpen = 0.5f;

    public void OpenFlag()
    {
        flag1.DORotate(new Vector3(flag1.eulerAngles.x,flag1.eulerAngles.y,0f), durationOpen);
        flag2.DORotate(new Vector3(flag2.eulerAngles.x,flag2.eulerAngles.y,0f), durationOpen);
    }
}

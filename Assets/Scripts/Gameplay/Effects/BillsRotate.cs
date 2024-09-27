using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BillsRotate : MonoBehaviour
{
    [SerializeField] private float speedRotate;
    void Update()
    {
        transform.eulerAngles += new Vector3(0,1,0) * speedRotate;        
    }
}

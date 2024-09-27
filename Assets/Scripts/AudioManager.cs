using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioClip takeCash;
    [SerializeField] private AudioClip takeBottle;
    [SerializeField] private AudioClip flag;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Instance = this;
    }

    public void CollectCoin()
    {
        audioSource.PlayOneShot(takeCash);
    }

    public void CollectBottle()
    {
        audioSource.PlayOneShot(takeBottle);
    }

    public void Flag()
    {
        audioSource.PlayOneShot(flag);
    }
    
}

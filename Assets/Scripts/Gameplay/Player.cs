using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;

public class PlayerInfo
{
    public float status;
    public float cash;
}

public class Player : MonoBehaviour
{
    public static Action<float> OnFinish;
    public static Action<float> OnLose;

    [Serializable]
    class PlayerStates
    {
        public string StatusName;
        public Color ColorName;
        public int animId;
        public GameObject Skin;
        public float Value;        
        public bool IsReached;
    }

    [SerializeField] private Animator animator;
    [SerializeField] private FloatingTextController floatingTextController;
    [SerializeField] private List<PlayerStates> playerStates = new();
    [SerializeField] private StatusBar statusBar;
    private PlayerStates currentPlayerState;
    public PlayerInfo playerInfo;

    private void Start()
    {
        currentPlayerState = playerStates[0];
        playerInfo = new PlayerInfo();    
    }

    private void OnEnable() 
    {
        UIManager.OnStartGame += SetMove;
    }

    private void OnDisable()
    {
        UIManager.OnStartGame -= SetMove;
    }

    private void SetMove()
    {
        Debug.Log($"Starting");
        animator.SetFloat("StateId", 1);
        Debug.Log($"Setttingg");
    }

    private void ChangeStatus(float value)
    {
        playerInfo.status = Mathf.Clamp(playerInfo.status + value, 0, 100f);
        floatingTextController.CreateEffect(value);

        if(playerInfo.status <= 0f)
        {
            animator.SetBool("defeat", true);
            OnLose?.Invoke(playerInfo.status);
        }

        UIManager.Instance.RefreshCounter(playerInfo.status);
        statusBar.UpdateBar(playerInfo.status);

        if(value >= 0)
        {
            foreach (var state in playerStates)
            {   
                if(!state.IsReached)
                {
                    if(playerInfo.status >= state.Value)
                    {
                        RefreshSkin(state); 
                    }                
                }
            }            
        }
        else
        {
            if(currentPlayerState.Value > playerInfo.status)
            {
                currentPlayerState.IsReached = false;
                int index = playerStates.IndexOf(currentPlayerState);

                RefreshSkin(playerStates[index-1]);
            }
        }
    }

    private void RefreshSkin(PlayerStates state) // Обновка скина
    {
        if(currentPlayerState != null)
        {
            currentPlayerState.Skin.SetActive(false);
        }

        state.Skin.SetActive(true);
        currentPlayerState = state;
        state.IsReached = true;
        
        animator.SetFloat("StateId", state.animId);
        statusBar.ChangeState(state.StatusName, state.ColorName);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Cash"))
        {
            AudioManager.Instance.CollectCoin();
            ChangeStatus(3f);
            Destroy(other.gameObject);
        }   
        if(other.CompareTag("Bottle"))
        {
            AudioManager.Instance.CollectBottle();
            ChangeStatus(-5f);
            Destroy(other.gameObject);
        }
        if(other.CompareTag("RedGates"))
        {
            AudioManager.Instance.CollectBottle();
            ChangeStatus(-20f);
            Destroy(other.gameObject);
        }
        if(other.CompareTag("GreenGates"))
        {
            AudioManager.Instance.CollectCoin();
            ChangeStatus(20f);
            Destroy(other.gameObject);
        }
        if(other.TryGetComponent(out FlagArea flagArea))
        {
            AudioManager.Instance.Flag();
            flagArea.OpenFlag();
        }

        if(other.CompareTag("Finish"))
        {
            animator.SetBool("dance", true);
            OnFinish?.Invoke(playerInfo.status);
        }
    }
}

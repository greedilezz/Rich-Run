using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public static Action OnStartGame;
    public static UIManager Instance;
    [SerializeField] private ScenesManager scenesManager;
    [SerializeField] private TMP_Text finishCounter;
    [SerializeField] private TMP_Text cashCounter;
    [SerializeField] private TMP_Text levelCounter;
    [SerializeField] private Transform[] startMenuUI;
    [SerializeField] private Transform[] gameMenuUI;
    [SerializeField] private Transform[] finishMenuUI;
    [SerializeField] private Transform[] loseMenuUI;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable() 
    {
        Player.OnFinish += FinishMenu;        
        Player.OnLose += LoseMenu;        
    }

    private void OnDisable()
    {
        Player.OnFinish -= FinishMenu;   
        Player.OnLose -= LoseMenu;         
    }

    public void StartGame()
    {
        OnStartGame?.Invoke();

        foreach (var item in gameMenuUI)
        {
            item.gameObject.SetActive(true);
        }

        foreach (var item in startMenuUI)
        {
            item.gameObject.SetActive(false);
        }
    }

    public void GoToMenu()
    {
        foreach (var item in startMenuUI)
        {
            item.gameObject.SetActive(true);
        }
        
        foreach (var item in gameMenuUI)
        {
            item.gameObject.SetActive(false);
        }
    }

    public void LoseMenu(float coins)
    {
        foreach (var item in gameMenuUI)
        {
            item.gameObject.SetActive(false);
        }
        foreach (var item in loseMenuUI)
        {
            item.gameObject.SetActive(true);
        }  
    }

    public void Repeat()
    {
        scenesManager.LoadScene(0);
        // SceneManager.LoadScene(0);
    }

    public void FinishMenu(float coins)
    {
        finishCounter.text = $"{coins}$";
        foreach (var item in gameMenuUI)
        {
            item.gameObject.SetActive(false);
        }
        foreach (var item in finishMenuUI)
        {
            item.gameObject.SetActive(true);
        }
    }

    public void CollectCoins()
    {
        scenesManager.LoadScene(1);
        // SceneManager.LoadScene(1);
    }

    public void RefreshCounter(float value)
    {
        cashCounter.text = value.ToString();
    }

}

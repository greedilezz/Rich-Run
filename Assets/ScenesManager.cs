using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{
    private GameObject _transitionScreenPrefab, _transitionScreenInstance;

    private Image _transScreenImage;

    public void LoadLevelToLoad()
    {
        int levelToLoad = PlayerPrefs.GetInt("LevelToLoad", 1);
        LoadScene(levelToLoad);
    }

    public void SetNextSceneAsLevelToLoad()
    {
        int levelToLoad = PlayerPrefs.GetInt("LevelToLoad", 1);
        levelToLoad++;
        PlayerPrefs.SetInt("LevelToLoad", levelToLoad);
    }

    public void SetLevelToLoad(int targetValue) => PlayerPrefs.SetInt("LevelToLoad", targetValue);

    private void Awake()
    {
        Time.timeScale = 1.0f;
        _transitionScreenPrefab = Resources.Load("TransitionScreen") as GameObject;
        _transitionScreenInstance = Instantiate(_transitionScreenPrefab);
        _transScreenImage = _transitionScreenInstance.GetComponentInChildren<Image>();
        _transScreenImage.color = Color.black;
        _transScreenImage.DOColor(Color.clear, 1f);
    }

    public void LoadScene(int index) => LoadSceneAnimated(index);

    private bool _isLoading = false;

    private void LoadSceneAnimated(int index)
    {
        if (!_isLoading)
        {
            _transScreenImage.DOColor(Color.black, 1f);
            StartCoroutine(LoadDelay(index));
            _isLoading = true;
        }
    }

    private IEnumerator LoadDelay(int index)
    {
        yield return new WaitForSeconds(1f);
        {
            SceneManager.LoadScene(index);
        }
    }

    public void LoadNextScene() => LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    public void Reload() => LoadScene(SceneManager.GetActiveScene().buildIndex);
}

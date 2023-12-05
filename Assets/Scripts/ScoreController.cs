using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public static ScoreController Instance { get; private set; }
    public bool hasCollided = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        var buttonObject = GameObject.FindWithTag("RestartButton");
        var button = buttonObject.GetComponent<Button>();
        button.onClick.AddListener(RestartGame);
    }

    public void GameOver()
    {
        var uiPanel = GameObject.FindWithTag("UIPanel");
        var canvasGroup = uiPanel.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void RestartGame()
    {
        Debug.Log("restarting game");
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}

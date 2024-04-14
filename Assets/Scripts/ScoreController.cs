using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public static ScoreController Instance { get; private set; }
    public bool hasCollided = false;

    private float scoreTime = 0f;

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

    private void Update()
    {
        if (!hasCollided)
        {
            scoreTime += Time.deltaTime;
        }
    }

    public string GetCurrentScoreTime()
    {
        int totalSeconds = Mathf.FloorToInt(scoreTime);
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        string timerText = string.Format("{0:D2}:{1:D2}", minutes, seconds);
        return timerText;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public static ScoreController Instance { get; private set; }
    public bool hasCollided = false;
    private Button startButton;
    private CanvasGroup uiPanel;

    private float scoreTime = 0f;

    private AudioController _audioController;

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

        _audioController = GetComponent<AudioController>();

        Time.timeScale = 0;
    }

    private void Start()
    {
        Debug.Log("start");

        uiPanel = GameObject.FindWithTag("UIPanel").GetComponent<CanvasGroup>();

        var startButtonObject = GameObject.FindWithTag("StartButton");
        startButton = startButtonObject.GetComponent<Button>();
        Debug.Log(startButton);
        startButton.onClick.AddListener(StartGame);

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

    public void StartGame()
    {
        Debug.Log("start game");

        Time.timeScale = 1;

        uiPanel.alpha = 0;
        uiPanel.interactable = false;
        uiPanel.blocksRaycasts = false;

        startButton.gameObject.SetActive(false);

        _audioController.PlayAudio();
    }

    public void GameOver()
    {
        uiPanel.alpha = 1;
        uiPanel.interactable = true;
        uiPanel.blocksRaycasts = true;

        _audioController.SetAudioLowpass(true);

        StartCoroutine(RestartGame());
    }

    public IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(1.5f);

        Debug.Log("restarting game");
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}

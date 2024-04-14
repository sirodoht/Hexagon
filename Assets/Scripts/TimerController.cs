using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    private TextMeshProUGUI timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.FindWithTag("TimerText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        timer.text = ScoreController.Instance.GetCurrentScoreTime();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float _rate = 0.25f;

    private float _timePassedBackground;

    private Color lerpedColor = Color.white;
    private Renderer _renderer;

    [SerializeField] Color startColor;
    [SerializeField] Color endColor;

    private void Awake()
    {
        _renderer = GameObject.FindWithTag("Background").GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _timePassedBackground = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        var speed = Time.time * _rate;
        var pingpongValue = Mathf.PingPong(speed, 1f);
        lerpedColor = Color.Lerp(startColor, endColor, pingpongValue);
        //Debug.Log($"color: {lerpedColor}");
        _renderer.material.color = lerpedColor;

        _timePassedBackground += Time.deltaTime;
        if (_timePassedBackground > Random.Range(8f, 12f))
        {
            _timePassedBackground = 0;
            _rate = Random.Range(0.1f, 0.5f);
        }
    }
}

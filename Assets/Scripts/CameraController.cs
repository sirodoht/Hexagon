using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera _camera;
    private Transform _camTransform;

    private float _startCameraSize = 1.5f;
    private float _endCameraSize = 2;

    private float _rate = 0.00001f;

    private float _direction = 0.1f;
    private float _timePassedCamera;

    void Awake()
    {
        _camera = Camera.main;
        _camTransform = _camera.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        _timePassedCamera = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        _camTransform.Rotate(0, _direction, 0, Space.World);

        var speed = Time.time * _rate;
        var pingpongValue = Mathf.PingPong(speed, 1f);
        _camera.orthographicSize = Mathf.Lerp(
            _startCameraSize,
            _endCameraSize,
            pingpongValue
        );

        _timePassedCamera += Time.deltaTime;
        if (_timePassedCamera > Random.Range(3f, 5f))
        {
            _timePassedCamera = 0;
            _direction = -1 * _direction;

            _rate = Random.Range(0.00001f, 0.00005f);
        }
    }
}

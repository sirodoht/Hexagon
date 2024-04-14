using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera _camera;
    private Transform _camTransform;

    private float _startCameraSize = 1.5f;
    private float _endCameraSize = 2.5f;

    private float _startRotateAngle = 85;
    private float _endRotateAngle = 70;
    private float _rotationTime = 0;

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
        var range = _endRotateAngle - _startRotateAngle;
        var pingPong = (Mathf.Sin(_rotationTime * 1f - Mathf.PI / 2) + 1) / 2;

        _camera.orthographicSize = Mathf.Lerp(
            _startCameraSize,
            _endCameraSize,
            pingPong
        );

        var xRotationValue = pingPong * range + _startRotateAngle;
        Vector3 currentRotation = _camTransform.eulerAngles;
        currentRotation.x = xRotationValue;
        _camTransform.eulerAngles = currentRotation;
        _rotationTime += Time.deltaTime;

        _camTransform.Rotate(0, _direction, 0, Space.World);

        _timePassedCamera += Time.deltaTime;
        if (_timePassedCamera > Random.Range(3f, 5f))
        {
            _timePassedCamera = 0;
            _direction = -1 * _direction;
        }
    }
}

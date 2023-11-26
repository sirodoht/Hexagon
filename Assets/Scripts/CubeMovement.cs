using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    private GameObject hexagon;

    private void Awake()
    {
        hexagon = GameObject.FindWithTag("Hexagon");
    }

    void Start()
    {

    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, hexagon.transform.position, 1f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        if (other.gameObject.CompareTag("Triangle"))
        {
            Time.timeScale = 0f;
        }
    }
}

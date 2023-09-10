using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private GameObject hexagon;
    private GameObject triangle;
    [SerializeField] private int angleSpeed = 200;

    private void Awake()
    {
        hexagon = GameObject.FindWithTag("Hexagon");
        triangle = GameObject.FindWithTag("Triangle");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            triangle.transform.RotateAround(hexagon.transform.position, Vector3.up, -angleSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            triangle.transform.RotateAround(hexagon.transform.position, Vector3.up, angleSpeed * Time.deltaTime);
        }
    }
}

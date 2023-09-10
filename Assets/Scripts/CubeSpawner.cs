using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    [SerializeField] private float intervalPeriod = 2.0f;
    [SerializeField] private float timeSince = 0f;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSince += Time.deltaTime;
        if (intervalPeriod < timeSince)
        {
            Debug.Log("time passed");
            Instantiate(cube, Random.insideUnitSphere * 4, cube.transform.rotation);
            timeSince = 0f;
        }

    }
}

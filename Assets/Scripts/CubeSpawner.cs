using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    private Vector3[] pointsArray = new Vector3[6];
    private GameObject[] objectArray = new GameObject[6];
    private LineRenderer[] lrArray = new LineRenderer[6];
    private GameObject[] lrObjectsArray = new GameObject[6];
    private int moduloRange;
    bool movingHexagon;

    private void Awake()
    {

    }

    void Start()
    {
        DefinePositions();
    }

    void FixedUpdate()
    {
        if (!movingHexagon)
        {
            return;
        }

        for (var i = 0; i < objectArray.Length; i++)
        {
            objectArray[i].transform.position = Vector3.MoveTowards(
                objectArray[i].transform.position,
                transform.position,
                1f * Time.deltaTime
            );

            pointsArray[i] = objectArray[i].transform.position;
        }

        for (var i = 0; i < lrArray.Length; i++)
        {
            if (i % moduloRange == 0)
            {
                continue;
            }

            Vector3[] posArray = new Vector3[2];
            posArray[0] = pointsArray[i];
            if (i < lrArray.Length - 1)
            {
                posArray[1] = pointsArray[i + 1];
            }
            else
            {
                posArray[1] = pointsArray[0];
            }

            lrArray[i].SetPositions(posArray);

            Destroy(lrArray[i].gameObject.GetComponent<MeshCollider>());

            MeshCollider meshCollider = lrArray[i].gameObject.AddComponent<MeshCollider>();
            Mesh mesh = new Mesh();
            lrArray[i].BakeMesh(mesh, false);
            meshCollider.sharedMesh = mesh;

        }

        if (Vector3.Distance(objectArray[0].transform.position, Vector3.zero) < 0.35f)
        {
            movingHexagon = false;
            RestartSequence();
        }

        if (ScoreController.Instance.hasCollided)
        {
            movingHexagon = false;
            Debug.Log("game over");
            ScoreController.Instance.GameOver();
        }

    }

    void DefinePositions()
    {
        var numberOfObjects = 6;
        var radius = 2;

        for (var i = 0; i < numberOfObjects; i++)
        {
            var radians = 2 * Mathf.PI / numberOfObjects * i;
            var vertical = Mathf.Sin(radians);
            var horizontal = Mathf.Cos(radians);

            var spawnDir = new Vector3(horizontal, 0, vertical);
            var spawnPos = transform.position + spawnDir * radius;

            pointsArray[i] = spawnPos;

            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            var renderer = cube.GetComponent<Renderer>();
            renderer.enabled = false;

            var boxCollider = cube.GetComponent<BoxCollider>();
            Destroy(boxCollider);

            cube.transform.position = spawnPos;
            cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            objectArray[i] = cube;
        }

        DrawHexagon();
    }

    void DrawHexagon()
    {
        moduloRange = Random.Range(2, 7);
        Debug.Log(moduloRange);
        for (var i = 0; i < lrArray.Length; i++)
        {
            if (i % moduloRange == 0)
            {
                continue;
            }

            GameObject lineObject = new GameObject();
            lrObjectsArray[i] = lineObject;
            lineObject.transform.position = Vector3.zero;
            lrArray[i] = lineObject.AddComponent<LineRenderer>();
            lrArray[i].material = new Material(Shader.Find("Sprites/Default"));
            lrArray[i].numCapVertices = 10;
            lrArray[i].positionCount = 2;
            lrArray[i].startWidth = 0.1f;
            lrArray[i].endWidth = 0.1f;

            Vector3[] posArray = new Vector3[2];
            posArray[0] = pointsArray[i];
            if (i < lrArray.Length - 1)
            {
                posArray[1] = pointsArray[i + 1];
            }
            else
            {
                posArray[1] = pointsArray[0];
            }

            lrArray[i].SetPositions(posArray);
            lrArray[i].loop = false;
        }

        movingHexagon = true;
    }

    void RestartSequence()
    {
        for (var i = 0; i < 6; i++)
        {
            Destroy(objectArray[i]);
            Destroy(lrObjectsArray[i]);
            pointsArray[i] = Vector3.zero;
        }

        DefinePositions();
    }
}

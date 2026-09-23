using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CubeRotation : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    [SerializeField] private float spawnTime;

    [SerializeField] private GameObject mainCanva;
    [SerializeField] private Text scoreText;
    private int score;

    [SerializeField] private Camera cam;
    private RaycastHit hit;

    private Vector3 touchStart;

    private bool started;
    private int cubeNumber;

    void Update()
    {
        if (!started) return;
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            touchStart = touch.position;
            Ray raycast = cam.ScreenPointToRay(touch.position);

            if (Physics.Raycast(raycast, out hit))
            {
                if (hit.collider.CompareTag("Cube"))
                {
                    Destroy(hit.collider.gameObject);
                    cubeNumber--;
                    score++;
                    scoreText.text = score.ToString();
                }
            }
        }
    }

    public void Activate()
    {
        started = true;
        mainCanva.SetActive(true);
        StartCoroutine(SpawnCubes());
    }

    public IEnumerator SpawnCubes()
    {
        if (cubeNumber < 10)
        {
            Vector3 playerPosition = cam.transform.position;

            float angle = Random.Range(0f, Mathf.PI * 2);
            float ray = 3.0f;

            float x = Mathf.Cos(angle) * ray;
            float z = Mathf.Sin(angle) * ray;
            float y = Random.Range(-0.5f, 1.5f);

            Vector3 cubePosition = new(playerPosition.x + x, playerPosition.y + y, playerPosition.z + z);

            Instantiate(cube, cubePosition, Quaternion.identity, transform);
            cubeNumber++;
        }

        yield return new WaitForSeconds(spawnTime);
        StartCoroutine(SpawnCubes());
    }
}

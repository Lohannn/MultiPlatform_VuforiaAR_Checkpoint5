using UnityEngine;

public class CubeRotation : MonoBehaviour
{
    [SerializeField] private Transform cube;
    [SerializeField] private float speed;

    private bool canMove;

    void Update()
    {
        if (canMove && Input.touchCount > 0)
        {
            cube.Rotate(Vector3.one * speed * Time.deltaTime);
        }
    }

    public void Activate()
    {
        canMove = true;
    }

    public void Deactivate()
    {
        canMove = false;
        cube.eulerAngles = Vector3.zero;
    }
}

using UnityEngine;

public class Controller : MonoBehaviour
{

    public float moveSpeed = 6;
    Camera viewCamera;
    Vector3 velocity;

    void Awake()
    {
        viewCamera = Camera.main;
    }

    void Update()
    {
        velocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
            velocity.z += 1;
        if (Input.GetKey(KeyCode.S))
            velocity.z -= 1;
        if (Input.GetKey(KeyCode.D))
            velocity.x += 1;
        if (Input.GetKey(KeyCode.A))
            velocity.x -= 1;

        Vector3 mousePos = viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, viewCamera.transform.position.y));
        transform.LookAt(mousePos + Vector3.up * transform.position.y);
        transform.position += velocity * moveSpeed * Time.deltaTime;
    }
}
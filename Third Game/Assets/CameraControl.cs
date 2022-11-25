using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraControl : MonoBehaviour
{
    public float speed = 5;

    public int currentMode = 0;
    public CameraFollow cameraFollow;


    // Start is called before the first frame update
    void Start()
    {
        cameraFollow = GetComponent<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SwitchMode();
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            SceneManager.LoadScene(1);
        }

        if (currentMode == 1)
        {
            return;
        }

        if (Input.GetKey(KeyCode.I))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.K))
        {
            transform.Translate(-Vector3.up * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.J))
        {
            transform.Translate(-Vector3.right * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.L))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }

    private void SwitchMode()
    {
        if (currentMode == 0)
        {
            cameraFollow.enabled = true;
            transform.position = new Vector3(transform.position.x, 10f, transform.position.z);
            currentMode = 1;
        }

        else if (currentMode == 1)
        {
            cameraFollow.enabled = false;
            transform.position = new Vector3(transform.position.x, 15f, transform.position.z);
            currentMode = 0;
        }
    }
}
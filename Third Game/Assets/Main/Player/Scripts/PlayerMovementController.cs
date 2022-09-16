using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{

    public float speed;
    public float turnSpeed;
    public PlayerInputController inputController;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        Move();
    }
    
    void Move() {
        
        if (inputController.isForward) {
            Debug.Log("<color=red>按下了W键</color>");
            transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        }
        if (inputController.isBackward) {
            Debug.Log("<color=blue>按下了S键</color>");
            transform.Translate(Vector3.back * (speed * Time.deltaTime));
        }
        if (inputController.isTurnLeft) {
            Debug.Log("<color=green>按下了A键</color>");
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        }
        if (inputController.isTurnRight) {
            Debug.Log("<color=yellow>按下了D键</color>");
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public bool isForward;
    public bool isBackward;
    public bool isTurnLeft;
    public  bool isTurnRight;
    
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        GetPlayerInput();
    }
    
    void GetPlayerInput() {
        isForward = Keyboard.current.wKey.isPressed;
        isBackward = Keyboard.current.sKey.isPressed;
        isTurnLeft = Keyboard.current.aKey.isPressed;
        isTurnRight = Keyboard.current.dKey.isPressed;
    }

}

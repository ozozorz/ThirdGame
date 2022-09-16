using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveTest : MonoBehaviour {
    private Animator animator;
    private float threshold = 0.1f;
    public float forwardSpeed = 2.0f;
    public float backwardSpeed = 1.5f;
    private float targetSpeed;
    private float currentSpeed;

    private Vector3 movement;

    private Vector2 moveInput;
    private Vector2 aimPosition;

    private bool isAiming;
    private bool currentAimState;

    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        Move();
        PlayerRotate();
    }

    void Move() {
        currentSpeed = Mathf.Lerp(targetSpeed, currentSpeed, 0.9f);
        movement = new Vector3(0, 0, currentSpeed * Time.deltaTime);
        transform.position += movement;
    }
    
    public void PlayerMove(InputAction.CallbackContext callbackContext) {
        Vector2 movement = callbackContext.ReadValue<Vector2>();
        targetSpeed = 0f;
        if (movement.y > threshold) {
            animator.SetBool("向前走", true);
            targetSpeed = forwardSpeed * movement.y;
        } else if (movement.y <= threshold) {
            animator.SetBool("向前走", false);
        }
        
        if (movement.y < -threshold) {
            animator.SetBool("向后退", true);
            targetSpeed = backwardSpeed * movement.y;
        } else if (movement.y >= -threshold) {
            animator.SetBool("向后退", false);
        }
    }
    
    public void GetAimPosition(InputAction.CallbackContext ctx) {
        aimPosition = ctx.ReadValue<Vector2>();
    }

    void PlayerRotate() {
        // Vector3 playerTargetPosition;
        // Ray ray = Camera.main.ScreenPointToRay(aimPosition);
        // Plane hitPlane = new Plane(Vector3.up, transform.position);
        // float distance;
        // if (hitPlane.Raycast(ray, out distance)) {
        //     playerTargetPosition = ray.GetPoint(distance);
        //     transform.LookAt(playerTargetPosition);
        // }
        // Debug.Log(aimPosition);
    }
}

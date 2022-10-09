using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveTest : LivingEntity {
    
    // private Animator animator;
    
    // 摇杆死区
    private float threshold = 0.1f;
    
    // 前进速度
    public float forwardSpeed = 2.0f;
    // 后退速度
    public float backwardSpeed = 1.5f;
    // 侧走速度
    public float strafeSpeed = 1.5f;
    
    // 目标速度
    private Vector2 targetSpeed;
    // 当前速度
    private Vector2 currentSpeed;

    private Vector3 movement;

    private Camera viewCamara;

    // Start is called before the first frame update
    public override void Start() {
        base.Start();
        // animator = GetComponent<Animator>();
        viewCamara = Camera.main;
    }

    // Update is called once per frame
    void Update() {
        Move();
        Look();
    }

    void Move() {
        currentSpeed.y = Mathf.Lerp(targetSpeed.y, currentSpeed.y, 0.9f);
        currentSpeed.x = Mathf.Lerp(targetSpeed.x, currentSpeed.x, 0.9f);
        //movement = new Vector3(currentSpeed.x * Time.deltaTime, 0, currentSpeed.y * Time.deltaTime);
        //movement = (transform.forward * currentSpeed.y + transform.right * currentSpeed.x) * Time.deltaTime;
        // transform.position += movement;
        cc.SimpleMove(transform.forward * currentSpeed.y + transform.right * currentSpeed.x);
        animator.SetFloat("Speed", currentSpeed.y);
    }
    
    public void PlayerMove(InputAction.CallbackContext callbackContext) {
        Vector2 moveInput = callbackContext.ReadValue<Vector2>();
        targetSpeed = new Vector2(0, 0);
        if (moveInput.y > threshold) {
            animator.SetBool("向前走", true);
            targetSpeed.y = forwardSpeed * moveInput.y;
        } else if (moveInput.y <= threshold) {
            animator.SetBool("向前走", false);
        }
        
        if (moveInput.y < -threshold) {
            animator.SetBool("向后退", true);
            targetSpeed.y = backwardSpeed * moveInput.y;
        } else if (moveInput.y >= -threshold) {
            animator.SetBool("向后退", false);
        }

        if (moveInput.x > threshold) {
            animator.SetBool("向右走", true);
            targetSpeed.x = strafeSpeed * moveInput.x;
        } else if (moveInput.x <= threshold) {
            animator.SetBool("向右走", false);
        }

        if (moveInput.x < -threshold) {
            animator.SetBool("向左走", true);
            targetSpeed.x = strafeSpeed * moveInput.x;
        } else if (moveInput.x >= -threshold) {
            animator.SetBool("向左走", false);
        }
    }

    private Vector3 world;
    void Look() {
        // Vector3 mousePosition = Pointer.current.position.ReadValue();
        // Vector3 playerPosition = Camera.main.WorldToScreenPoint(transform.position);
        // mousePosition.z = playerPosition.z;
        // world.x=Camera.main.ScreenToWorldPoint(mousePosition).x;
        // world.z=Camera.main.ScreenToWorldPoint(mousePosition).z;
        // world.y=transform.position.y;
        // transform.LookAt(world);
        
    }

    public void PlayerLook(InputAction.CallbackContext callbackContext) {
        Vector3 mousePosition = Pointer.current.position.ReadValue();
        Ray ray = viewCamara.ScreenPointToRay(mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;
        if (groundPlane.Raycast(ray, out rayDistance)) {
            Vector3 point = ray.GetPoint(rayDistance);
            point.y = transform.position.y;
            Debug.DrawLine(ray.origin, point, Color.red);
            transform.LookAt(point, Vector3.up);
        }
        // Debug.Log(mousePosition);
    }
}

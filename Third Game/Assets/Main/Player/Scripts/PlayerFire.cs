using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(GunController))]
public class PlayerFire : MonoBehaviour {
    private GunController gunController;

    // Start is called before the first frame update
    void Start() {
        gunController = GetComponent<GunController>();
    }

    // Update is called once per frame
    public void Fire(InputAction.CallbackContext callbackContext) {
        if (EnemySystem.instance.gameOver)
        {
            return;
        }

        if (callbackContext.ReadValue<float>() != 0) {
            gunController.Shoot();
        }
    }
}
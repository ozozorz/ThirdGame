using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OuterBehaviour : MonoBehaviour {

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("Player")) {
            if (CardSystem.instance.hasCard)
            {
                // TODO 游戏通关
                Debug.Log("游戏通关！");
            }
        }
    }
}

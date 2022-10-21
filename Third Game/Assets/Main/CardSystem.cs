using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardSystem : MonoBehaviour
{

    public static CardSystem instance;

    public bool hasCard = false;

    public GameObject card;

    private void Awake()
    {
        instance = this;
    }


    private void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("Player"))
        {
            hasCard = true;
            Destroy(card);
        }
    }
}

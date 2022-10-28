using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EquipSilencer : MonoBehaviour
{
    public GameObject silencner;

    public GunController gunController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            GunSoundSystem.instance.EquipSilencer();
            gunController.EquipSilencer();
            Destroy(silencner);
        }
    }
}
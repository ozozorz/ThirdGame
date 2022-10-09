using UnityEngine;
using System.Collections;

public class LightingController : MonoBehaviour
{
    public Light lighitningLight;

    // Use this for initialization
    void Start()
    {
        GunSoundSystem.instance.hideFireSound = false;
        StartCoroutine(PlayLightings());
    }

    IEnumerator PlayLightings()
    {
        while (true) {
            yield return new WaitForSeconds(1f);
            StartCoroutine(LightSmall());
            yield return new WaitForSeconds(4f);
            StartCoroutine(LightBig());
        }
    }

    IEnumerator LightBig()
    {
        lighitningLight.enabled = true;
        yield return new WaitForSeconds(0.07f);
        lighitningLight.enabled = false;
        yield return new WaitForSeconds(0.2f);
        lighitningLight.enabled = true;
        yield return new WaitForSeconds(0.07f);
        lighitningLight.enabled = false;
        yield return new WaitForSeconds(0.2f);
        lighitningLight.enabled = true;
        yield return new WaitForSeconds(0.2f);
        lighitningLight.enabled = false;
        yield return new WaitForSeconds(0.5f);
        PlaySound();

        GunSoundSystem.instance.hideFireSound = true;
        yield return new WaitForSeconds(1f);
        GunSoundSystem.instance.hideFireSound = false;
    }

    IEnumerator LightSmall()
    {
        lighitningLight.enabled = true;
        yield return new WaitForSeconds(0.07f);
        lighitningLight.enabled = false;
        yield return new WaitForSeconds(0.2f);
        lighitningLight.enabled = true;
        yield return new WaitForSeconds(0.2f);
        lighitningLight.enabled = false;
        yield return new WaitForSeconds(0.5f);
        PlaySound();
        
        GunSoundSystem.instance.hideFireSound = true;
        yield return new WaitForSeconds(1f);
        GunSoundSystem.instance.hideFireSound = false;
    }

    void PlaySound()
    {

    }
}

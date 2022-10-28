using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemySystem : MonoBehaviour
{
    public static EnemySystem instance;

    public List<EnemyBehaviour> enemies;

    public bool gameOver;

    public AudioSource alertSound;
    public AudioSource gunSound;
    public GameObject enemyBody;
    public AudioSource enemySpottedSound;

    private void Awake()
    {
        instance = this;
    }

    public void GameOver()
    {
        gameOver = true;

        StartCoroutine(PlaySounds());
    }

    IEnumerator PlaySounds()
    {
        alertSound.Play();
        yield return new WaitForSeconds(2.0f);
        gunSound.Play();
    }
}

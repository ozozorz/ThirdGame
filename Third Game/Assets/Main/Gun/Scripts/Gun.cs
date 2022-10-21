using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
    // public Vector3 offsetPosition;
    // public Vector3 offsetRotation;

    public Transform muzzle;
    public Projectile projectile;
    public GameObject gunShotFx;
    public float msBetweenShots = 100f;
    public float muzzleVelocity = 35f;

    public AudioSource shootSfx;

    public AudioSource silencerSfx;

    private float nextShotTime;

    public void Shoot() {
        if (Time.time > nextShotTime) {
            nextShotTime = Time.time + msBetweenShots / 1000f;
            Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation);
            newProjectile.SetSpeed(muzzleVelocity);
            Instantiate(gunShotFx, muzzle.position, muzzle.rotation);
            PlayFireSound();
        }
    }

    public void PlayFireSound() {
        if (GunSoundSystem.instance.hasSilencer)
        {
            silencerSfx.Play();
        }
        else
        {
            shootSfx.Play();
        }
        
        if (GunSoundSystem.instance.hideFireSound) {
            return;
        }
        
        var enes = EnemySystem.instance.enemies;
        foreach (var e in enes) {
            if (e == null)
                continue;
            e.GetComponent<CheckSoundBehaviour>()?.ReceiveSoundEvent(muzzle.position, 0);
        }
        
        // TODO 游戏结束
    }
}
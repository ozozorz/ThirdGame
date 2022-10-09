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
        if (GunSoundSystem.instance.hideFireSound) {
            return;
        }
        // 发出枪声
    }
}

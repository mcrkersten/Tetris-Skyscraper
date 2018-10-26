using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    [Header("BulletSettings")]
    public int damage;
    public int bulletSpeed;

    [Header("AmmoSettings")]
    public int ammo;
    public int magazines;
    public int projectileAmount;
    public GameObject projectile;

    [Header("GunSettings")]
    public float reloadTime;


    private void Reload() {

    }


    public void Shoot(int projectiles) {

    }
}

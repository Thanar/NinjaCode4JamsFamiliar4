﻿using UnityEngine;
using System.Collections;

public class GrenadeLauncher : FireWeapon {

	// Use this for initialization
	void Start () {
        damage = 20;
        armorPenetration = 100;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Attack()
    {
        if (nextTimeReady > Time.time)
        {
            return;
        }

        Bullet myBullet = ((GameObject)Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation)).GetComponent<Bullet>();

        myBullet.damage = this.damage;
        myBullet.armorPenetration = this.armorPenetration;
        myBullet.rigidbody.velocity = transform.forward.normalized * 10 + Vector3.up;

        nextTimeReady = Time.time + cooldown;
    }
}
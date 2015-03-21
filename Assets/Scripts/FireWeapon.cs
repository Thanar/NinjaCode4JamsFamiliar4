using UnityEngine;
using System.Collections;

public class FireWeapon : Weapon {

    public float nextTimeReady = 0;
    public float cooldown = 1;

    public float reloadTime = 3;

    public int bulletsTotal = 30;
    public int currentBullets = 12;
    public int bulletsPerCharge = 12;

    public Transform bulletSpawnPoint;

    public GameObject bulletPrefab;

    public override void Attack()
    {
        base.Attack();
        if (nextTimeReady > Time.time)
        {
            return;
        }
        if (currentBullets > 0)
        {
            Bullet myBullet = ((GameObject)Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation)).GetComponent<Bullet>();

            myBullet.damage = this.damage;
            myBullet.armorPenetration = this.armorPenetration;
            myBullet.rigidbody.velocity = transform.forward.normalized * 100;

            nextTimeReady = Time.time + cooldown;

            currentBullets--;
        }
        else
        {
            if (bulletsTotal > 0)
            {
                bulletsTotal -= bulletsPerCharge;
                if (bulletsTotal < 0)
                {
                    bulletsTotal = 0;
                }

                currentBullets = bulletsPerCharge;
                if (bulletsTotal < currentBullets)
                {
                    currentBullets = bulletsTotal;
                }
                nextTimeReady = Time.time + reloadTime;
            }
        }

    }
}

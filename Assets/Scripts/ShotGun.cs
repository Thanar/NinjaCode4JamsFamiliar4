using UnityEngine;
using System.Collections;

public class ShotGun : Weapon {


    public override void Attack()
    {
        //base.Attack();
        if (nextTimeReady > Time.time)
        {
            return;
        }
        if (currentBullets > 0)
        {


            shootSound.Play();


            for (int i = 0; i < 5; i++)
            {
                Bullet myBullet = ((GameObject)Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation)).GetComponent<Bullet>();

                myBullet.damage = this.damage;
                myBullet.armorPenetration = this.armorPenetration;
                myBullet.GetComponent<Rigidbody>().velocity = (bulletSpawnPoint.forward + bulletSpawnPoint.right * Random.Range(-0.1f, 0.1f) ).normalized * 100;
            }

                nextTimeReady = Time.time + cooldown;

            currentBullets--;
        }
        else
        {
            if (bulletsTotal > 0)
            {

                reloadingSound.Play();

                reloading = true;
                StartCoroutine(FinishReloading(reloadTime));
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

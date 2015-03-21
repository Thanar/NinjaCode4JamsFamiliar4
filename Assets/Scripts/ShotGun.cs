using UnityEngine;
using System.Collections;

public class ShotGun : Weapon {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Attack()
    {
        base.Attack();
        if (nextTimeReady > Time.time)
        {
            return;
        }
        if (currentBullets > 0)
        {
            

            for (int i = 0; i < 5; i++)
            {
                Bullet myBullet = ((GameObject)Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation)).GetComponent<Bullet>();

                myBullet.damage = this.damage;
                myBullet.armorPenetration = this.armorPenetration;
                myBullet.rigidbody.velocity = (bulletSpawnPoint.forward + bulletSpawnPoint.right * Random.Range(-0.1f, 0.1f) + bulletSpawnPoint.up * Random.Range(-0.2f, 0.2f)).normalized * 100;
            }

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

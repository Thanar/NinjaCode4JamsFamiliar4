using UnityEngine;
using System.Collections;

public class GrenadeLauncher : Weapon {

	// Use this for initialization
	public override void Start () {
        base.Start();
        damage = 20;
        armorPenetration = 100;
	}

    public override void Attack()
    {
        if (nextTimeReady > Time.time)
        {
            return;
        }

        Grenade myBullet = ((GameObject)Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation)).GetComponent<Grenade>();
        shootSound.Play();
        myBullet.damage = this.damage;
        myBullet.armorPenetration = this.armorPenetration;
        myBullet.explosionTime = Time.time + 3f;
        myBullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward.normalized * 10 + Vector3.up;

        nextTimeReady = Time.time + cooldown + Random.Range(0,0.1f);
    }
}

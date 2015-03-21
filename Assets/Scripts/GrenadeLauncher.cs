using UnityEngine;
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
        Debug.Log("LAUNCH");
        if (nextTimeReady > Time.time)
        {
            return;
        }

        Grenade myBullet = ((GameObject)Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation)).GetComponent<Grenade>();

        myBullet.damage = this.damage;
        myBullet.armorPenetration = this.armorPenetration;
        myBullet.explosionTime = Time.time + 5;
        myBullet.rigidbody.velocity = transform.forward.normalized * 10 + Vector3.up;

        nextTimeReady = Time.time + cooldown;
    }
}

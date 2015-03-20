using UnityEngine;
using System.Collections;

public class FireWeapon : Weapon {

    public float nextTimeReady = 0;
    public float cooldown = 1;

    public GameObject bulletPrefab;

    public override void Attack()
    {
        base.Attack();
        if (nextTimeReady > Time.time)
        {
            return;
        }

        Bullet myBullet = ((GameObject)Instantiate(bulletPrefab, transform.position, transform.rotation)).GetComponent<Bullet>();

        myBullet.damage = this.damage;
        myBullet.armorPenetration = this.armorPenetration;
        myBullet.rigidbody.velocity = transform.forward.normalized * 100;
    }
}

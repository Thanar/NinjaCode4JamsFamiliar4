using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public float damage = 10;
    public float armorPenetration = 0;
    public DropWeapon dropWeapon;


    public float nextTimeReady = 0;
    public float cooldown = 1;

    public float reloadTime = 3;

    public int bulletsTotal = 30;
    public int currentBullets = 12;
    public int bulletsPerCharge = 12;

    public Transform bulletSpawnPoint;

    public GameObject bulletPrefab;

    public bool reloading = false;
    public bool depleted = false;

    public virtual void Attack()
    {
        if (nextTimeReady > Time.time)
        {
            return;
        }
        if (currentBullets > 0)
        {
            Bullet myBullet = ((GameObject)Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation)).GetComponent<Bullet>();

            myBullet.damage = this.damage;
            myBullet.armorPenetration = this.armorPenetration;
            myBullet.rigidbody.velocity = bulletSpawnPoint.forward.normalized * 100;

            nextTimeReady = Time.time + cooldown + Random.Range(0, 0.1f);

            currentBullets--;
        }
        else
        {

            if (bulletsTotal > 0)
            {
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
                nextTimeReady = Time.time + reloadTime + Random.Range(0, 0.1f);
            }
            else
            {
                depleted = true;
            }
        }

    }

    public virtual void Dropped()
    {

    }

    public virtual void Taken()
    {

    }

    public IEnumerator FinishReloading(float time)
    {
        yield return new WaitForSeconds(time);
        reloading = false;
    }

    public virtual bool IsDepleted()
    {
        return this.depleted;
    }
}

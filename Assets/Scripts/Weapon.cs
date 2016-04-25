using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{

	public AudioSource shootSound;
	public AudioSource reloadingSound;
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
	public LineRenderer laserSight;
	public bool laserSightActivate = false;

	public GameObject discPrefab;

	public Transform discSpawnPoint;

	public virtual void Start ()
	{
		laserSightActivate = false;

	}

	/*  public virtual void Update()
    {
       if (laserSightActivate)
        {
            RaycastHit hit;
            Ray ray = new Ray(bulletSpawnPoint.position, bulletSpawnPoint.forward);
            if (Physics.Raycast(ray, out hit))
            {

               laserSight.SetPosition(0, bulletSpawnPoint.position);
               laserSight.SetPosition(1, hit.point);
            }
            else
            {

                laserSight.SetPosition(0, bulletSpawnPoint.position);
                laserSight.SetPosition(1, bulletSpawnPoint.position+bulletSpawnPoint.forward*1000);
            }
        }
        else
        {
            laserSight.SetPosition(0, Vector3.zero);
            laserSight.SetPosition(1, Vector3.zero);
        }

    }*/
	public virtual void AttackDisc(){
		Debug.Log ("aparece");
			

			shootSound.Play ();
			
			//Disco myDisc = ((GameObject)Instantiate (discPrefab, discSpawnPoint.position, discSpawnPoint.rotation)).GetComponent<Disco> ();

			Disco myDisc= dropWeapon.GetComponent<Disco>();
		//	myDisc.damage = damage;

			myDisc.armorPenetration = this.armorPenetration;
			
		myDisc.gameObject.GetComponent<BoxCollider> ().isTrigger = false;
		//myDisc.gameObject.AddComponent<Rigidbody>();
		myDisc.gameObject.GetComponent<Rigidbody>().isKinematic=false;
		myDisc.gameObject.GetComponent<Rigidbody> ().detectCollisions = true;

		myDisc.GetComponent<Rigidbody> ().velocity = (discSpawnPoint.forward+ discSpawnPoint.right*Random.Range (-0.1f, 0.1f)).normalized*100;
			
		//+ discSpawnPoint.right


	}

	public virtual void Attack ()
	{

		if (nextTimeReady > Time.time) {
			return;
		}

		if (currentBullets > 0) {
			shootSound.Play ();

			Bullet myBullet = ((GameObject)Instantiate (bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation)).GetComponent<Bullet> ();

			myBullet.damage = this.damage;
			myBullet.armorPenetration = this.armorPenetration;
			myBullet.GetComponent<Rigidbody> ().velocity = bulletSpawnPoint.forward.normalized * 100;

			nextTimeReady = Time.time + cooldown + Random.Range (0, 0.1f);

			currentBullets--;
		} else {

			if (bulletsTotal > 0) {
				reloadingSound.Play ();
				reloading = true;
				StartCoroutine (FinishReloading (reloadTime));
				if (bulletsTotal < 0) {
					bulletsTotal = 0;
				}

				currentBullets = bulletsPerCharge;
				if (bulletsTotal < bulletsPerCharge) {
					currentBullets = bulletsTotal;
				}
				bulletsTotal -= currentBullets;
				nextTimeReady = Time.time + reloadTime + Random.Range (0, 0.1f);
			} else {
				depleted = true;
			}
		}

	}

	public virtual void  RechargeBullets ()//funcion de recargar balas
	{
		int bulletsToAdd = 0;

		if (bulletsTotal > 0 ||currentBullets > 0) {//SI QUEDAN BALAS EN LA RECAMARA O EN LOS BOLSILLOS NO SE SUELTA EL ARMA AUTAMATICAMENTE
			reloadingSound.Play ();
			reloading = true;
			StartCoroutine (FinishReloading (reloadTime));


			if (bulletsTotal > 0 ){// SOLO SI TENGO BALAS PARA RECARGAR

				bulletsToAdd = bulletsPerCharge - currentBullets;

			if (bulletsTotal >= bulletsToAdd) {
				
				currentBullets += bulletsToAdd;
				bulletsTotal -= bulletsToAdd;
			
			} else {
				currentBullets += bulletsTotal;
				bulletsTotal = 0;
			
			}

			if (bulletsTotal < 0) {
				bulletsTotal = 0;
			}
		
			nextTimeReady = Time.time + reloadTime + Random.Range (0, 0.1f);
			}

		} else {
			depleted = true;
		
			
		}
	}

	public virtual void Dropped ()
	{
		transform.parent = null;
		dropWeapon.enabled = true;
		dropWeapon.iAmDropped = true;
	}







	public virtual void Taken ()
	{

	}

	public IEnumerator FinishReloading (float time)
	{
		yield return new WaitForSeconds (time);
		reloading = false;
	}

	public virtual bool IsDepleted ()
	{
		return this.depleted;
	}
}

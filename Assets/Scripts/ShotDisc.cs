using UnityEngine;
using System.Collections;

public class ShotDisc : Weapon {



	public override void AttackDisc()
	{Debug.Log ("aparece");

		//base.Attack();


			
			
		shootSound.Play ();
			
			

		Disco myDisc= dropWeapon.GetComponent<Disco>();
		myDisc.damage = this.damage;
		
		myDisc.armorPenetration = this.armorPenetration;
		
		myDisc.gameObject.GetComponent<BoxCollider> ().isTrigger = false;
		//myDisc.gameObject.AddComponent<Rigidbody>();
		

		myDisc.GetComponent<Rigidbody> ().velocity = (discSpawnPoint.forward + discSpawnPoint.right * Random.Range (1f, 2f)).normalized * 100;




	}
}

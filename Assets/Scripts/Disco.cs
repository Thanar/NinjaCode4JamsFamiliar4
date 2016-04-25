using UnityEngine;
using System.Collections;

public class Disco :Weapon{
	
	//public float damage=10;
	//public float armorPenetration=0;


	void OnCollisionEnter(Collision hit)//en este caso existe fuego amigo para los enemigos
	{	
		Character other = hit.gameObject.GetComponent<Character>();

		if (other)
		{

			other.Damage(damage, armorPenetration)  ;

			Debug.Log("damage"+damage);
		}
		// (GetComponent<Rigidbody> ().velocity.magnitude< 0.01f) {

	//	}
		if(!hit.gameObject.tag.Equals("Enemy"))//&& !hit.gameObject.tag.Equals("Player"))
		{	
			StartCoroutine("WaitForTriggerTrue");

		}

	}

	IEnumerator WaitForTriggerTrue(){
		Debug.Log("hitrare");
		yield return new WaitForSeconds (0.01f);
		GetComponent<BoxCollider>().isTrigger=true;
		GetComponent<Rigidbody>().isKinematic=true;
	}
}

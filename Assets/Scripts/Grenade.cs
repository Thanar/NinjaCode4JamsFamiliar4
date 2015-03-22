using UnityEngine;
using System.Collections;

public class Grenade : MonoBehaviour {

    public AudioSource granadeClinkAudio;
    public AudioSource granadeExplosionAudio;


    public float damage = 20;
    public float armorPenetration = 100;

    public float explosionTime = 0;

    public GameObject explosionPrefab;

    public void SetCountDown(float sec)
    {
        explosionTime = Time.time + sec;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (explosionTime < Time.time)
        {

            granadeExplosionAudio.Play();

            Character auxCharacter;
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            foreach (Collider c in Physics.OverlapSphere(transform.position, 2.5f))
            {
                auxCharacter = c.GetComponent<Character>();
                if (auxCharacter)
                {
                    auxCharacter.Damage(damage, armorPenetration);
                    //auxCharacter.rigidbody.AddForce((transform.forward.normalized) * 8, ForceMode.VelocityChange);
                    auxCharacter.Push((auxCharacter.transform.position - transform.position).normalized * 5);
                }
                else
                {
                    if (c.rigidbody && !c.rigidbody.isKinematic)
                    {
                        c.rigidbody.velocity = ((c.transform.position - transform.position).normalized * 10);
                    }
                }
            }
            Destroy(this.gameObject);
        }
	}



    public void OnCollisionEnter(Collision other)
    {
        granadeClinkAudio.Play();
    }
}

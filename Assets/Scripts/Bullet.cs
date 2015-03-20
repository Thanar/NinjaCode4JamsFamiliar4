using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float damage;
    public float armorPenetration;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnCollisionEnter(Collision collision)
    {
        collision.other.GetComponent<Character>().Damage(damage);
    }
}

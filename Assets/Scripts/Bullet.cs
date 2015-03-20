﻿using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float damage=10;
    public float armorPenetration=0;

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, 5);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnCollisionEnter(Collision collision)
    {
        Character other = collision.gameObject.GetComponent<Character>();
        if (other)
        {
            other.Damage(damage, armorPenetration);

            Destroy(this.gameObject);

        }
    }
}
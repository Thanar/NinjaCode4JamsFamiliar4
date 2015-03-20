using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    public float health = 100;
    public float armor = 0;

    public float charachterImpulse = 10000;
    public float maxSpeed = 2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual void Damage(float damage, float armorPenetration = 0)
    {
        float currentArmor = armor - armorPenetration;

        if (currentArmor < 0)
        {
            currentArmor = 0;
        }

        damage = damage * (100 / (100 + currentArmor));

        health -= damage;
        

        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        GameObject.Destroy(this.gameObject);
    }
}

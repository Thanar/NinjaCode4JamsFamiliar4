using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    public float Maxhealth = 100;
    public float health = 100;
    public float armor = 0;

    public float charachterImpulse = 10000;
    public float maxSpeed = 2;


    public float fistsTimeReady = 0;
    public float fistsCooldown = 2;
    public float fistsRange = 1;

    public float fistsDamage = 5;
    public float fistsArmorPenetration = 0;

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

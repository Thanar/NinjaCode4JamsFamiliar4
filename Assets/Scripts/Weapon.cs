using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public float damage = 10;
    public float armorPenetration = 0;
    public DropWeapon dropWeapon;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual void Attack()
    {

    }

    public virtual void Dropped()
    {

    }

    public virtual void Taken()
    {

    }

    public virtual bool IsDepleted()
    {
        return false;
    }
}

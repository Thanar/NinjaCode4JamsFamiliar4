using UnityEngine;
using System.Collections;

public class EnemyAI : Character {

    public PlayerController player;

    public Weapon weapon;
    public bool hasWeapon = false;

    public Transform weaponTransform;


    Vector3 newRotation = new Vector3(0, 0, 0);


	void Start () {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
        if (weapon != null)
        {
            TakeWeapon(weapon);
        }
	}

    void Update()
    {
        newRotation.y = Quaternion.LookRotation(player.transform.position - transform.position).eulerAngles.y;
        transform.eulerAngles = newRotation;


        if (hasWeapon)
        {
            weapon.Attack();
        }
    }

    public void TakeWeapon(Weapon w)
    {
        this.weapon = w;
        hasWeapon = true;
        weapon.rigidbody.isKinematic = true;
        weapon.collider.enabled = false;
        weapon.transform.position = weaponTransform.position;
        weapon.transform.rotation = weaponTransform.rotation;
        weapon.transform.parent = weaponTransform;
    }

    public void DropWeapon()
    {
        this.weapon = null;
        hasWeapon = false;
        weapon.rigidbody.isKinematic = false;
        weapon.collider.enabled = true;
        //weapon.transform.position = weaponTransform.position;
        //weapon.transform.rotation = weaponTransform.rotation;
        weapon.transform.parent = null;
    }

    void FixedUpdate()
    {
        rigidbody.AddForce((player.transform.position-transform.position).normalized*1000);
    }



    public override void Die()
    {
        //base.Die();

        DropWeapon();

        GameObject.Destroy(this.gameObject);
    }
}

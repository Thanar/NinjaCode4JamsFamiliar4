using UnityEngine;
using System.Collections;

public class EnemyAI : Character {

    public PlayerController player;

    public Weapon weapon;
    public bool hasWeapon = false;

    public Transform weaponTransform;


    Vector3 newRotation = new Vector3(0, 0, 0);
    Vector3 player2DDirection = new Vector3(0,0,0);


    public float fistsTimeReady = 0;
    public float fistsCooldown = 2;
    public float fistsRange = 1;

    public float fistsDamage = 5;
    public float fistsArmorPenetration = 0;


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
        else
        {
            if (fistsTimeReady < Time.time)
            {
                if(Vector3.Distance(transform.position,player.transform.position)<fistsRange)
                {
                    Debug.Log("FALCON PUNCH");
                    fistsTimeReady = Time.time + fistsCooldown;
                    player.Damage(fistsDamage,fistsArmorPenetration);
                    player.rigidbody.AddForce((transform.forward.normalized+Vector3.up)*2,ForceMode.VelocityChange);
                }
            }
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
        weapon.Taken();
    }

    public void DropWeapon()
    {

        hasWeapon = false;
        weapon.rigidbody.isKinematic = false;
        weapon.collider.enabled = true;
        //weapon.transform.position = weaponTransform.position;
        //weapon.transform.rotation = weaponTransform.rotation;
        weapon.transform.parent = null;
        weapon.Dropped();
        this.weapon = null;

    }

    void FixedUpdate()
    {
        player2DDirection = (player.transform.position - transform.position).normalized;
        player2DDirection.y = 0;
        rigidbody.AddForce(player2DDirection*1000);
    }



    public override void Die()
    {
        //base.Die();

        DropWeapon();

        GameObject.Destroy(this.gameObject);
    }
}

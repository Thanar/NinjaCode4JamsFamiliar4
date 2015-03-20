using UnityEngine;
using System.Collections;

public class EnemyAI : Character {

    public PlayerController player;

    public Weapon weapon;
    public bool hasWeapon = false;


    Vector3 newRotation = new Vector3(0, 0, 0);


	void Start () {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }

        hasWeapon = weapon != null;
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

    void FixedUpdate()
    {
        rigidbody.AddForce((player.transform.position-transform.position).normalized*1000);
    }
}

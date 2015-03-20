using UnityEngine;
using System.Collections;

public class EnemyAI : Character {

    public PlayerController player;

    public Weapon weapon;


	void Start () {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
	}

    void Update()
    {
        weapon.Attack();
    }

    void FixedUpdate()
    {
        rigidbody.AddForce((player.transform.position-transform.position).normalized*1000);
    }
}

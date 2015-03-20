using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    public PlayerController player;
	void Start () {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
	}

    void FixedUpdate()
    {
        rigidbody.AddForce((player.transform.position-transform.position).normalized*1000);
    }
}

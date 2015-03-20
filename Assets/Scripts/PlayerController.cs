using UnityEngine;
using System.Collections;

public class PlayerController : Character {

	// Use this for initialization
	void Start () {
	
	}


    void FixedUpdate()
    {
        Debug.Log("Fixed Update");
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody.AddForce(-Vector3.right*1000);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rigidbody.AddForce(Vector3.right*1000);
        }

        if (Input.GetKey(KeyCode.W))
        {
            rigidbody.AddForce(Vector3.forward*1000);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rigidbody.AddForce(-Vector3.forward*1000);
        }
	}
}

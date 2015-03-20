using UnityEngine;
using System.Collections;

public class PlayerController : Character {

    public Vector3 newRotation = new Vector3(0,0,0);

    Vector3 rayVector = new Vector3();

    public float movementForceScale=1;

	// Use this for initialization
	void Start () {
	
	}

    void Update()
    {
        RaycastHit hit;
        rayVector = Input.mousePosition;
        rayVector.z = 10000;
        Ray ray = Camera.main.ScreenPointToRay(rayVector);
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.transform.position);
            newRotation.y = Quaternion.LookRotation(hit.point - transform.position).eulerAngles.y;
            transform.eulerAngles = newRotation;


        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody.AddForce(-Vector3.right.normalized * 1000 * movementForceScale);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rigidbody.AddForce(Vector3.right.normalized * 1000 * movementForceScale);
        }

        if (Input.GetKey(KeyCode.W))
        {
            rigidbody.AddForce(Vector3.forward.normalized * 1000 * movementForceScale);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rigidbody.AddForce(-Vector3.forward.normalized * 1000 * movementForceScale);
        }
	}
}

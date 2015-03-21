using UnityEngine;
using System.Collections;

public class EnemyRagdollController : MonoBehaviour {

    public Rigidbody mainRigidbody;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 7);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}

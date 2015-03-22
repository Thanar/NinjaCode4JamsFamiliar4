using UnityEngine;
using System.Collections;

public class EnemyRagdollController : MonoBehaviour {

    public Rigidbody mainRigidbody;

    public AudioSource[] sonidos;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 7);
        int i = Random.Range(0, sonidos.Length);
        AudioSource j = sonidos[i];
        j.Play();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}

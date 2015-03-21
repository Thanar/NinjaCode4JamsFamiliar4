using UnityEngine;
using System.Collections;

public class Heal : MonoBehaviour {

    public float heal = 25f;
    public GameObject parent;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("triger: " + other.tag);
        if (other.tag == "Player")
        {

            PlayerController p = other.GetComponent<PlayerController>();
            p.AddHealth(heal);
            Destroy(parent);

            
        }
    }
}
